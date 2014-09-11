--CREATE DATABASE til_tests

DROP TABLE dayPeriod
DROP TABLE estimatePeriod
DROP TABLE formulaPeriode
DROP TABLE eksamPeriod
DROP TABLE meeting
DROP TABLE dayAktivities
DROP TABLE estimateAktivities
DROP TABLE formulaAktivities
DROP TABLE eksamnAktivities
DROP TABLE period
DROP TABLE person



GO
CREATE TABLE person
(
initials VARCHAR(4) PRIMARY KEY,
password VARCHAR(15) NOT NULL,
forname VARCHAR(15),
lastname VARCHAR(15)
)
CREATE TABLE period
(
periodId INT IDENTITY(1,1) PRIMARY KEY,
person VARCHAR(4) FOREIGN KEY REFERENCES person(initials) NOT NULL,
startdate DATE NOT NULL,
enddate DATE NOT NULL
)
CREATE TABLE meeting
(
period INT PRIMARY KEY FOREIGN KEY REFERENCES period(periodId),
estimatedHours FLOAT NOT NULL
)
CREATE TABLE dayAktivities
(
aktivity VARCHAR(20) PRIMARY KEY
)
CREATE TABLE estimateAktivities
(
aktivity VARCHAR(20) PRIMARY KEY
)
CREATE TABLE formulaAktivities
(
aktivity VARCHAR(20) PRIMARY KEY,
multiplyer FLOAT NOT NULL
)
CREATE TABLE eksamnAktivities
(
name VARCHAR(20) PRIMARY KEY,
multiplyer1 FLOAT NOT NULL,
multiplyer2 FLOAT NOT NULL,
multiplyer3 FLOAT NOT NULL
)
CREATE TABLE dayPeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
dayActivity VARCHAR(20) FOREIGN KEY REFERENCES dayAktivities(aktivity) NOT NULL,
daysUsed INT  NOT NULL,
PRIMARY KEY (period, dayActivity) 
)
CREATE TABLE estimatePeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
estimateActivity VARCHAR(20) FOREIGN KEY REFERENCES estimateAktivities(aktivity) NOT NULL,
hoursUsed FLOAT  NOT NULL,
PRIMARY KEY (period, estimateActivity) 
)
CREATE TABLE formulaPeriode
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
formulaActivity VARCHAR(20) FOREIGN KEY REFERENCES formulaAktivities(aktivity) NOT NULL,
variable FLOAT NOT NULL,
PRIMARY KEY (period, formulaActivity) 
)
CREATE TABLE eksamPeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
eksamnActivity VARCHAR(20) FOREIGN KEY REFERENCES eksamnAktivities(name) NOT NULL,
students FLOAT NOT NULL,
projekts FLOAT NOT NULL,
daysUsed FLOAT NOT NULL,
PRIMARY KEY (period, eksamnActivity) 
)
GO
DROP TRIGGER meetingCalculater
GO
CREATE TRIGGER meetingCalculater 
ON period 
AFTER INSERT
AS
BEGIN
DECLARE @workdays INT
DECLARE @period INT
DECLARE @estimatedHours FLOAT
DECLARE @start VARCHAR(24)
DECLARE @end VARCHAR(24)

SET @start = (SELECT startDate FROM inserted)
SET @end = (SELECT endDate FROM inserted)
SET @period = (SELECT periodId FROM inserted)
SET @workdays = (Select
   (DATEDIFF(dd, @start, @end) + 1)
  -(DATEDIFF(wk, @start, @end) * 2)
  -(CASE WHEN DATENAME(dw, @start) = 'Sunday' THEN 1 ELSE 0 END)
  -(CASE WHEN DATENAME(dw, @end) = 'Saturday' THEN 1 ELSE 0 END))
  -- Vi antager at en arbejds dag er 7.4 time og at møde procenten ikke ændres fra 9%
SET  @estimatedHours = @workdays * 7.4 * 0.09

INSERT INTO meeting VALUES(@period, @estimatedHours)

END
GO