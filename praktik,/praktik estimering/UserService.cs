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

        public Boolean login(string initials, string password)
        {
            try
            {
                string sql = "SELECT init, pass FROM person WHERE init = '" + initials + "'";
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

        public DataTable getPastPeriods(string initialer)
        {
            string sql = "SELECT * FROM Period where person = '" + initialer + "'";
            return getDataTable(sql);
        }

        public String getInitials()
        {
            string init = null;
            foreach (DataRow row in userTabel.Rows)
            {
                init = row["Init"].ToString();
            }

            return init;
        }








        private static DataTable getDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error while reading database");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close;
            }
            return dt;
        }








    }


}
