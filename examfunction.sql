drop function getExamValue

CREATE FUNCTION getExamValue
(@students int, @projects int, @days int, @education varchar(50), @formulaId int) 

RETURNS float

AS 
BEGIN 
declare @Return float
declare @studentvalue float
declare @projectvalue float
declare @daysvalue float

Set @studentvalue = Case when @students = 0 then 0 else
(Select Sum(@students * p.Parameter) from Parameter p where p.Name = 'studerende' and p.Education = (Select Id from Education where Name = @education) and p.Formula = @formulaId) end
Set @projectvalue = Case when @projects = 0 then 0 else
(Select Sum(@projects * p.Parameter) from Parameter p where p.Name = 'projekter' and p.Education = (Select Id from Education where Name = @education) and p.Formula = @formulaId) end
Set @daysvalue = Case when @days = 0 then 0 else
(Select Sum(@days * p.Parameter) from Parameter p where p.Name = 'dage' and p.Education = (Select Id from Education where Name = @education) and p.Formula = @formulaId) end

Set @Return = Round(Sum(@studentvalue + @projectvalue + @daysvalue), 2)

return @Return
end