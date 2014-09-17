using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace praktik_estimering
{
    class PeriodService
    {
        private static SqlConnection con;

        private static PeriodService instance;
        private static int activeUser;
        private static int activePeriod;
        public List<string> sqls = new List<string>();

        private PeriodService()
        {
            DataLink dl = new DataLink();
            con = dl.getConnection();
            activeUser = int.Parse(UserService.Instance.getuserId());
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

                string sql = "INSERT INTO Period(Person, StartDate, EndDate) " +
                             "values(@activeUser, @start, @end) ";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.Add("@start", SqlDbType.Date).Value = start.ToShortDateString();
                command.Parameters.Add("@end", SqlDbType.Date).Value = end.ToShortDateString();
                command.Parameters.Add("@activeUser", SqlDbType.Int).Value = activeUser;
                executeQuery(command);

                sql = "SELECT MAX(Id) 'id' FROM Period WHERE Person = " + activeUser;
                DataTable dt = getDataTable(sql);
                string ap = "";
                foreach (DataRow row in dt.Rows)
                {
                    ap = row["id"].ToString();
                    
                }
                activePeriod = Convert.ToInt32(ap);
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
            string sql = "SELECT dt.Id 'Id', dt.TypeName 'Name' " +
                         "FROM DayTable dt";

            DataTable dt = getDataTable(sql);

            dt.Columns.Add("Days", Type.GetType("System.Int32"));
            foreach (DataRow row in dt.Rows)
            {
                row["Days"] = 0;
            }
            return dt;
        }
        public bool InsertDayActivities(DataTable activityTable)
        {
            bool result = false;
            string sql;
            string id;
            string value;

            for (int i = 0; i < activityTable.Rows.Count; i++)
            {
                id = activityTable.Rows[i].ItemArray[0].ToString();
                value = activityTable.Rows[i].ItemArray[2].ToString();
                sql = "INSERT INTO DayActive VALUES (" + activePeriod + ", " + id + ", " + value + ")";

                sqls.Add(sql);
                result = true;
            }
            return result;
        }
        public DataTable EstimateactivityList()
        {
            string sql = "SELECT dt.Id 'Id', dt.TypeName 'Name' " +
                         "FROM Estimate dt";

            DataTable dt = getDataTable(sql);

            dt.Columns.Add("Hours", Type.GetType("System.Double"));
            foreach (DataRow row in dt.Rows)
            {
                row["Hours"] = 0;
            }
            return dt;
        }
        public bool InsertestimationActivities(DataTable activityTable)
        {
            bool result = false;
            string sql;
            string id;
            string value;

            for (int i = 0; i < activityTable.Rows.Count; i++)
            {
                id = activityTable.Rows[i].ItemArray[0].ToString();
                value = activityTable.Rows[i].ItemArray[2].ToString();
                sql = "INSERT INTO EstimateActive VALUES (" + activePeriod + ", " + id + ", " + value + ")";

                sqls.Add(sql);
                result = true;
            }
            return result;
        }
        public DataTable EactivityList()
        {
            string sql = "SELECT dt.Id 'Id', dt.TypeName 'Name' " +
                         "FROM Estimate dt";

            DataTable dt = getDataTable(sql);

            dt.Columns.Add("Hours", Type.GetType("System.Double"));
            foreach (DataRow row in dt.Rows)
            {
                row["Hours"] = 0;
            }
            return dt;
        }
        public DataTable FormulaList()
        {
            string sql = "SELECT f.Id 'Id', f.Name 'Name' " +
                         "FROM Formula f";

            DataTable dt = getDataTable(sql);

            dt.Columns.Add("antal", Type.GetType("System.Double"));

            foreach (DataRow row in dt.Rows)
            {
                row["antal"] = 0;
            }
            return dt;
        }
        public bool InsertFormulaActivities(DataTable activityTable)
        {

            string sql;
            string formulaId;
            double number;

            for (int i = 0; i < activityTable.Rows.Count; i++)
            {
                formulaId = activityTable.Rows[i].ItemArray[2].ToString();
                number = Convert.ToDouble(activityTable.Rows[i].ItemArray[2].ToString());
                sql = "INSERT INTO FormulasActive VALUES ( " + activePeriod + ", " + formulaId + ", " + number + ")";

                sqls.Add(sql);
            }
            return true;
        }
        public DataTable getExamns()
        {
            string sql = "SELECT e.Id 'Id', e.name 'name'" +
                         "FROM Exam e";

            DataTable dt = getDataTable(sql);

            dt.Columns.Add("students", Type.GetType("System.Double"));
            dt.Columns.Add("projekts", Type.GetType("System.Double"));
            dt.Columns.Add("days", Type.GetType("System.Double"));

            foreach (DataRow row in dt.Rows)
            {
                row["students"] = 0;
                row["projekts"] = 0;
                row["days"] = 0;
            }
            return dt;
        }
        public void InsertList()
        {
            try
            {
                SqlCommand cmd;
                con.Open();
                foreach (string s in sqls)
                {
                    cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();
                }
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
