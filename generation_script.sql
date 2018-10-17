Create table S(

S char(4) primary key,
SNAME char(16),
STATUS int,
CITY char(16)


);

.separator ";"
.import data\\importers.txt S


Create table P(

P char(4) primary key,
PNAME char(16),
COLOR char(16),
WEIGHT int,
CITY char(16)

);

.separator ";"
.import data\\products.txt P


Create table J(

J char(4) primary key,
JNAME char(16),
CITY char(16)


);

.separator ";"
.import data\\projects.txt J


Create table SPJ(

S char(4),
P char(4),
J char(4),

QTY int,


CONSTRAINT SColumn
    FOREIGN KEY (S)
    REFERENCES S (S)
	
	
CONSTRAINT PColumn
    FOREIGN KEY (P)
    REFERENCES P (P)
	
	
CONSTRAINT JColumn
    FOREIGN KEY (J)
    REFERENCES J (J)

);

.separator ";"
.import data\\SPJ.txt SPJ

.headers on
.mode column
.quit 
