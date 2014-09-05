DROP Procedure getFormulaValue

CREATE Procedure getFormulaValue

@number int,
@name varchar(25),
@education varchar(25),
@eng bit,
@Result float out

AS
BEGIN
DECLARE @engvalue float
SET @engvalue = 1
if @eng = 1 AND @name = 'uv' SET @engvalue = (Select Parameter from Parameter where Name = 'engtal')
Select @Result = Round(Sum(@number * p.Parameter), 2) * @engvalue
from Formula f, Parameter p
where f.name = @name and p.Name like '%' + @name + '%' and p.Education = (Select Id from Education where Name = @education)

END

-- Start Testdata

Declare @out float
exec getFormulaValue 100, 'div', 'Datamatiker', 0, @out output

select @out as Estimate

-- Slut Testdata