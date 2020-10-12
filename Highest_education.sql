CREATE TABLE Highest_education(
  id     INTEGER  NOT NULL PRIMARY KEY 
  ,degree VARCHAR(56) NOT NULL
  ,value  INTEGER  NOT NULL

);

INSERT INTO Highest_education(id,degree,value) VALUES (0,'Postgraduate Degree Level',24);
INSERT INTO Highest_education(id,degree,value) VALUES (1,'Doctoral Degree Level',23);
INSERT INTO Highest_education(id,degree,value) VALUES (2,'Master Degree Level',22);
INSERT INTO Highest_education(id,degree,value) VALUES (3,'Graduate Diploma and Graduate Certificate Level',21);
INSERT INTO Highest_education(id,degree,value) VALUES (4,'Graduate Diploma Level',20);
INSERT INTO Highest_education(id,degree,value) VALUES (5,'Graduate Certificate Level',19);
INSERT INTO Highest_education(id,degree,value) VALUES (6,'Bachelor Degree Level',18);
INSERT INTO Highest_education(id,degree,value) VALUES (7,'Advanced Diploma and Diploma Level',17);
INSERT INTO Highest_education(id,degree,value) VALUES (8,'Advanced Diploma and Associate Degree Level',16);
INSERT INTO Highest_education(id,degree,value) VALUES (9,'Diploma Level',15);
INSERT INTO Highest_education(id,degree,value) VALUES (10,'Certificate III & IV Level',14);
INSERT INTO Highest_education(id,degree,value) VALUES (11,'Certificate IV',13);
INSERT INTO Highest_education(id,degree,value) VALUES (12,'Certificate III',12);
INSERT INTO Highest_education(id,degree,value) VALUES (13,'Years 10 and above',11);
INSERT INTO Highest_education(id,degree,value) VALUES (14,'Year 12',10);
INSERT INTO Highest_education(id,degree,value) VALUES (15,'Year 11',9);
INSERT INTO Highest_education(id,degree,value) VALUES (16,'Year 10',8);
INSERT INTO Highest_education(id,degree,value) VALUES (17,'Certificate I & II Level Advanced Diploma and Diploma Level',7);
INSERT INTO Highest_education(id,degree,value) VALUES (18,'Certificate I & II Level',6);
INSERT INTO Highest_education(id,degree,value) VALUES (19,'Certificate II',5);
INSERT INTO Highest_education(id,degree,value) VALUES (20,'Certificate I',4);
INSERT INTO Highest_education(id,degree,value) VALUES (21,'Year 9',3);
INSERT INTO Highest_education(id,degree,value) VALUES (22,'Year 8 or below',2);
INSERT INTO Highest_education(id,degree,value) VALUES (23,'Did not go to school',1);
INSERT INTO Highest_education(id,degree,value) VALUES (24,'Unknown education background',0);