using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters;
using System.Windows;

namespace praktik_estimering
{
    class UpdateService
    {
        private int selectedPeriod;
        private static UpdateService instance;
        private static SqlConnection con;

        public DataSet allTables;
        public DataTable Day { get; private set; }
        public DataTable Esti { get; private set; }
        public DataTable Form { get; private set; }
        public DataTable Exam { get; private set; }

        private SqlDataAdapter dataAdapterAllTables;

        private UpdateService()
        {
            DataLink dl = new DataLink();
            con = dl.getConnection();
            allTables = new DataSet();
            dataAdapterAllTables = new SqlDataAdapter();
        }
        public static UpdateService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UpdateService();
                }
                return instance;
            }
        }
        public DataTable getPeriods()
        {
            string sql = "SELECT periodId 'Id', startdate 'start', enddate 'end' " +
                         "FROM period " +
                         "WHERE person = '" + UserService.Instance.getuserId() + "'";
            DataTable dt = getDataTable(sql);
            dt.Columns.Add("Show", typeof(String));

            string start;
            string end;
            foreach (DataRow row in dt.Rows)
            {
                start = row[1].ToString();
                end = row[2].ToString();

                start = Convert.ToDateTime(start).ToShortDateString();
                end = Convert.ToDateTime(end).ToShortDateString();

                row["show"] = start + " - " + end;
            }
            return dt;
        }
        public void getDataSet(int selectedValue)
        {
            allTables.Clear();

            string sql = "SELECT * FROM dayPeriod WHERE period = " + selectedValue + ";" +
                         "SELECT * FROM estimatePeriod WHERE period = " + selectedValue + ";" +
                         "SELECT * FROM formulaPeriod WHERE period = " + selectedValue + ";" +
                         "SELECT * FROM examPeriod WHERE period = " + selectedValue + ";";

            SqlDataAdapter dataAdapterallTables = new SqlDataAdapter(sql, con);
            dataAdapterallTables.TableMappings.Add("day", "dayPeriod");
            dataAdapterallTables.TableMappings.Add("estimate", "estimatePeriod");
            dataAdapterallTables.TableMappings.Add("formula", "formulaPeriod");
            dataAdapterallTables.TableMappings.Add("exam", "examPeriod");

            dataAdapterallTables.Fill(allTables);

            Day = allTables.Tables[0];
            Esti = allTables.Tables[1];
            Form = allTables.Tables[2];
            Exam = allTables.Tables[3];
        }
        public void updatePeriod()
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand dayComand = updateDay();
                SqlCommand EstimateComand = updateEstimate();
                SqlCommand FormulaComand = updateFormula();
                SqlCommand ExamComand = updateExam();

                dayComand.Transaction = tran;
                EstimateComand.Transaction = tran;
                FormulaComand.Transaction = tran;
                ExamComand.Transaction = tran;

                dayComand.ExecuteNonQuery();
                EstimateComand.ExecuteNonQuery();
                FormulaComand.ExecuteNonQuery();
                ExamComand.ExecuteNonQuery();

                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                MessageBox.Show(e.Message);
                // MessageBox.Show("something went wrong with the new data, please check your data and try again");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        /*
         * kig på parameter typerne de skal lige passes til.
         */
        private SqlCommand updateDay()
        {
            dataAdapterAllTables.UpdateCommand =
                new SqlCommand(
                    "UPDATE dayPeriod SET daysUsed= @daysUsed WHERE period = @period AND dayActivity = @dayActivity", con);
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@daysUsed", "daysUsed");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@period",SqlDbType.Int, "period");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@dayActivity", "dayActivity");

            return dataAdapterAllTables.UpdateCommand;
        }
        private SqlCommand updateEstimate()
        {
            dataAdapterAllTables.UpdateCommand =
                new SqlCommand(
                    "UPDATE estimatePeriod SET hoursUsed = @hoursUsed WHERE period = @period AND estimateActivity = @estimateActivity",
                    con);
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@hoursUsed", "hoursUsed");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@estimateActivity", "estimateActivity");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@period", "period");

            return dataAdapterAllTables.UpdateCommand;
        }
        private SqlCommand updateFormula()
        {
            dataAdapterAllTables.UpdateCommand =
                new SqlCommand(
                    "UPDATE formulaPeriod SET variable = @variable WHERE period = @period AND formulaActivity = @formulaActivity",
                    con);
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@variable", "variable");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@period", "period");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@formulaActivity", "formulaActivity");

            return dataAdapterAllTables.UpdateCommand;
        }
        private SqlCommand updateExam()
        {
            dataAdapterAllTables.UpdateCommand =
                new SqlCommand(
                    "UPDATE examPeriod SET students = @students, projekts = @projekts, daysUsed =@daysUsed WHERE period =  @period AND examActivity = @examActivity",
                    con);
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@students", "students");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@projekts", "projekts");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@daysUsed", "daysUsed");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@period", "period");
            dataAdapterAllTables.UpdateCommand.Parameters.Add("@examActivity", "examActivity");

            return dataAdapterAllTables.UpdateCommand;
        }
        private static DataTable getDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            catch (SqlException)
            {
                // e.Message
                // "Error while reading database"
                MessageBox.Show("Error while reading database");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return dt;
        }
    }
}
