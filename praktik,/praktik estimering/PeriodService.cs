using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace praktik_estimering
{
    class PeriodService
    {
        private static SqlConnection con;

        private static PeriodService instance;
        private static string activeUser;
        private static int activePeriod;

        public List<string> sqls = new List<string>();

        private PeriodService()
        {
            DataLink dl = new DataLink();
            con = dl.getConnection();
            activeUser = UserService.Instance.getuserId();
        }
        public static PeriodService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PeriodService();
                }
                return instance;
            }
        }
        public bool InsertNewPeriod(DateTime start, DateTime end)
        {
            bool result = false;
            try
            {
                if (start > end) throw new Exception("end date needs to be later than the start date");
                if (start.Equals(null) || end.Equals(null)) throw new Exception("you need to choose both a start and end date");

                string sql = "INSERT INTO period(person, startdate, enddate) " +
                    "VALUES(@activeUser, @start, @end)";

                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.Add("@start", SqlDbType.Date).Value = start.ToShortDateString();
                command.Parameters.Add("@end", SqlDbType.Date).Value = end.ToShortDateString();
                command.Parameters.Add("@activeUser", SqlDbType.VarChar).Value = activeUser;
                executeQuery(command);

                sql = "SELECT MAX(periodId) 'id' FROM period WHERE person = '" + activeUser + "' ";
                DataTable dt = getDataTable(sql);

                foreach (DataRow row in dt.Rows)
                {
                    activePeriod = Convert.ToInt32(row[0].ToString());
                }
                Debug.WriteLine("ACTIVE PERIOD IS: " + activePeriod);
                UserService.Instance.SelectedPeriod = activePeriod;
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }
        public DataTable DayactivityList()
        {
            string sql = "SELECT activity 'Activity' FROM dayActivities ";
            DataTable dt = getDataTable(sql);

            dt.Columns.Add("Days", typeof(Int32));
            foreach (DataRow row in dt.Rows)
            {
                row["Days"] = 0;
            }
            return dt;
        }
        public bool InsertDayActivities(DataTable activityTable)
        {
            string sql;
            string activity;
            string value;

            foreach (DataRow row in activityTable.Rows)
            {
                activity = row.ItemArray[0].ToString();
                value = row.ItemArray[1].ToString();

                sql = "INSERT INTO dayPeriod VALUES (" + activePeriod + ", '" + activity + "', " + value + ")";
                sqls.Add(sql);
            }
            return true;
        }
        public DataTable EstimateactivityList()
        {
            string sql = "SELECT activity 'Activity' FROM estimateActivities";

            DataTable dt = getDataTable(sql);

            dt.Columns.Add("Hours", typeof(Double));
            foreach (DataRow row in dt.Rows)
            {
                row["Hours"] = 0;
            }
            return dt;
        }
        public bool InsertestimationActivities(DataTable activityTable)
        {
            string sql;
            string activity;
            string value;

            foreach (DataRow row in activityTable.Rows)
            {
                activity = row.ItemArray[0].ToString();
                value = row.ItemArray[1].ToString();
                sql = "INSERT INTO estimatePeriod VALUES (" + activePeriod + ", '" + activity + "', " + value + ")";

                sqls.Add(sql);
            }
            return true;
        }
        public DataTable FormulaList()
        {
            string sql = "SELECT fa.activity 'Activity' FROM formulaActivities fa";

            DataTable dt = getDataTable(sql);

            dt.Columns.Add("antal", typeof(Double));

            foreach (DataRow row in dt.Rows)
            {
                row["antal"] = 0;
            }
            return dt;
        }
        public bool InsertFormulaActivities(DataTable activityTable)
        {
            string sql;
            string activity;
            double value;

            foreach (DataRow row in activityTable.Rows)
            {
                activity = row.ItemArray[0].ToString();
                value = Convert.ToDouble(row.ItemArray[1].ToString());
                sql = "INSERT INTO formulaPeriod VALUES ( " + activePeriod + ", '" + activity + "', " + value + ")";

                sqls.Add(sql);
            }
            return true;
        }
        public DataTable ExamnsList()
        {
            string sql = "SELECT name 'Activity' FROM examActivities";

            DataTable dt = getDataTable(sql);

            dt.Columns.Add("students", typeof(Double));
            dt.Columns.Add("projekts", typeof(Double));
            dt.Columns.Add("days", typeof(Double));
            foreach (DataRow row in dt.Rows)
            {
                row["students"] = 0;
                row["projekts"] = 0;
                row["days"] = 0;
            }
            return dt;
        }
        public bool InsertExamnActivities(DataTable activityTable)
        {
            string sql;
            string activity;
            double students;
            double projekt;
            double days;

            foreach (DataRow row in activityTable.Rows)
            {
                activity = row.ItemArray[0].ToString();
                students = Convert.ToDouble(row.ItemArray[1].ToString());
                projekt = Convert.ToDouble(row.ItemArray[2].ToString());
                days = Convert.ToDouble(row.ItemArray[3].ToString());

                sql = "INSERT INTO examPeriod VALUES ( " + activePeriod + ", '" + activity + "', " + students + ", " + projekt + ", " + days + ")";

                sqls.Add(sql);
            }
            InsertList(sqls);
            updatePeriod();
            return true;
        }

        public void cancelEverything()
        {
            try
            {
                string sql = "DELETE FROM period WHERE periodId = " + activePeriod;

                SqlCommand command = new SqlCommand(sql, con);
                executeQuery(command);

                clearList();
                UserService.Instance.SelectedPeriod = activePeriod;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private void updatePeriod()
        {
            string sql = "UPDATE period " +
                         "SET nettoHours= dbo.getNettoTime(" + activePeriod + ") " +
                         "WHERE periodId=" + activePeriod;
            SqlCommand com = new SqlCommand(sql, con);
            executeQuery(com);
        }

        private void InsertList(List<string> list)
        {
            SqlCommand cmd;
            try
            {
                foreach (string s in list)
                {
                    Debug.WriteLine(s);
                    cmd = new SqlCommand(s, con);
                    executeQuery(cmd);
                }
                clearList();
            }
            catch (Exception e)
            {

            }

        }

        private static void executeQuery(SqlCommand cmd)
        {
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private static DataTable getDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Error while reading database");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return dt;
        }
        private void clearList()
        {
            sqls.Clear();
        }
    }
}
