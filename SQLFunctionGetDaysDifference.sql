CREATE FUNCTION getDaysDifference
(@id int)

RETURNS int
AS
BEGIN

Declare @workdays int
Declare @start varchar(24)
Declare @end varchar(24)

Set @start = CONVERT(nvarchar(24), (Select StartDate from Period where Id = @Id), 121)
Set @end = CONVERT(nvarchar(24), (Select EndDate from Period where Id = @Id), 121)
Set @workdays = (Select
   (DATEDIFF(dd, @start, @end) + 1)
  -(DATEDIFF(wk, @start, @end) * 2)
  -(CASE WHEN DATENAME(dw, @start) = 'Sunday' THEN 1 ELSE 0 END)
  -(CASE WHEN DATENAME(dw, @end) = 'Saturday' THEN 1 ELSE 0 END))
Return @workdays
End