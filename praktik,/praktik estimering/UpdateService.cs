﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace praktik_estimering
{
    class UpdateService
    {
        private int selectedPeriod;
        private static UpdateService instance;
        private static SqlConnection con;
        private DataSet allTables;

        public DataTable Day { get; private set; }
        public DataTable Esti { get; private set; }
        public DataTable Form { get; private set; }
        public DataTable Exam { get; private set; }

        private UpdateService()
        {
            DataLink dl = new DataLink();
            con = dl.getConnection();
            allTables = new DataSet();
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

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.TableMappings.Add("day", "dayPeriod");
            da.TableMappings.Add("estimate", "estimatePeriod");
            da.TableMappings.Add("formula", "formulaPeriod");
            da.TableMappings.Add("exam", "examPeriod");

            da.Fill(allTables);

            Day = allTables.Tables[0];
            Esti = allTables.Tables[1];
            Form = allTables.Tables[2];
            Exam = allTables.Tables[3];

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
