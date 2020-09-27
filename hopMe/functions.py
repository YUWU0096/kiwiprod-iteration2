import constants
import numpy as np


# returns a dataframe of entries active in labour force
def get_labour_force(emp_df):
    return emp_df.loc[(emp_df["LFSP"] != "6") & (emp_df["LFSP"] != "X")]


# get gender of entries
def get_gender_info(emp_def):
    return emp_def["SEXP"].replace({"1": 1, "2": 2})


# get english proficiency strings
def get_english_proficiency(emp_df):
    emp_df["ENGLP"].replace(constants.english_values, inplace=True)
    return emp_df["ENGLP"].replace(constants.english_index_values)


# get highest education attainment
def get_highest_education_level(emp_df):
    # select only values with corresponding education attainments
    # rows with inadequate describtion, not stated and values not in the above list
    # which is defined in the associated document: https://www.abs.gov.au/ausstats/abs@.nsf/Lookup/2901.0Chapter4402016
    # will be removed
    # fill na values in HEAP with values values from HSCP
    emp_df.HSCP.replace(constants.hscp_values, inplace=True)
    emp_df.HEAP = emp_df.apply(lambda x: x.HSCP if (x.HEAP == np.nan) else x.HEAP, axis=1)
    emp_df.replace('X', '001', inplace=True)
    # replace code with text
    emp_df = emp_df[emp_df["HEAP"].isin(constants.heap_values.keys())]
    emp_df["HEAP"].replace(constants.heap_values, inplace=True)
    return emp_df["HEAP"].replace(constants.heap_index_values)


# get employed status
def get_employment_status(emp_df):
    # an individual is not employed if they are not in an INDP or if they have LFSP values of 4, 5, 6 or X
    emp_df["INDP"] = emp_df["INDP"].replace(np.nan, 'XXXX')  # can't deal with NaNs
    return emp_df.apply(
        lambda x: int(not ((x.LFSP is np.nan or x.LFSP in ["4", "5", "6", "X"])
                           and (x.INDP is np.nan or x.INDP == 'XXXX'))), axis=1
    )


# Function for return top N predictions
def topn_predictions(ml, df, n=5):
    predictions = ml.predict_proba(df)
    return [ml.classes_[x] for x in np.argsort(predictions, axis=1)[:, :5]]