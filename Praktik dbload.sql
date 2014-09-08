--CREATE DATABASE praktik_estimate

DROP TABLE FormulaParameter
DROP TABLE Parameter
DROP TABLE FormulasActive
DROP TABLE Formula
DROP TABLE EstimateActive
DROP TABLE Estimate
DROP TABLE DayActive
DROP TABLE DayTable
DROP TABLE Period
DROP TABLE Person
DROP TABLE Education

GO

CREATE TABLE Person
(
Id int IDENTITY(1,1) primary key,
Name varchar(30) not null,
Pass varchar(10) not null,
Init varchar(4) not null unique,
)

CREATE TABLE Period
(
Id int IDENTITY(1,1) primary key,
Person int foreign key references Person(Id),
StartDate date not null,
EndDate date not null
)

CREATE TABLE DayTable
(
Id int IDENTITY(1,1) primary key,
TypeName varchar(25) not null
)

CREATE TABLE DayActive
(
Id int IDENTITY(1,1) primary key,
Period int foreign key references Period(Id),
DayTable int foreign key references DayTable(Id),
Number float,
)

CREATE TABLE Estimate
(
Id int IDENTITY(1,1) primary key,
TypeName varchar(25) not null
)

CREATE TABLE EstimateActive
(
Id int IDENTITY(1,1) primary key,
Period int foreign key references Period(Id),
Estimate int foreign key references Estimate(Id),
Number float
)

CREATE TABLE Formula
(
Id int identity(1,1) primary key,
Name varchar(255) not null,
)

CREATE TABLE FormulasActive
(
Id int IDENTITY(1,1) primary key,
Period int foreign key references Period(Id),
Formula int foreign key references Formula(Id),
Number float
)

CREATE TABLE Education
(
Id int IDENTITY(1,1) primary key,
Name varchar(25)
)

CREATE TABLE Parameter
(
Id int identity(1,1) primary key,
Name varchar(255) not null,
Education int foreign key references Education(Id),
Formula int foreign key references Formula(Id),
Parameter float not null
)

CREATE TABLE FormulaParameter
(
Id int IDENTITY(1,1) primary key,
Formula int foreign key references Formula(Id),
Parameter int foreign key references Parameter(Id)
)

GO

INSERT INTO Education values ('Datamatiker')
INSERT INTO Education values ('IT Teknolog')
INSERT INTO Education values ('Webudvikling')
INSERT INTO Education values ('Multimediedesign')

INSERT INTO Person values('Torben Krøjmand', 'fisk123', 'TK')
INSERT INTO Person values('Søren Madsen', 'jens', 'SM')
INSERT INTO Person values('Erik Jacobsen', 'kosteskab', 'EJ')
INSERT INTO Person values('Tester McTest', 'test1', 'test')

INSERT INTO Period values(1, '20140801', '20141231')
INSERT INTO Period values(2, '20140801', '20141231')
INSERT INTO Period values(3, '20140801', '20141231')
INSERT INTO Period values(2, '20140801', '20141231')
INSERT INTO Period values(4, '20140801', '20141231')
INSERT INTO Period values(4, '20140101', '20140831')

INSERT INTO DayTable values('Vacation')
INSERT INTO DayTable values('Holiday')
INSERT INTO DayTable values('Illness')
INSERT INTO DayTable values('Absent')

INSERT INTO Estimate values('Booklist')
INSERT INTO Estimate values('Other Estimates')

INSERT INTO DayActive values(1, 1, 20)
INSERT INTO DayActive values(1, 1, 6)
INSERT INTO DayActive values(1, 1, 3)
INSERT INTO DayActive values(5, 2, 15)
INSERT INTO DayActive values(5, 1, 3)
INSERT INTO DayActive values(5, 3, 2)
INSERT INTO DayActive values(5, 4, 1)
INSERT INTO DayActive values(6, 1, 30)
INSERT INTO DayActive values(6, 4, 8)

