import argparse
import json
import os
import re
from time import time

import gensim
import nltk
import numpy as np
import pandas as pd
from gensim.models.doc2vec import LabeledSentence
from hazm import word_tokenize
from NLPInfrastructure import SentenceNormalizer
from NLPInfrastructure.resources import Not1Gram, postWords, prepositions, stopWords
from sklearn.decomposition import TruncatedSVD
from sklearn.ensemble import RandomForestClassifier
from sklearn.feature_extraction.text import CountVectorizer, TfidfVectorizer
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import (
    accuracy_score,
    classification_report,
    confusion_matrix,
    f1_score,
    precision_score,
    recall_score,
)
from gensim.models import Word2Vec
from sklearn.model_selection import train_test_split
from sklearn.neighbors import KNeighborsClassifier
from tqdm import tqdm
from xgboost import XGBClassifier

normalizer = SentenceNormalizer()
BASE_DIR = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))


def train(X, Y, configs):
    vectorizer = get_vectorizer(configs["vectorizer"])
    X_train_vectorized = vectorizer.fit_transform(X)

    if "dimentionality_reduction" in configs:
        dim_red = get_dimentionality_reduction(configs["dimentionality_reduction"])
        X_train_vectorized = dim_red.fit_transform(X_train_vectorized)
    else:
        dim_red = None

    classifier = get_classifier(configs["classifier"])
    classifier.fit(X_train_vectorized, Y)

    return vectorizer, dim_red, classifier


class FileManager:
    def __init__(self, file_name, remove_stop_words=True):
        df = pd.read_excel(file_name)

        if ("text" not in df.columns) | ("label" not in df.columns):
            raise Exception("There is no [text] or [label] column in input file!")

        df.drop_duplicates(["text"], keep="last", inplace=True)
        df = df[df["text"].notna()]
        df = df[df["label"].notna()]

        df["text"] = df["text"].astype(str)
        texts = df["text"].values
        texts = self.normalize_texts(texts)

        if remove_stop_words:
            print("Remove stopwords")
            texts = [self.remove_stopword(t) for t in tqdm(texts)]

        df["text"] = texts
        self.df = df

    @staticmethod
    def remove_stopword(text):
        words = text.split(" ")
        words_filtered = []
        for w in words:
            if (
                    (w not in stopWords)
                    and (w not in prepositions)
                    and (w not in postWords)
                    and (w not in Not1Gram)
            ):
                words_filtered.append(w)
        return " ".join(words_filtered)

    @staticmethod
    def normalize_texts(texts):
        def normalize_sentence(text: str):
            def convert_hashtag_to_text(text: str):
                return text.replace("#", " ").replace("_", " ")

            text = normalizer.organize_text(text)
            text = normalizer.replace_urls(text)
            text = normalizer.replace_emails(text)
            text = normalizer.replace_usernames(text)
            text = normalizer.edit_arabic_letters(text)
            text = normalizer.replace_phone_numbers(text)
            text = normalizer.replace_emoji(text)
            text = normalizer.replace_duplicate_punctuation(text)

            text = convert_hashtag_to_text(text)

            text = normalizer.replace_consecutive_spaces(text)
            return text

        print("Normalizing sentences...")
        return [normalize_sentence(text) for text in tqdm(texts)]


def get_vectorizer(vectorizer):
    class Word2VecVectorizer:
        def __init__(self, vector_size=100):
            w2v_model = Word2Vec(size=vector_size, window=5, min_count=2, workers=4)
            self.w2v_model = w2v_model
            self.vector_size = vector_size

        def fit_transform(self, texts):
            self.fit(texts=texts)
            return self.transform(texts=texts)

        def fit(self, texts):

            texts_tokenized = self.tokenize_series(texts)

            t = time()
            self.w2v_model.build_vocab(sentences=texts_tokenized, progress_per=10000)
            print("Time to build vocab: {} mins".format(round((time() - t) / 60, 2)))

            t = time()
            self.w2v_model.train(
                sentences=texts_tokenized,
                total_examples=self.w2v_model.corpus_count,
                epochs=30,
                report_delay=1,
            )
            print(
                "Time to train the model: {} mins".format(round((time() - t) / 60, 2))
            )

            self.w2v_model.init_sims(replace=True)

        def tokenize_series(self, X):
            texts = list(X)
            texts_tokenized = [tokenize(text) for text in texts]
            return texts_tokenized

        def transform(self, texts):
            texts_tokenized = self.tokenize_series(texts)

            results = []
            for idx, text in enumerate(texts_tokenized):
                vectors = []
                for word in text:
                    if word in self.w2v_model.wv:
                        vectors.append(self.w2v_model.wv[word])
                # we should convert 2d array to 1d
                if len(vectors) == 0:
                    vectors = np.zeros((1, 100))
                else:
                    vectors = np.array(vectors)

                vectors = np.mean(vectors, axis=0)

                results.append(vectors)
            results_array = np.array(results)
            return results_array

    if vectorizer["title"] == "TfidfVectorizer":
        return TfidfVectorizer(
            tokenizer=tokenize,
            ngram_range=(1, 3),
            # min_df=.0025,
            # max_df=0.25
        )
    elif vectorizer["title"] == "CountVectorizer":
        return CountVectorizer(
            tokenizer=tokenize,
            ngram_range=(1, 3),
            # min_df=.0025,
            # max_df=0.25
        )
    elif vectorizer["title"] == "Word2VecVectorizer":
        return Word2VecVectorizer()
    else:
        raise Exception("Unsupported Vectorizer!")


