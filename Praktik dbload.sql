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

CREATE TABLE Day
(
Id int IDENTITY(1,1) primary key,
TypeName varchar(25) not null
)

CREATE TABLE DayActive
(
Id int IDENTITY(1,1) primary key,
Period int foreign key references Period(Id),
Day int foreign key references Day(Id),
Number int,
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
Number int
)

CREATE TABLE Formulas
(
Id int IDENTITY(1,1) primary key,
TypeName varchar(25) not null
)

CREATE TABLE FormulasActive
(
Id int IDENTITY(1,1) primary key,
Period int foreign key references Period(Id),
Formulas int foreign key references Formulas(Id),
Number int
)

CREATE TABLE Formula
(
Id int IDENTITY(1,1) primary key,
Formulas int foreign key references Formulas(Id),
Name varchar(25) not null,
Formula varchar(50)
)

CREATE TABLE Parameter
(
Id int IDENTITY(1,1) primary key,
Parameter varchar(50) not null
)

CREATE TABLE FormulaParameter
(
Id int IDENTITY(1,1) primary key,
Formula int foreign key references Formula(Id),
Parameter int foreign key references Parameter(Id)
)

GO