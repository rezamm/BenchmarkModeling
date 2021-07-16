import argparse
import json
import os

import matplotlib.pyplot as plt
import pandas as pd
import numpy as np

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument(
        "result_directory",
        metavar="result_directory",
        help="Results Directory Location",
    )
    args = parser.parse_args()

    result_directory = args.result_directory
    # result_directory = (
    #     "D:\\Data\\Concepts\\Falling-of-America\\3_Benchmarks\\2021-06-26T15-07-40"
    # )

    print("Selected Result Directory:", result_directory)

    # read all files in directory
    files = os.listdir(result_directory)
    scores = []
    for f in files:
        if ".json" not in f:
            continue
        file_location = os.path.join(result_directory, f)
        with open(file_location, mode="r", encoding="utf-8") as fp:
            score = {**json.load(fp)}
            score["method"] = f
            scores.append(score)

    df = pd.DataFrame(scores)

    labels = df[["method"]].values
    accuracies = df[["Accuracy"]].values
    precisions = df[["Precision"]].values
    recalls = df[["Recall"]].values
    f1s = df[["F1"]].values

    figure, axis = plt.subplots(4, 1)

    axis[0].barh(list(labels.reshape(-1)), list(accuracies.reshape(-1)), color="red")
    axis[0].set_title("Accuracy")
    for container in axis[0].containers:
        axis[0].bar_label(container)

    axis[1].barh(list(labels.reshape(-1)), list(precisions.reshape(-1)), color="blue")
    axis[1].set_title("Precision")
    for container in axis[1].containers:
        axis[1].bar_label(container)

    axis[2].barh(list(labels.reshape(-1)), list(recalls.reshape(-1)), color="green")
    axis[2].set_title("Recall")
    for container in axis[2].containers:
        axis[2].bar_label(container)

    axis[3].barh(list(labels.reshape(-1)), list(f1s.reshape(-1)), color="orange")
    axis[3].set_title("F1")
    for container in axis[3].containers:
        axis[3].bar_label(container)

    mng = plt.get_current_fig_manager()
    mng.full_screen_toggle()
    figure.tight_layout()
    img_path = os.path.join(result_directory, "comparison1.png")
    plt.savefig(img_path, dpi=1000, pad_inches=5)

    # x = np.arange(len(labels))  # the label locations
    # width = 0.1  # the width of the bars

    # fig, ax = plt.subplots()
    # rects1 = ax.bar(x + (width/4)*1, accuracies.reshape(-1), width, label='Accuracy')
    # rects2 = ax.bar(x + (width/4)*2, precisions.reshape(-1), width, label='Precision')
    # rects3 = ax.bar(x + (width/4)*3, recalls.reshape(-1), width, label='Recall')
    # rects4 = ax.bar(x + (width/4)*4, f1s.reshape(-1), width, label='F1')

    # # Add some text for labels, title and custom x-axis tick labels, etc.
    # ax.set_ylabel('Scores')
    # ax.set_title('Method\'s Scores')
    # ax.set_xticks(x)
    # ax.set_xticklabels(labels)
    # ax.legend()

    # ax.bar_label(rects1, padding=3)
    # ax.bar_label(rects2, padding=3)
    # ax.bar_label(rects3, padding=3)
    # ax.bar_label(rects4, padding=3)

    # fig.tight_layout()

    # mng = plt.get_current_fig_manager()
    # mng.full_screen_toggle()
    # img_path = os.path.join(result_directory, "comparison.png")
    # plt.savefig(img_path, dpi=1000, pad_inches=5)

    df.set_index("method", inplace=True)
    df_show = df.T
    ax = df_show.plot.bar(fontsize=7)

    for container in ax.containers:
        ax.bar_label(container)

    plt.tight_layout(pad=2)
    plt.xticks(rotation=0)
    plt.legend(
        loc="upper center",
        bbox_to_anchor=(0.5, -0.05),
        ncol=2,
        prop={"size": 7},
    )
    mng = plt.get_current_fig_manager()
    mng.full_screen_toggle()
    img_path = os.path.join(result_directory, "comparison2.png")
    plt.savefig(img_path, dpi=200, pad_inches=5)
