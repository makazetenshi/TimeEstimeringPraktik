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
                getUserLoginData(initials);

                string pass=null;
                string init=null;

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


        private void getUserLoginData(string initials)
        {
            string sql = "SELECT init, pass FROM person WHERE init = '" + initials + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            userTabel = new DataTable();
            da.Fill(userTabel);
        }

        public void logUserOut()
        {
            userTabel.Clear();
        }








    }


}
