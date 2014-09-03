using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktik_estimering
{
    class DataLink
    {
        String connStr;
        SqlConnection con;
        
        public DataLink()
        {
            connStr = @"Data Source=BJARKES-PC\SQLEXPRESS;Initial Catalog=praktik_estimate;Integrated Security=True";
            con = new SqlConnection(connStr);
        }

        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
