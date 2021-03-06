--USE DATABASE praktik_estimate

DROP TABLE dayPeriod
DROP TABLE estimatePeriod
DROP TABLE formulaPeriode
DROP TABLE examPeriod
DROP TABLE meetingVariable
DROP TABLE meeting
DROP TABLE dayActivities
DROP TABLE estimateActivities
DROP TABLE formulaActivities
DROP TABLE examActivities
DROP TABLE period
DROP TABLE person

GO
CREATE TABLE person
(
initials VARCHAR(4) PRIMARY KEY,
password VARCHAR(15) NOT NULL,
firstname VARCHAR(15),
lastname VARCHAR(15)
)
CREATE TABLE period
(
periodId INT IDENTITY(1,1) PRIMARY KEY,
person VARCHAR(4) FOREIGN KEY REFERENCES person(initials) NOT NULL,
startdate DATE NOT NULL,
enddate DATE NOT NULL,
nettoHours FLOAT
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
CREATE TABLE dayActivities
(
activity VARCHAR(50) PRIMARY KEY
)
CREATE TABLE estimateActivities
(
activity VARCHAR(50) PRIMARY KEY
)
CREATE TABLE formulaActivities
(
activity VARCHAR(50) PRIMARY KEY,
formulamultiplier FLOAT NOT NULL,
)
CREATE TABLE examActivities
(
name VARCHAR(50) PRIMARY KEY,
studentsmultiplier FLOAT NOT NULL,
projectsmultiplier FLOAT NOT NULL,
daysmultiplier FLOAT NOT NULL
)
CREATE TABLE dayPeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
dayActivity VARCHAR(50) FOREIGN KEY REFERENCES dayActivities(activity) NOT NULL,
daysUsed INT  NOT NULL,
PRIMARY KEY (period, dayActivity) 
)
CREATE TABLE estimatePeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
estimateActivity VARCHAR(50) FOREIGN KEY REFERENCES estimateActivities(activity) NOT NULL,
hoursUsed FLOAT  NOT NULL,
PRIMARY KEY (period, estimateActivity) 
)
CREATE TABLE formulaPeriode
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
formulaActivity VARCHAR(50) FOREIGN KEY REFERENCES formulaActivities(activity) NOT NULL,
variable FLOAT NOT NULL,
PRIMARY KEY (period, formulaActivity) 
)
CREATE TABLE examPeriod
(
period INT FOREIGN KEY REFERENCES period(periodid) NOT NULL,
examActivity VARCHAR(50) FOREIGN KEY REFERENCES examActivities(name) NOT NULL,
students FLOAT NOT NULL,
projekts FLOAT NOT NULL,
daysUsed FLOAT NOT NULL,
PRIMARY KEY (period, examActivity) 
)

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
SET  @estimatedHours = @workdays * (SELECT value FROM meetingVariable WHERE name = 'workHours') * (SELECT value/100 FROM meetingVariable WHERE name = 'percentage')

INSERT INTO meeting VALUES(@period, @estimatedHours)

END
GO
BEGIN TRY
    BEGIN TRANSACTION
		-- vigtige system variabler
		INSERT INTO meetingVariable VALUES ('percentage',9)
		INSERT INTO meetingVariable VALUES ('workHours',7.4)
		
		-- test data
    	INSERT INTO person VALUES('test', 'test1', 'Tester', 'McTest')
		INSERT INTO person VALUES('TK', 'fisk1', 'Torben', 'Krøjmand')
		INSERT INTO person VALUES('SM', 'fisk2', 'Søren', 'Madsen')
		INSERT INTO person VALUES('KR', 'fisk3', 'Karsten', 'Rasmussen')
		
		INSERT INTO period VALUES('test','20140801','20140901', 355.8)
		INSERT INTO period VALUES('test','20140901','20141001', null)
		INSERT INTO period VALUES('TK','20140101','20140731', null)
		INSERT INTO period VALUES('SM','20140101','20140731', null)
		INSERT INTO period VALUES('SM','20140801','20141231', null)
		INSERT INTO period VALUES('KR','20140801','20141231', null)
		
		INSERT INTO dayActivities VALUES('Illness')
		INSERT INTO dayActivities VALUES('Vacation')
		INSERT INTO dayActivities VALUES('Holiday')
		INSERT INTO dayActivities VALUES('Absent')
		
		INSERT INTO estimateActivities VALUES('Booklisting')
		INSERT INTO estimateActivities VALUES('Office')
		
		INSERT INTO formulaActivities VALUES('Classes, Datamatiker', 3)
		INSERT INTO formulaActivities VALUES('Classes, Datamatiker, English', 5)
		INSERT INTO formulaActivities VALUES('Classes, Multimediedesign', 2.3)
		INSERT INTO formulaActivities VALUES('Classes, Multimediedesign, English', 3.3)
		INSERT INTO formulaActivities VALUES('Olc', 0.4)
		INSERT INTO formulaActivities VALUES('HOP', 2.63)
		INSERT INTO formulaActivities VALUES('Praktik', 1.98)
		INSERT INTO formulaActivities VALUES('HOP, English', 3.54)
		INSERT INTO formulaActivities VALUES('Olc, English', 0.5)
		INSERT INTO formulaActivities VALUES('Praktik, English', 2.87)
		
		INSERT INTO examActivities VALUES('2. Semester Datamatiker', 0.66, 4,1.33)
		INSERT INTO examActivities VALUES('3. Semester Datamatiker', 0.66, 0,2.5)		
		
		INSERT INTO dayPeriod VALUES(1,'Illness',3)
		INSERT INTO dayPeriod VALUES(1,'Vacation',3)
		INSERT INTO dayPeriod VALUES(1,'Holiday',3)
		INSERT INTO dayPeriod VALUES(2,'Illness',0)
		INSERT INTO dayPeriod VALUES(2,'Vacation',0)
		INSERT INTO dayPeriod VALUES(2,'Holiday',0)
		
		INSERT INTO estimatePeriod VALUES(1,'Booklisting',3)
		INSERT INTO estimatePeriod VALUES(1,'Office',5)
		INSERT INTO estimatePeriod VALUES(2,'Booklisting',8)
		INSERT INTO estimatePeriod VALUES(2,'Office',16)
		
		INSERT INTO formulaPeriode VALUES(1,'Classes, Datamatiker',25)
		INSERT INTO formulaPeriode VALUES(1,'Classes, Datamatiker, English',4)
		INSERT INTO formulaPeriode VALUES(1,'Olc',0)
		INSERT INTO formulaPeriode VALUES(2,'Classes, Datamatiker',25)
		INSERT INTO formulaPeriode VALUES(2,'Classes, Datamatiker, English',4)
		INSERT INTO formulaPeriode VALUES(2,'Olc',3)
				
		INSERT INTO examperiod VALUES(1,'2. Semester Datamatiker',25,6,3)
		INSERT INTO examperiod VALUES(1,'3. Semester Datamatiker',16,4,0)
		INSERT INTO examperiod VALUES(2,'2. Semester Datamatiker',0,0,0)
		INSERT INTO examperiod VALUES(2,'3. Semester Datamatiker',0,0,0)
    COMMIT
END TRY
BEGIN CATCH
 ROLLBACK
END CATCH





