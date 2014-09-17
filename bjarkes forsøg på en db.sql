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
CREATE TABLE meetingVariable
(
name VARCHAR(15) PRIMARY KEY,
value FLOAT
)
INSERT INTO meetingVariable VALUES ('percentage',9)
INSERT INTO meetingVariable VALUES ('workHours',7.4)
CREATE TABLE dayAktivities
(
aktivity VARCHAR(50) PRIMARY KEY
)
CREATE TABLE estimateAktivities
(
aktivity VARCHAR(50) PRIMARY KEY
)
CREATE TABLE formulaAktivities
(
aktivity VARCHAR(50) PRIMARY KEY,
multiplyer FLOAT NOT NULL,
)
CREATE TABLE eksamnAktivities
(
name VARCHAR(50) PRIMARY KEY,
multiplyer1 FLOAT NOT NULL,
multiplyer2 FLOAT NOT NULL,
multiplyer3 FLOAT NOT NULL
)
CREATE TABLE dayPeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
dayActivity VARCHAR(50) FOREIGN KEY REFERENCES dayAktivities(aktivity) NOT NULL,
daysUsed INT  NOT NULL,
PRIMARY KEY (period, dayActivity) 
)
CREATE TABLE estimatePeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
estimateActivity VARCHAR(50) FOREIGN KEY REFERENCES estimateAktivities(aktivity) NOT NULL,
hoursUsed FLOAT  NOT NULL,
PRIMARY KEY (period, estimateActivity) 
)
CREATE TABLE formulaPeriode
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
formulaActivity VARCHAR(50) FOREIGN KEY REFERENCES formulaAktivities(aktivity) NOT NULL,
variable FLOAT NOT NULL,
PRIMARY KEY (period, formulaActivity) 
)
CREATE TABLE eksamPeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
eksamnActivity VARCHAR(50) FOREIGN KEY REFERENCES eksamnAktivities(name) NOT NULL,
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
DECLARE @start VARCHAR(50)
DECLARE @end VARCHAR(50)

SET @start = (SELECT startDate FROM inserted)
SET @end = (SELECT endDate FROM inserted)
SET @period = (SELECT periodId FROM inserted)
SET @workdays = (Select
   (DATEDIFF(dd, @start, @end) + 1)
  -(DATEDIFF(wk, @start, @end) * 2)
  -(CASE WHEN DATENAME(dw, @start) = 'Sunday' THEN 1 ELSE 0 END)
  -(CASE WHEN DATENAME(dw, @end) = 'Saturday' THEN 1 ELSE 0 END))
  -- Vi antager at en arbejds dag er 7.4
SET  @estimatedHours = @workdays * (SELECT workingHours FROM meetingVariable) * (SELECT percentage/100 FROM meetingVariable)

INSERT INTO meeting VALUES(@period, @estimatedHours)

END
GO
BEGIN TRY
    BEGIN TRANSACTION
    	INSERT INTO person VALUES('test', 'test1', 'Tester', 'McTest')
		
		INSERT INTO period VALUES('test','20140801','20140901')
		INSERT INTO period VALUES('test','20140901','20141001')
		
		INSERT INTO dayAktivities VALUES('Illness')
		INSERT INTO dayAktivities VALUES('Vacation')
		INSERT INTO dayAktivities VALUES('Holiday')
		
		INSERT INTO estimateAktivities VALUES('Bookkeeping')
		INSERT INTO estimateAktivities VALUES('Office')
		
		INSERT INTO formulaAktivities VALUES('Classes, Datamat', 3)
		INSERT INTO formulaAktivities VALUES('Classes, Datamatiker, English', 5)
		INSERT INTO formulaAktivities VALUES('Olc', 0.4)
		
		INSERT INTO eksamnAktivities VALUES('2. SEMESTER DATA', 0.66, 4,1.33)
		INSERT INTO eksamnAktivities VALUES('3. SEMESTER DATA', 0.66, 0,2.5)		
		
		INSERT INTO dayPeriod VALUES(1,'Illness',3)
		INSERT INTO dayPeriod VALUES(1,'Vacation',3)
		INSERT INTO dayPeriod VALUES(1,'Holiday',3)
		INSERT INTO dayPeriod VALUES(2,'Illness',0)
		INSERT INTO dayPeriod VALUES(2,'Vacation',0)
		INSERT INTO dayPeriod VALUES(2,'Holiday',0)
		
		INSERT INTO estimatePeriod VALUES(1,'Bookkeeping',3)
		INSERT INTO estimatePeriod VALUES(1,'Office',5)
		INSERT INTO estimatePeriod VALUES(2,'Bookkeeping',8)
		INSERT INTO estimatePeriod VALUES(2,'Office',16)
		
		INSERT INTO formulaPeriode VALUES(1,'Classes, Datamat',25)
		INSERT INTO formulaPeriode VALUES(1,'Classes, Datamatiker, English',4)
		INSERT INTO formulaPeriode VALUES(1,'Olc',0)
		INSERT INTO formulaPeriode VALUES(2,'Classes, Datamat',25)
		INSERT INTO formulaPeriode VALUES(2,'Classes, Datamatiker, English',4)
		INSERT INTO formulaPeriode VALUES(2,'Olc',3)
				
		INSERT INTO eksamPeriod VALUES(1,'2. SEMESTER DATA',25,6,3)
		INSERT INTO eksamPeriod VALUES(1,'3. SEMESTER DATA',16,4,0)
		INSERT INTO eksamPeriod VALUES(2,'2. SEMESTER DATA',0,0,0)
		INSERT INTO eksamPeriod VALUES(2,'3. SEMESTER DATA',0,0,0)
    COMMIT
END TRY
BEGIN CATCH
 ROLLBACK
END CATCH

