--CREATE DATABASE praktik_estimate


--DROP TABLE FormulaParameter
--DROP TABLE Parameter
--DROP TABLE Formula
--DROP TABLE FormulasActive
--DROP TABLE Formulas
--DROP TABLE EstimateActive
--DROP TABLE Estimate
--DROP TABLE DayActive
--DROP TABLE Day
--DROP TABLE Period
--DROP TABLE Person


IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.FormulaParameter') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.FormulaParameter

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.Parameter') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.Parameter

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.Formula') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.Formula

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.FormulasActive') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.FormulasActive

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.Formulas') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.Formulas

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.EstimateActive') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.EstimateActive

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.Estimate') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.Estimate

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.DayActive') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.DayActive

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.Day') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.Day

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.Period') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.Period

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'praktik_estimate.Person') AND TYPE IN (N'U'))
DROP TABLE praktik_estimate.Person



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
StartDate datetime not null,
EndDate datetime not null
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
Id int IDENTITY(1,1) primary key,
Name varchar(25) not null,
Formula varchar(50)
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
Id int IDENTITY(1,1) primary key,
Name varchar(25) not null,
Education int foreign key references Education(Id),
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

INSERT INTO Period values(1, '20140801', '20141231')
INSERT INTO Period values(2, '20140801', '20141231')
INSERT INTO Period values(3, '20140801', '20141231')
INSERT INTO Period values(2, '20140801', '20141231')

INSERT INTO DayTable values('Vacation')
INSERT INTO DayTable values('Holiday')
INSERT INTO DayTable values('Illness')
INSERT INTO DayTable values('Absent')

INSERT INTO Estimate values('Booklist')
INSERT INTO Estimate values('Other Estimates')

INSERT INTO DayActive values(1, 1, 20)
INSERT INTO DayActive values(1, 1, 6)
INSERT INTO DayActive values(1, 1, 3)

INSERT INTO EstimateActive values(1, 1, 9.2)
INSERT INTO EstimateActive values(2, 1, 8)

INSERT INTO Parameter values('uvtal', 1, 2.5)
INSERT INTO Parameter values('hoptal', 1, 5)
INSERT INTO Parameter values('divtal', 1, 0.75)
INSERT INTO Parameter values('olctal', 1, 1.33)
INSERT INTO Parameter values('praktiktal', 1, 0.66)

INSERT INTO Formula values('uv', 'AntalLektioner*uvtal')
INSERT INTO Formula values('hop', 'AntalGrupper*hoptal')
INSERT INTO Formula values('div', 'AntalTimer*divtal')
INSERT INTO Formula values('olc', 'AntalLektioner*olctal')
INSERT INTO Formula values('praktik', 'AntalPersoner*praktiktal')

INSERT INTO FormulaParameter values(1, 1)
INSERT INTO FormulaParameter values(2, 2)
INSERT INTO FormulaParameter values(3, 3)
INSERT INTO FormulaParameter values(4, 4)
INSERT INTO FormulaParameter values(5, 5)

INSERT INTO FormulasActive values (1, 2, 100)