using System;
using System.Collections.Generic;
using System.Linq;
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
        static DataLink dl = new DataLink();
        static SqlConnection con = dl.getConnection();

        DataTable userTabel = null;

        private int selectedPeriod;
        private static UserService instance;

        private UserService() { }

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
                string sql = "SELECT * FROM person WHERE init = '" + initials + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                userTabel = new DataTable();
                da.Fill(userTabel);

                string pass = null;
                string init = null;

                foreach (DataRow row in userTabel.Rows)
                {
                    pass = row["Pass"].ToString();
                    init = row["Init"].ToString();
                }

                if (initials == init)
                {
                    if (password == pass)
                    {
                        return true;
                    }
                    else throw new Exception("password does not match initials.");
                }
                else throw new Exception("initals was not found please try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
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

            summary.AddRange(addDayAktivities());
            summary.AddRange(addEstimateAktivities());
            summary.AddRange(addFormulaAktivities());

            return summary;
        }
        public string getNormTime()
        {
            double result;
            using (SqlConnection con = dl.getConnection())
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "getDaysDifference";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", getuserId());

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                con.Open();
                cmd.ExecuteNonQuery();
                result = (int)returnParameter.Value;
            }

            return (result * 7.5).ToString();
        }
        public int getUsedTime()
        {
            string sqlUsedTime = "SELECT SUM(da.Number * 7.4) + SUM(ea.Number) + SUM(fa.Number) 'sum' " +
                                 "FROM Period p, DayActive da, EstimateActive ea, FormulasActive fa " +
                                 "WHERE p.Id = da.Period AND p.Id = ea.Period AND p.Id = fa.Period AND p.Id = " + selectedPeriod;

            DataTable timeTable = getDataTable(sqlUsedTime);

            double usedTime = -1;
            foreach (DataRow row in timeTable.Rows)
            {
                usedTime = double.Parse(row["sum"].ToString());
            }
            int result = Convert.ToInt32(usedTime);
            return result;
        }
        private static DataTable getDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {               
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                // e.Message
                // "Error while reading database"
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return dt;
        }
        private String getuserId()
        {
            string id = null;
            foreach (DataRow row in userTabel.Rows)
            {
                id = row["id"].ToString();
            }

            return id;
        }
        private List<String> addDayAktivities()
        {
            List<string> dayList = new List<string>();
            string sqlDay = "SELECT dt.TypeName 'Type', SUM(da.number)*7.4 'Hours' " +
                         "FROM DayActive da, DayTable dt " +
                         "WHERE da.DayTable = dt.Id AND da.Period = " + selectedPeriod +
                         "GROUP BY dt.TypeName ";

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

            string sqlEstimate = "SELECT e.TypeName as 'Type', SUM(number) as 'Hours' " +
                                "from Estimate as e, EstimateActive as ea " +
                                "where ea.estimate = e.id and ea.period = " + selectedPeriod +
                                "GROUP BY e.TypeName ";

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

            string sqlformula = "SELECT f.Name, SUM(fa.Number) " +
                                "FROM FormulasActive fa, Period p, Formula f " +
                                "WHERE fa.Formula = F.Id AND fa.Period = " + selectedPeriod +
                                "GROUP BY f.Name ";

            DataTable formulaTable = getDataTable(sqlformula);

            formulaList.Add("form activities: ");

            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in formulaTable.Rows)
            {
                sb.AppendLine(string.Join("  -  ", row.ItemArray));
                formulaList.Add(sb.ToString());
                sb.Clear();
            }
            return formulaList;
        }



    }
}
