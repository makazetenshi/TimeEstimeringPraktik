CREATE FUNCTION getValueSimple
(@name varchar(50), @number float, @education varchar(50), @engvalue bit) 

RETURNS float

AS 
BEGIN 
declare @Return float
Select @Return = Round(Sum(@number * p.Parameter), 2) * @engvalue
from Formula f, Parameter p
where f.name = @name and p.Name like '%' + @name + '%' and p.Education = (Select Id from Education where Name = @education)
return @Return
end