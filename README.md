# MLOps

## Introduction
This repo is taken from a project from October 2022 to practice using ML.NET and scripting a MLOps Toolchain. The project uses data from https://github.com/huzaifsayed/Linear-Regression-Model-for-House-Price-Prediction/blob/master/USA_Housing.csv to predict house prices. 

## Flow
A python script is called with the following arguments: 
train_time: time to train the model for
test_percentage: percentage of the dataset to separate for testing

1. The python script creates the train and test data sets
2. The python script runs the C# script which trains the model 
3. The pythons script runs the C# script which tests the model 


# TODO
- Generate statistics for the tested model
