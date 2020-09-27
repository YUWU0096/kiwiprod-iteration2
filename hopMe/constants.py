import pandas as pd
# Constants extracted from 2016 census dictionary
# Highest level of education
heap_values = {
    "100": "Postgraduate Degree Level",
    "110": "Postgraduate Degree Level",
    "111": "Doctoral Degree Level",
    "120": "Master Degree Level",
    "200": "Graduate Diploma and Graduate Certificate Level",
    "211": "Graduate Diploma and Graduate Certificate Level",
    "221": "Graduate Diploma Level",
    "222": "Graduate Certificate Level",
    "300": "Bachelor Degree Level",
    "310": "Bachelor Degree Level",
    "400": "Advanced Diploma and Diploma Level",
    "411": "Advanced Diploma and Diploma Level",
    "413": "Advanced Diploma and Associate Degree Level",
    "421": "Diploma Level",
    "500": "Certificate III & IV Level",
    "511": "Certificate III & IV Level",
    "514": "Certificate IV",
    "521": "Certificate III",
    "600": "Years 10 and above",
    "611": "Year 12",
    "613": "Year 11",
    "621": "Year 10",
    "700": "Certificate I & II Level Advanced Diploma and Diploma Level",
    "720": "Certificate I & II Level",
    "721": "Certificate II",
    "724": "Certificate I",
    "800": "Year 9",
    "811": "Year 9",
    "812": "Year 8 or below",
    "998": "Did not go to school",
    "001": "Unknown education background"
}
# convert text to incremental values
heap_index_values = {
    "Postgraduate Degree Level": 24,
    "Doctoral Degree Level": 23,
    "Master Degree Level": 22,
    "Graduate Diploma and Graduate Certificate Level": 21,
    "Graduate Diploma Level": 20,
    "Graduate Certificate Level": 19,
    "Bachelor Degree Level": 18,
    "Advanced Diploma and Diploma Level": 17,
    "Advanced Diploma and Associate Degree Level": 16,
    "Diploma Level": 15,
    "Certificate III & IV Level": 14,
    "Certificate IV": 13,
    "Certificate III": 12,
    "Years 10 and above": 11,
    "Year 12": 10,
    "Year 11": 9,
    "Year 10": 8,
    "Certificate I & II Level Advanced Diploma and Diploma Level": 7,
    "Certificate I & II Level": 6,
    "Certificate II": 5,
    "Certificate I": 4,
    "Year 9": 3,
    "Year 8 or below": 2,
    "Did not go to school": 1,
    "Unknown education background": 0
}

# highest school year compeleted
# used to replace na values in heap
hscp_values = {
        '1': '611',
        '2': '613',
        '3': '621',
        '4': '811',
        '5': '812',
        '6': '998',
        'X': 'XXX'
}
# English proficiency
english_values = {
    "1": "Speaks English Only",
    "2": "Speaks English Very Well",
    "3": "Speaks English Well",
    "4": "Speaks English poorly",
    "5": "Do not speak English",
    "6": "Unknown English proficiency"
}
# text to numeric
english_index_values = {
    "Do not speak English": 0,
    "Speaks English poorly": 1,
    "Speaks English Well": 2,
    "Speaks English Very Well": 3,
    "Speaks English Only": 4
}
# field of study string to value
field_study_values = {
    'NO FIELD OF STUDY': 0,
    'NATURAL AND PHYSICAL SCIENCES': 1,
    'INFORMATION TECHNOLOGY': 2,
    'ENGINEERING AND RELATED TECHNOLOGIES': 3,
    'ARCHITECTURE AND BUILDING': 4,
    'AGRICULTURE, ENVIRONMENTAL AND RELATED STUDIES': 5,
    'HEALTH': 6,
    'EDUCATION': 7,
    'MANAGEMENT AND COMMERCE': 8,
    'SOCIETY AND CULTURE': 9,
    'CREATIVE ARTS': 10,
    'FOOD, HOSPITALITY AND PERSONAL SERVICES': 11,
    'MIXED FIELD PROGRAMMES': 12
}