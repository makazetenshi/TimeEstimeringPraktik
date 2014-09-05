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
                MessageBox.Show("Error while reading database");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return dt;
        }

        public DataTable getPastPeriods()
        {
            string sql = "SELECT * FROM Period WHERE person = '" + getuserId() + "'";
            return getDataTable(sql);
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

        public List<String> getSelectedPeriodData()
        {
            List<string> summary = new List<string>();
            
            string sqlDay = " SELECT dt.TypeName 'Type', SUM(number)*7.4 'Hours', COUNT(dt.TypeName) 'Entries' " +
                         "FROM DayActive da, DayTable dt" +
                         "WHERE da.DayTable = dt.Id AND da.Period =" + selectedPeriod +
                         "GROUP BY dt.TypeName";
            string sqlEstimate = "SELECT e.TypeName 'Type', SUM(number) 'Hours', COUNT(e.TypeName) 'Entries'" +
                                 "from Estimate e, EstimateActive ea" +
                                 "where ea.Estimate = e.Id and ea.Period = " + selectedPeriod +
                                 "GROUP BY e.TypeName";
           
            DataTable dayTable = getDataTable(sqlDay);
            DataTable estimateTable = getDataTable(sqlDay);

            string line;
            summary.Add("Day activities:");
            foreach (var row in dayTable.Rows)
            {
                Debug.WriteLine(row.ToString());
               // line = row.ToString();
               // summary.Add(line);
            }
            
            summary.Add("estimate aktivities:");
            foreach (var row in estimateTable.Rows)
            {
                Debug.WriteLine(row.ToString());
                // line = row.ToString();
                // summary.Add(line);
            }


            return summary;
        } 








    }


}
