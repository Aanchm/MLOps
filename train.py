import pandas as pd
import numpy as np
import sys
from pathlib import Path
import subprocess

repopath = Path(__file__).parent
model_train_script = fr"{repopath}\model_train\bin\Release\net6.0\publish\model_train.exe"
model_test_script = fr"{repopath}\model_test\bin\Release\net6.0\publish\model_test.exe"
train_file = fr"{repopath}\taxi-fare-train.csv" 
test_file = fr"{repopath}\taxi-fare-test.csv" 

#SCRIPT ARGS
if len(sys.argv) < 2 :
    print(f"not enough args: {sys.argv}")
    exit()

train_time = sys.argv[1]

# TRAIN MODEL
subprocess.call([model_train_script, train_file, train_time])      

#TEST MODEL
subprocess.call([model_test_script, test_file]) 

# #GET OUTPUT
# output_fp = test_data_fp.replace(".csv", f"_predictions_{model_id}.csv")
# stats = procD.analyse_data(output_fp)
# procD.add_log_data(output_fp, stats, log_file, model_id)