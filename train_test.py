import pandas as pd
import numpy as np
import sys
from pathlib import Path
import subprocess
from sklearn.model_selection import train_test_split

#SCRIPT ARGS
if len(sys.argv) < 2 :
    print(f"not enough args: {sys.argv}")
    exit()

train_time = sys.argv[1]
test_percentage = float(sys.argv[2])/100

repopath = Path(__file__).parent
model_train_script = fr"{repopath}\model_train\bin\Release\net6.0\publish\model_train.exe"
model_test_script = fr"{repopath}\model_test\bin\Release\net6.0\publish\model_test.exe"
data_file = fr"{repopath}\USA_Housing.csv"
training_file = fr"{repopath}\training_set.csv"
testing_file = fr"{repopath}\testing_set.csv"

# Prepare data
data = pd.read_csv(data_file)
train, test = train_test_split(data, test_size=test_percentage)
train.to_csv(training_file, index = False)
test.to_csv(testing_file, index = False) 

# TRAIN MODEL
subprocess.call([model_train_script, training_file, train_time])      

#TEST MODEL
# subprocess.call([model_test_script, testing_file]) 

# # #GET OUTPUT
# # output_fp = test_data_fp.replace(".csv", f"_predictions_{model_id}.csv")
# # stats = procD.analyse_data(output_fp)
# # procD.add_log_data(output_fp, stats, log_file, model_id)