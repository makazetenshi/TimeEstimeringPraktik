drop procedure insertFormulaActive

CREATE PROCEDURE insertFormulaActive

@periodId int,
@formulaId int,
@numberStudents int,
@numberProjects int,
@numberDays int,
@education varchar(25)

AS
BEGIN

Declare @Value float

Set @Value = dbo.getExamValue(@numberStudents, @numberProjects, @numberDays, @education, @formulaId)

Insert into FormulasActive values(@periodId, @formulaId, @Value)

end