def get_dimentionality_reduction(dimentionality_reduction):
    if dimentionality_reduction["title"] == "TruncatedSVD":
        return TruncatedSVD(
            algorithm="randomized",
            n_components=dimentionality_reduction["n_components"],
        )
    else:
        raise Exception("Unsupported Dimentionality Reduction Method!")


def get_classifier(classifier):
    if classifier["title"] == "KNeighborsClassifier":
        return KNeighborsClassifier(n_neighbors=5, n_jobs=-1)
    elif classifier["title"] == "XGBClassifier":
        return XGBClassifier(max_depth=3, n_estimators=300, learning_rate=0.1)
    elif classifier["title"] == "LogisticRegression":
        return LogisticRegression(
            C=0.3, penalty="l2", solver="liblinear", max_iter=600, verbose=1
        )
    elif classifier["title"] == "RandomForestClassifier":
        return RandomForestClassifier(n_estimators=100, n_jobs=-1)
    else:
        raise Exception("Unsupported Classifier!")


def tokenize(inp_str):
    words = word_tokenize(inp_str)
    return words


def train_evaluate_validation(X_train, X_test, y_train, y_test, result_dir, configs):
    vectorizer, dim_red, classifier = train(X_train, y_train, configs)

    print("Validation")
    X_test_vectorized = vectorizer.transform(X_test)

    if dim_red is not None:
        X_test_vectorized = dim_red.transform(X_test_vectorized)

    preds = classifier.predict(X_test_vectorized)

    metrics = {
        "Accuracy": accuracy_score(y_test, preds),
        "Precision": precision_score(y_test, preds),
        "Recall": recall_score(y_test, preds),
        "F1": f1_score(y_test, preds),
    }

    if "dimentionality_reduction" in configs:
        result_file_name = "{}-{}-{}.json".format(
            configs["vectorizer"]["title"],
            configs["dimentionality_reduction"]["title"],
            configs["classifier"]["title"],
        )
    else:
        result_file_name = "{}-{}.json".format(
            configs["vectorizer"]["title"], configs["classifier"]["title"]
        )

    with open(os.path.join(result_dir, result_file_name), mode="w") as f:
        json.dump(metrics, f)

    print(metrics)
    print("Done doing method: ", configs)


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument(
        "train",
        metavar="train",
        help="train file location"
    )
    parser.add_argument(
        "test",
        metavar="test",
        help="test file location"
    )
    parser.add_argument(
        "result_dir",
        metavar="result_directory",
        help="result directory location"
    )
    parser.add_argument(
        "vectorizer",
        metavar="vectorizer",
        help="vectorizer algorithm"
    )
    parser.add_argument(
        "classifier",
        metavar="classifier",
        help="classifier algorithm"
    )
    parser.add_argument(
        "--dimentionality_reduction",
        metavar="dimentionality_reduction",
        help="dimentionality reduction algorithm",
        default=None,
    )
    parser.add_argument(
        "--dimentionality_reduction_n_components",
        metavar="dimentionality_reduction_n_components",
        help="dimentionality reduction components",
        default=100,
    )
    args = parser.parse_args()

    train_path = args.train
    test_path = args.test
    result_dir = args.result_dir
    vectorizer = args.vectorizer
    classifier = args.classifier
    dimentionality_reduction = args.dimentionality_reduction
    dimentionality_reduction_n_components = args.dimentionality_reduction_n_components

    # train_path = 'D:\\Data\\Concepts\\Family\\data\\train.xlsx'
    # test_path = 'D:\\Data\\Concepts\\Family\\data\\test.xlsx'
    # result_dir = 'D:\\Data\\Concepts\\Family\\benchmarks\\2021-07-13T19-18-22'
    # vectorizer = 'Word2VecVectorizer'
    # classifier = 'XGBClassifier'
    # dimentionality_reduction = None
    # dimentionality_reduction_n_components = 100

    print("Train File location:", train_path)
    print("Test File location:", test_path)
    print("Result Directory location:", result_dir)
    print("Vectorizer:", vectorizer)
    print("Classifier:", classifier)
    print("Dimentionality Reduction Algorithm:", dimentionality_reduction)

    train_fm = FileManager(train_path)
    test_fm = FileManager(test_path)

    train_df = train_fm.df
    test_df = test_fm.df

    X_train = train_df["text"].values
    Y_train = train_df["label"].values

    X_test = test_df["text"].values
    Y_test = test_df["label"].values

    configs = {
        "vectorizer": {"title": vectorizer},
        "classifier": {"title": classifier},
    }

    if dimentionality_reduction is not None:
        configs["dimentionality_reduction"] = {
            "title": dimentionality_reduction,
            "n_components": dimentionality_reduction_n_components,
        }

    train_evaluate_validation(X_train, X_test, Y_train, Y_test, result_dir, configs)
