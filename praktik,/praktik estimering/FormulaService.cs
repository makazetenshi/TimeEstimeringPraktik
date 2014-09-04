using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktik_estimering
{
    class FormulaService
    {
        DataLink dl = new DataLink();

        public double CalculateFormula(string name, string formula, string[] parameters)
        {
            string convertet = ConvertFormula(name, formula, parameters);
            string value = new DataTable().Compute(convertet, null).ToString();
            return double.Parse(value);
        }

        public string ConvertFormula(string name, string formula, string[] parameters)
        {
            string result = "";

            if (name.Equals("uv"))
            {
                formula = formula.Replace("AntalLektioner", parameters[0].ToString());
                formula = formula.Replace("uvtal", parameters[1].ToString());
                if (formula.Contains(','))
                    formula = formula.Replace(',', '.');

                result = formula;
            }
            else if (name.Equals("olc"))
            {
                formula = formula.Replace("AntalLektioner", parameters[0].ToString());
                formula = formula.Replace("olctal", parameters[1].ToString());
                if (formula.Contains(','))
                    formula = formula.Replace(',', '.');

                result = formula;
            }
            else if (name.Equals("div"))
            {
                formula = formula.Replace("AntalTimer", parameters[0].ToString());
                formula = formula.Replace("divtal", parameters[1].ToString());
                if (formula.Contains(','))
                    formula = formula.Replace(',', '.');

                result = formula;
            }
            else if (name.Equals("hop"))
            {
                formula = formula.Replace("AntalGrupper", parameters[0].ToString());
                formula = formula.Replace("hoptal", parameters[1].ToString());
                if (formula.Contains(','))
                    formula = formula.Replace(',', '.');

                result = formula;
            }
            else if (name.Equals("praktik"))
            {
                formula = formula.Replace("AntalPersoner", parameters[0].ToString());
                formula = formula.Replace("praktiktal", parameters[1].ToString());
                if (formula.Contains(','))
                    formula = formula.Replace(',', '.');

                result = formula;
            }

            return result;
        }

        private string[] DoDB(string sql)
        {
            string[] result = new string[3];

            SqlConnection con = dl.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result[0] = reader[0] + "";
                result[1] = reader[1] + "";
                result[2] = reader[2] + "";
            }
            con.Close();

            return result;
        }

        public void StoreProcedureSimple(string name, int number)
        {
            double result = 0;
            using (SqlConnection con = dl.getConnection())
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "getFormulaValue";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(name, number);

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Float);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                con.Open();
                cmd.ExecuteNonQuery();
                result = (double)returnParameter.Value;

            }

            Debug.WriteLine(result);
        }


        public double getInfo(int numberVariable, string name)
        {
            string sql = "";

            if (name.Equals("uv"))
                sql = "SELECT f.Name, f.Formula, p.Parameter FROM Formula f, Parameter p  WHERE f.Name = 'uv' AND p.Name = 'uvtal'";
            else if (name.Equals("olc"))
                sql = "SELECT f.Name, f.Formula, p.Parameter FROM Formula f, Parameter p  WHERE f.Name = 'olc' AND p.Name = 'olctal'";
            else if (name.Equals("div"))
                sql = "SELECT f.Name, f.Formula, p.Parameter FROM Formula f, Parameter p  WHERE f.Name = 'div' AND p.Name = 'divtal'";
            else if (name.Equals("hop"))
                sql = "SELECT f.Name, f.Formula, p.Parameter FROM Formula f, Parameter p  WHERE f.Name = 'hop' AND p.Name = 'hoptal'";
            else if (name.Equals("praktik"))
                sql = "SELECT f.Name, f.Formula, p.Parameter FROM Formula f, Parameter p  WHERE f.Name = 'praktik' AND p.Name = 'praktiktal'";

            string[] result = DoDB(sql);

            string[] parameters = new string[2];

            parameters[0] = numberVariable.ToString();
            parameters[1] = result[2].ToString();

            return (CalculateFormula(result[0], result[1], parameters));

        }

        public int GetWorkingDays(DateTime from, DateTime to)
        {
            var dayDifference = (int)to.Subtract(from).TotalDays + 1;
            return Enumerable
                .Range(0, dayDifference)
                .Select(x => from.AddDays(x))
                .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);
        }

    }
}
