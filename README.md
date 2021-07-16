# BenchmarkModeling
## Description
This repository includes a c# app interface and a python script as backend to run benchmark NLP methods on selected inputs.
<br/>
The main app interface will be something like screenshot below.
<br/>
<br/>
![App](https://user-images.githubusercontent.com/11568577/126014817-0dba08d7-9b88-432c-b4f9-2a07fb433233.PNG)

## Requirements
### Python Libraries
1. gensim
2. nltk
3. numpy
4. pandas
5. hazm
6. sklearn
7. tqdm
8. xgboost

## Inputs
You Should input two valid excel files as input, 1 for train and another for test. The excel files should have 2 columns [text] and [label]. The other columns will be ignored during the process.

## Process
After selecting inputs and result directory you can mark/unmark methods to be executed. After marking methods, select [Start] Button. It will create a background process and run every method marked from grid view. You can watch the process from status bar including current status, progress and current method.

## Outputs
After status label turned into "Ready" again the results are in the [Result Directory]. There will be a json file for each method marked. If you want to create diagrams including each metric for selected methods you can select [Show Latest Results] button. It will read latest experiment folder in result directory and uses files in json format to create diagrams.

## Methods
Each method consists of 3 parts. 2 essential and 1 optional. The encoding is [Vectorizer].[Dimentionality Reduction].[Classifier]. For [Vectorizer] part you can use {'TfidfVectorizer', 'CountVectorizer', 'Word2VecVectorizer' }. The [Dimentionality Reduction] part is optional. Available option for [Dimentionality Reduction] is {'TruncatedSVD'}. And for [Classifier] part you can use {'KNeighborsClassifier', 'XGBClassifier', 'LogisticRegression', 'RandomForestClassifier'}. 
<br/>You can also add other compination of methods to [app.config.json] file following the instances.
