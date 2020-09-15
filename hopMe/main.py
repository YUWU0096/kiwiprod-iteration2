import itertools

from functions import *
# Classifier
from sklearn.svm import SVC
# export sklearn model
from skl2onnx import convert_sklearn
from skl2onnx.common.data_types import FloatTensorType
from skl2onnx.common.data_types import Int32TensorType
from constants import *

import pandas as pd

# disable false-positive warnings regarding pandas column selection
pd.options.mode.chained_assignment = None  # default='warn'

if __name__ == '__main__':

    # export index to value as database
    highest_education_index = pd.concat([pd.DataFrame(heap_index_values.keys()), pd.DataFrame(heap_index_values.values())], axis=1)
    highest_education_index.columns = ["degree", "value"]
    highest_education_index.to_csv("highest_education_index.csv", index=True, index_label='id')

    english_profeciency_index = pd.concat([pd.DataFrame(english_index_values.keys()), pd.DataFrame(english_index_values.values())], axis=1)
    english_profeciency_index.columns = ["degree", "value"]
    english_profeciency_index.to_csv("english_profeciency_index.csv", index=True, index_label='id')

    ## load the definition of OCCP from abs.gov.au
    # load from abs.gov.au
    occp_anzsco_df = pd.read_excel(
        "https://www.abs.gov.au/ausstats/subscriber.nsf/log?openagent&1220.0%20ANZSCO%20Version%201.2%20Structure%20v3.xls&1220.0&Data%20Cubes"
        "&6A8A6C9AC322D9ABCA257B9E0011956C&0&2013,%20Version%201.2&05.07.2013&Latest",
        sheet_name="Table 6").iloc[list(range(6, 1372, 1)), :].dropna().reset_index(drop=True)
    # load local file
    # occp_anzsco_df = pd.read_excel("1220.0 ANZSCO Version 1.2 Structure v3.xls", sheet_name="Table 6").iloc[list(range(5, 1372, 1)), : ].dropna().reset_index(drop=True)
    ## reformat the occp anzsco dataframe
    occp_anzsco_df.columns = ['code', 'name']
    occp_anzsco_dict = occp_anzsco_df.set_index('code').to_dict()['name']

    # occp_anzsco_df = pd.read_excel("1220.0 ANZSCO Version 1.2 Structure v3.xls", sheet_name="Table 6").iloc[list(range(5, 1372, 1)), :] \
    #     .dropna().reset_index(drop=True)
    # reformat anzsco dictionary
    occp_anzsco_df.columns = ['code', 'name']
    occp_anzsco_dict = occp_anzsco_df.set_index('code').to_dict()['name']

    # Data Exploration & Wrangling
    # Extract features and transform to usable format
    # load microdata for processing
    # from data.gov.au
    au_micro = pd.read_csv(
        "https://www.abs.gov.au/AUSSTATS/subscriber.nsf/log?openagent&acmid16_testfile.csv&3417.0.55.001&Data%20Cubes&7157AA0BF63DA7FECA2584D5001C5AFB&0&2016"
        "&20.12.2019&Latest",
        dtype=object)
    # au_micro = pd.read_csv("acmid16_testfile.csv", dtype=object)
    print("Processing ")

    # extract labels for machine training
    au_micro = au_micro[["AGEP", "ENGLP", "HEAP", "HSCP", "LFSP", "INDP", "OCCP", "SEXP"]]

    # labour force status LFSP, 6 and X stands for not in labour force and not stated, which will not be used
    # ultimately, someone who is actively not looking for employment, is not employed
    au_micro = get_labour_force(au_micro)

    # label gender
    au_micro['gender'] = get_gender_info(au_micro)

    # we cannot infer user gender, therefore the entries must be removed if there are any
    if not au_micro["SEXP"].isnull().values.any():
        au_micro.dropna(subset=['SEXP'])

    # convert age to integer
    au_micro["age"] = au_micro["AGEP"].apply(lambda x: int(x) if x.isnumeric() else x)
    au_micro = au_micro.loc[au_micro["age"] >= 18]  # only for age 18+
    au_micro["age_group_10y"] = au_micro["age"].apply(lambda x: int(x) // 10)

    # english speaking ability has 1 - 6, with 6 and X being not stated and missing
    # cannot infer language proficiency, therefore remove ENGLP values of 6 and X
    au_micro = au_micro.loc[(au_micro["ENGLP"] != "6") & (au_micro["ENGLP"] != "X")]

    # and convert to text
    au_micro["english_proficiency"] = get_english_proficiency(au_micro)

    # convert education code to text
    # education attainment cannot be inferred
    au_micro['highest_education'] = get_highest_education_level(au_micro)
    au_micro = au_micro[au_micro['highest_education'].notnull()]
    # explicitly convert to int
    au_micro.highest_education = au_micro.highest_education.astype(int)

    # convert employement status to employed or not
    au_micro["is_employed"] = get_employment_status(au_micro)
    au_micro = au_micro[au_micro["is_employed"] == True]

    # convert occupation code to text
    # remove illegal values
    au_micro = au_micro.loc[(au_micro.OCCP.notna()) & (au_micro.OCCP != "XXXXXX")]
    au_micro.OCCP = au_micro.OCCP.apply(lambda x: int(x))
    au_micro = au_micro.loc[au_micro.OCCP.isin(occp_anzsco_dict.keys())]
    print(occp_anzsco_dict)
    au_micro['occupation'] = au_micro.OCCP.replace(occp_anzsco_dict)

    # select needed labels only
    au_micro = au_micro[["gender", "age_group_10y", "english_proficiency", "highest_education",
                         "occupation"]].reset_index(drop=True)

    print("Data wrangling complete!")

    print("Training model with processed microdata...")
    # get best classifier and train using all data
    ml_model = SVC(kernel="linear", C=0.025, probability=True, decision_function_shape='ovo')
    ml_model.fit(au_micro.loc[:, au_micro.columns != 'occupation'], au_micro.occupation)

    # export model in ONNX
    ml_onnx = convert_sklearn(ml_model, initial_types=[('int_input', Int32TensorType([None, 4]))])
    with open("onnxModels/refugee_employment.onnx", "wb+") as f:
        f.write(ml_onnx.SerializeToString())

    print("ONNX model exported!")

    # for work around with ONNX model issues
    # use a database with all input permutations and corresponding outputs
    # create a dataframe of all permutations
    au_inputs = pd.DataFrame(list(itertools.product(*[au_micro.gender.unique(),
                                                      au_micro.age_group_10y.unique(),
                                                      au_micro.english_proficiency.unique(),
                                                      range(0,26)])),
                             columns=["gender", "age_group_10y", "english_proficiency", "highest_education"])
    # assign outputs to inputs
    # the variables are gender, age_group_10y, english_proficiency, highest_education
    list_pred = topn_predictions(ml_model, au_inputs, n=5)
    all_outputs = pd.DataFrame(list(map(np.ravel, list_pred)), columns=['pred_1', 'pred_2', 'pred_3', 'pred_4', 'pred_5'])
    # export the results to be used in web backend as a database
    input_predict = pd.concat([au_inputs, all_outputs], axis=1)
    input_predict.to_csv('predications.csv', index=True, index_label='id')
    print("Work around database file exported!")