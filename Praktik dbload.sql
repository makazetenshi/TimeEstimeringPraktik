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
