--USE DATABASE praktik_estimate

DROP TABLE dayPeriod
DROP TABLE dayActivities
DROP TABLE estimatePeriod
DROP TABLE estimateActivities
DROP TABLE formulaPeriod
DROP TABLE formulaActivities
DROP TABLE examPeriod
DROP TABLE examActivities
DROP TABLE meeting
DROP TABLE meetingVariable
DROP TABLE period
DROP TABLE person

GO
CREATE TABLE person
(
initials VARCHAR(4) PRIMARY KEY ,
password VARCHAR(15) NOT NULL,
firstname VARCHAR(15) NOT NULL,
lastname VARCHAR(15) NOT NULL,
admin bit
)
CREATE TABLE period
(
periodId INT IDENTITY(1,1) PRIMARY KEY,
person VARCHAR(4) FOREIGN KEY REFERENCES person(initials) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
startdate DATE NOT NULL,
enddate DATE NOT NULL,
nettoHours FLOAT
)
CREATE TABLE meeting
(
period INT PRIMARY KEY FOREIGN KEY REFERENCES period(periodId) ON DELETE CASCADE ON UPDATE CASCADE,
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
period INT FOREIGN KEY REFERENCES period(periodid)  ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
dayActivity VARCHAR(50) FOREIGN KEY REFERENCES dayActivities(activity)  ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
daysUsed INT  NOT NULL,
PRIMARY KEY (period, dayActivity) 
)
CREATE TABLE estimatePeriod
(
period INT FOREIGN KEY REFERENCES period(periodid)  ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
estimateActivity VARCHAR(50) FOREIGN KEY REFERENCES estimateActivities(activity)  ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
hoursUsed FLOAT  NOT NULL,
PRIMARY KEY (period, estimateActivity) 
)
CREATE TABLE formulaPeriod
(
period INT FOREIGN KEY REFERENCES period(periodid)  ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
formulaActivity VARCHAR(50) FOREIGN KEY REFERENCES formulaActivities(activity)  ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
variable INT NOT NULL,
PRIMARY KEY (period, formulaActivity) 
)
CREATE TABLE examPeriod
(
period INT FOREIGN KEY REFERENCES period(periodid)  ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
examActivity VARCHAR(50) FOREIGN KEY REFERENCES examActivities(name)  ON DELETE CASCADE ON UPDATE CASCADE NOT NULL,
students INT NOT NULL,
projekts INT NOT NULL,
daysUsed INT NOT NULL,
PRIMARY KEY (period, examActivity) 
)
GO
--DROP TRIGGER meetingCalculater
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
		INSERT INTO meetingVariable VALUES ('percentage',9)
		INSERT INTO meetingVariable VALUES ('workHours',7.4)
		INSERT INTO person VALUES('admi', 'admin', 'Administrator', '', 1)
		
--		INSERT INTO period VALUES('test','20140801','20140901', 355.8)
--		INSERT INTO period VALUES('test','20140901','20141001', null)
--		INSERT INTO period VALUES('TK','20140101','20140731', null)
--		INSERT INTO period VALUES('SM','20140101','20140731', null)
--		INSERT INTO period VALUES('SM','20140801','20141231', null)
--		INSERT INTO period VALUES('KR','20140801','20141231', null)
		
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
		
