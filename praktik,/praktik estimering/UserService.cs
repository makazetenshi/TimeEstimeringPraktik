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

                foreach (DataRow row in userTabel.Rows)
                {
                    string name = row["Name"].ToString();
                    string pass = row["Pass"].ToString();
                    string init = row["Init"].ToString();

                }

                if (initials == init && password == pass) return true;
                else throw new Exception("Wrong combination of Initials and password");

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



    }


}