INSERT INTO EstimateActive values(1, 1, 9.2)
INSERT INTO EstimateActive values(2, 1, 8)
INSERT INTO EstimateActive values(5, 1, 14)
INSERT INTO EstimateActive values(5, 2, 4)
INSERT INTO EstimateActive values(6, 1, 11)

INSERT INTO Formula values('Undervisning')
INSERT INTO Formula values('Hovedopgave')
INSERT INTO Formula values('Møder og Diverse')
INSERT INTO Formula values('OLC')
INSERT INTO Formula values('Praktik')
INSERT INTO Formula values('2. Semester Project')
INSERT INTO Formula values('SUM Project')
INSERT INTO Formula values('Specialisering')
INSERT INTO Formula values('3. Semester Project')

INSERT INTO Parameter values('uv', 1, 1, 2.5)
INSERT INTO Parameter values('hop', 1, 2, 3.1)
INSERT INTO Parameter values('div', 1, 3,  0.9)
INSERT INTO Parameter values('olc', 1, 4,  1.33)
INSERT INTO Parameter values('praktik', 1, 5, 0.8)
INSERT INTO Parameter values('studerende', 1, 6, 0.66)
INSERT INTO Parameter values('projekter', 1, 6, 4)
INSERT INTO Parameter values('dage', 1, 6, 1.33)
INSERT INTO Parameter values('projekter', 1, 7, 6.2)
INSERT INTO Parameter values('dage', 1, 7, 1.89)
INSERT INTO Parameter values('studerende', 1, 8, 5.3)
INSERT INTO Parameter values('studerende', 1, 9, 0.66)
INSERT INTO Parameter values('dage', 1, 9, 2.5)
INSERT INTO Parameter values('studerende', 2, 6, 0.75)
INSERT INTO Parameter values('projekter', 2, 6, 4.2)
INSERT INTO Parameter values('dage', 2, 6, 1.1)
INSERT INTO Parameter values('projekter', 2, 7, 4.2)
INSERT INTO Parameter values('dage', 2, 7, 1.77)
INSERT INTO Parameter values('studerende', 2, 8, 3)
INSERT INTO Parameter values('studerende', 2, 9, 0.5)
INSERT INTO Parameter values('dage', 2, 9, 2.87)

INSERT INTO FormulaParameter values(1,  1)
INSERT INTO FormulaParameter values(2,  2)
INSERT INTO FormulaParameter values(3,  3)
INSERT INTO FormulaParameter values(4,  4)
INSERT INTO FormulaParameter values(5,  5)
INSERT INTO FormulaParameter values(6,  6)
INSERT INTO FormulaParameter values(6,  7)
INSERT INTO FormulaParameter values(6,  8)
INSERT INTO FormulaParameter values(7,  9)
INSERT INTO FormulaParameter values(7, 10)
INSERT INTO FormulaParameter values(8, 11)
INSERT INTO FormulaParameter values(9, 12)
INSERT INTO FormulaParameter values(9, 13)
INSERT INTO FormulaParameter values(6, 14)
INSERT INTO FormulaParameter values(6, 15)
INSERT INTO FormulaParameter values(6, 16)
INSERT INTO FormulaParameter values(7, 17)
INSERT INTO FormulaParameter values(7, 18)
INSERT INTO FormulaParameter values(8, 19)
INSERT INTO FormulaParameter values(9, 20)
INSERT INTO FormulaParameter values(9, 21)

INSERT INTO FormulasActive values (5, 1, 213.33)
INSERT INTO FormulasActive values (5, 3, 13.2)
INSERT INTO FormulasActive values (5, 4, 55.1)
INSERT INTO FormulasActive values (5, 5, 32.45)
INSERT INTO FormulasActive values (5, 6, 42.61)
INSERT INTO FormulasActive values (5, 8, 22.93)
INSERT INTO FormulasActive values (6, 1, 172)
INSERT INTO FormulasActive values (6, 2, 64.76)
INSERT INTO FormulasActive values (6, 3, 8.4)
INSERT INTO FormulasActive values (6, 9, 100)