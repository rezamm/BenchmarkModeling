import os
import re
import argparse

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
from sklearn.model_selection import train_test_split
from sklearn.pipeline import FeatureUnion, Pipeline
from sklearn.preprocessing import StandardScaler
from tqdm import tqdm
from xgboost import XGBClassifier
from sklearn.neighbors import KNeighborsClassifier

import json

normalizer = SentenceNormalizer()
BASE_DIR = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))


def train(X, Y):
    vectorizer = TfidfVectorizer(
        tokenizer=tokenize,
        ngram_range=(1, 3),
        # min_df=.0025,
        # max_df=0.25
    )
    X_train_vectorized = vectorizer.fit_transform(X)

    classifier = KNeighborsClassifier(n_neighbors=5, n_jobs=-1)
    classifier.fit(X_train_vectorized, Y)

    return vectorizer, classifier


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


def tokenize(inp_str):
    words = word_tokenize(inp_str)
    return words


def train_evaluate_validation(X_train, X_test, y_train, y_test):
    vectorizer, classifier = train(X_train, y_train)

    print("Validation")
    X_test_vectorized = vectorizer.transform(X_test)
    preds = classifier.predict(X_test_vectorized)

    metrics = {
        "Accuracy": accuracy_score(y_test, preds),
        "Precision": precision_score(y_test, preds),
        "Recall": recall_score(y_test, preds),
        "F1": f1_score(y_test, preds),
        "classification_report": classification_report(y_test, preds),
        "confusion_matrix": confusion_matrix(y_test, preds),
    }

    with open("tfidf_knn.py.txt", mode="w") as f:
        json.dump(metrics, f)

    print("Accuracy:", accuracy_score(y_test, preds))
    print("Precision:", precision_score(y_test, preds))
    print("Recall:", recall_score(y_test, preds))
    print("F1:", f1_score(y_test, preds))
    print(classification_report(y_test, preds))
    print(confusion_matrix(y_test, preds))


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("train", metavar="train", help="train file location")
    parser.add_argument("test", metavar="test", help="test file location")
    args = parser.parse_args()

    train_fm = FileManager(args.train)
    test_fm = FileManager(args.test)

    train_df = train_fm.df
    test_df = test_fm.df

    X_train = train_df["text"]
    Y_train = train_df["label"]

    X_test = test_df["text"]
    Y_test = test_df["label"]

    train_evaluate_validation(X_train, Y_train, X_test, Y_test)
