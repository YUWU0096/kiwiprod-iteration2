CREATE TABLE English_Profeciency(
   id     INTEGER  NOT NULL PRIMARY KEY 
  ,degree VARCHAR(24) NOT NULL
  ,value  INTEGER  NOT NULL
);
INSERT INTO English_Profeciency(id,degree,value) VALUES (0,'Do not speak English',0);
INSERT INTO English_Profeciency(id,degree,value) VALUES (1,'Speaks English poorly',1);
INSERT INTO English_Profeciency(id,degree,value) VALUES (2,'Speaks English Well',2);
INSERT INTO English_Profeciency(id,degree,value) VALUES (3,'Speaks English Very Well',3);
INSERT INTO English_Profeciency(id,degree,value) VALUES (4,'Speaks English Only',4);