--		INSERT INTO dayPeriod VALUES(1,'Illness',3)
--		INSERT INTO dayPeriod VALUES(1,'Vacation',3)
--		INSERT INTO dayPeriod VALUES(1,'Holiday',3)
--		INSERT INTO dayPeriod VALUES(2,'Illness',0)
--		INSERT INTO dayPeriod VALUES(2,'Vacation',0)
--		INSERT INTO dayPeriod VALUES(2,'Holiday',0)
--		
--		INSERT INTO estimatePeriod VALUES(1,'Booklisting',3)
--		INSERT INTO estimatePeriod VALUES(1,'Office',5)
--		INSERT INTO estimatePeriod VALUES(2,'Booklisting',8)
--		INSERT INTO estimatePeriod VALUES(2,'Office',16)
--		
--		INSERT INTO formulaPeriod VALUES(1,'Classes, Datamatiker',25)
--		INSERT INTO formulaPeriod VALUES(1,'Classes, Datamatiker, English',4)
--		INSERT INTO formulaPeriod VALUES(1,'Olc',0)
--		INSERT INTO formulaPeriod VALUES(2,'Classes, Datamatiker',25)
--		INSERT INTO formulaPeriod VALUES(2,'Classes, Datamatiker, English',4)
--		INSERT INTO formulaPeriod VALUES(2,'Olc',3)
--		
--		INSERT INTO examperiod VALUES(1,'2. Semester Datamatiker',25,6,3)
--		INSERT INTO examperiod VALUES(1,'3. Semester Datamatiker',16,4,0)
--		INSERT INTO examperiod VALUES(2,'2. Semester Datamatiker',0,0,0)
--		INSERT INTO examperiod VALUES(2,'3. Semester Datamatiker',0,0,0)
    COMMIT
END TRY
BEGIN CATCH
 ROLLBACK
END CATCH

GO
DROP FUNCTION getDaysDifference
GO
CREATE FUNCTION getDaysDifference(@id int)
RETURNS FLOAT
AS
BEGIN

DECLARE @workdays FLOAT
DECLARE @start VARCHAR(24)
DECLARE @end VARCHAR(24)

SET @start = CONVERT(NVARCHAR(24), (SELECT startdate FROM period WHERE periodId = @Id), 121)
SET @end = CONVERT(NVARCHAR(24), (SELECT enddate FROM period WHERE periodId = @Id), 121)
SET @workdays = (SELECT
   (DATEDIFF(dd, @start, @end) + 1)
  -(DATEDIFF(wk, @start, @end) * 2)
  -(CASE WHEN DATENAME(dw, @start) = 'Sunday' THEN 1 ELSE 0 END)
  -(CASE WHEN DATENAME(dw, @end) = 'Saturday' THEN 1 ELSE 0 END))
RETURN @workdays * (SELECT value FROM meetingVariable WHERE name ='workHours')
END
GO

DROP FUNCTION getTotalTimeUsed
GO
CREATE FUNCTION getTotalTimeUsed(@id int)
RETURNS FLOAT
AS
BEGIN

DECLARE @days FLOAT
DECLARE @meeting FLOAT
DECLARE @estimate FLOAT
DECLARE @formula FLOAT
DECLARE @exam FLOAT
DECLARE @return FLOAT

DECLARE @temp FLOAT

SET @temp = (SELECT mv.value FROM meetingVariable mv WHERE mv.name = 'workHours')

SET @days = (SELECT Sum(dp.daysUsed * @temp)
			FROM dayPeriod dp
			WHERE dp.period = @id)

SET @meeting = (SELECT estimatedHours 
				FROM meeting 
				WHERE period = @id)

SET @estimate =  (SELECT Sum(ep.hoursUsed)
				 FROM estimatePeriod ep
				 WHERE ep.period = @id)

SET @formula =  (SELECT Sum(fp.variable*fa.formulamultiplier)
				 FROM formulaActivities fa, formulaPeriod fp 
				 WHERE fa.activity = fp.formulaActivity AND fp.period = @id)

SET @exam = (Select Sum((ep.students*ea.studentsmultiplier) + (ep.projekts*ea.projectsmultiplier) + (ep.daysUsed*ea.daysmultiplier))
			FROM examperiod ep, examActivities ea 
			WHERE ep.examActivity = ea.name AND  ep.period = @id)

SET @return = Round(Sum(@days + @estimate + @formula + @exam + @meeting), 2)

RETURN @return
END
GO

DROP FUNCTION getNettoTime
GO
CREATE FUNCTION getNettoTime(@id int)
RETURNS FLOAT
AS
BEGIN

DECLARE @Return FLOAT
DECLARE @days FLOAT

SET @days = dbo.getDaysDifference(@id)

SET @Return = Sum(dbo.getTotalTimeUsed(@id) - dbo.getDaysDifference(@id))

RETURN @Return
END
GO