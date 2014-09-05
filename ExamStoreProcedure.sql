DROP Procedure getExamFormulaValue
 
CREATE Procedure getExamFormulaValue

@numberStudents int,
@numberProjects int,
@numberDays float,
@name varchar(255),
@education varchar(25),
@Result float out

AS
BEGIN

Select @Result = ((Select ParameterStudents from Exams where Name = @name and Education = @education) * @numberStudents)
	    		+((Select ParameterProjects from Exams where Name = @name and Education = @education) * @numberProjects)
				+((Select ParameterDays from Exams where Name = @name and Education = @education) * @numberDays)

END

-- Start Testdata

Declare @out float
exec getExamFormulaValue 20, 4, 2, 3, 'Datamatiker', @out output

select @out as Estimate

-- Slut Testdata