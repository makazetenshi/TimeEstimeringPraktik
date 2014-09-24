using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace praktik_estimering
{
    class UserService
    {
        private static SqlConnection con;

        DataTable userTabel = null;

        private int selectedPeriod;
        private static UserService instance;

        private UserService()
        {
            DataLink dl = new DataLink();
            con = dl.getConnection();
            userTabel = new DataTable();
        }
        public static UserService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserService();
                }
                return instance;
            }
        }
        public int SelectedPeriod
        {
            get { return selectedPeriod; }
            set { selectedPeriod = value; }
        }
        public Boolean login(string initials, string password)
        {
            try
            {
                string sql = "SELECT * FROM person WHERE initials = '" + initials + "'";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                string pass = null;
                string init = null;

                foreach (DataRow row in dt.Rows)
                {
                    pass = row["password"].ToString();
                    init = row["initials"].ToString();
                }

                if (initials == init)
                {
                    if (password == pass)
                    {
                        userTabel = dt;
                        return true;
                    }
                    throw new Exception("password does not match initials.");
                }
                throw new Exception("initals was not found please try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void logUserOut()
        {
            userTabel.Clear();
        }
        public DataTable getPastPeriods()
        {
            string sql = "SELECT * FROM Period WHERE person = '" + getuserId() + "'";
            return getDataTable(sql);
        }
        public List<String> getSelectedPeriodData()
        {
            List<string> summary = new List<string>();

            Debug.WriteLine("period id: " + SelectedPeriod);

            summary.AddRange(addDayAktivities());
            summary.AddRange(addEstimateAktivities());
            summary.AddRange(addFormulaAktivities());
            summary.AddRange(addExamAktivities());

            foreach (string s in summary)
            {
                Debug.WriteLine(s);
            }

            return summary;
        }
        public double getNormTime()
        {
            double result;
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "getDaysDifference";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", SelectedPeriod);

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Float);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                con.Open();
                cmd.ExecuteNonQuery();
                result = Math.Round((double)returnParameter.Value,2);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            return Convert.ToDouble(result);
        }
        public double getUsedTime()
        {
            double result;
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "getTotalTimeUsed";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", SelectedPeriod);

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Float);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                con.Open();
                cmd.ExecuteNonQuery();
                result = Math.Round((double)returnParameter.Value,2);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            return Convert.ToDouble(result);
        }
        public double getNettoTid()
        {
            double result;
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "getNettoTime";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", SelectedPeriod);

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Float);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                con.Open();
                cmd.ExecuteNonQuery();
                result = Math.Round((double)returnParameter.Value, 2);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            return Convert.ToDouble(result);
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
        public String getuserId()
        {
            string id = null;
            foreach (DataRow row in userTabel.Rows)
            {
                id = row["initials"].ToString();
            }

            return id;
        }
        private List<String> addDayAktivities()
        {
            List<string> dayList = new List<string>();
            string sqlDay = "SELECT dp.dayActivity, dp.daysUsed * (SELECT mv.value FROM meetingVariable mv WHERE mv.name = 'workHours') 'Hours' "+
                            "FROM dayPeriod dp " +
                            "WHERE dp.period = " + SelectedPeriod;

            DataTable dayTable = getDataTable(sqlDay);


            dayList.Add("Day activities: ");

            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dayTable.Rows)
            {
                sb.AppendLine(string.Join("  -  ", row.ItemArray));
                dayList.Add(sb.ToString());
                sb.Clear();
            }
            return dayList;
        }
        private List<string> addEstimateAktivities()
        {
            List<string> estimateList = new List<string>();

            string sqlEstimate = "SELECT ep.estimateActivity 'Activity', ep.hoursUsed 'Hours' " +
                                 "FROM estimatePeriod ep " +
                                 "WHERE ep.period = " + SelectedPeriod;

            DataTable estimateTable = getDataTable(sqlEstimate);

            estimateList.Add("Estimate activities: ");

            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in estimateTable.Rows)
            {
                sb.AppendLine(string.Join("  -  ", row.ItemArray));
                estimateList.Add(sb.ToString());
                sb.Clear();
            }
            return estimateList;
        }
        private List<string> addFormulaAktivities()
        {
            List<string> formulaList = new List<string>();

            string sqlformula = "SELECT fp.formulaActivity 'Activity', fp.variable*fa.formulamultiplier 'Hours' " +
                                "FROM formulaActivities fa, formulaPeriod fp  " +
                                "WHERE fa.activity = fp.formulaActivity AND fp.period = " + SelectedPeriod;

            DataTable formulaTable = getDataTable(sqlformula);

            formulaList.Add("Form activities: ");

            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in formulaTable.Rows)
            {
                sb.AppendLine(string.Join("  -  ", row.ItemArray));
                formulaList.Add(sb.ToString());
                sb.Clear();
            }
            return formulaList;
        }
        private List<string> addExamAktivities()
        {
            List<string> formulaList = new List<string>();

            string sqlformula = "SELECT ep.examActivity 'Aktivity', (ep.students*ea.studentsmultiplier + ep.projekts*ea.projectsmultiplier + ep.daysUsed*ea.daysmultiplier) 'Hours' " +
                                "FROM examperiod ep, examActivities ea " +
                                "WHERE ep.examActivity = ea.name AND  ep.period = " + SelectedPeriod;

            DataTable examTable = getDataTable(sqlformula);

            formulaList.Add("Exam activities: ");

            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in examTable.Rows)
            {
                sb.AppendLine(string.Join("  -  ", row.ItemArray));
                formulaList.Add(sb.ToString());
                sb.Clear();
            }
            return formulaList;
        }
    }
}
