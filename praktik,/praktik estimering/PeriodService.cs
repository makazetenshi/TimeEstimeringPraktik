using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.SqlServer.Server;

namespace praktik_estimering
{
    class PeriodService
    {
        private static SqlConnection con;

        public static List<string> dayActivities;
        private static List<string> estimateActivities;
        private static List<string> formulaActivities;

        private static PeriodService instance;
        private static int activeUser;

        private PeriodService()
        {
            DataLink dl = new DataLink();
            con = dl.getConnection();
            activeUser = int.Parse(UserService.Instance.getuserId());

            dayActivities = new List<string>();
            estimateActivities = new List<string>();
            formulaActivities = new List<string>();
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
/*

        public static List<string> DayActivities
        {
            get { return dayActivities; }
        }
        public static List<string> EstimateActivities
        {
            get { return estimateActivities; }
        }
        public static List<string> FormulaActivities
        {
            get { return formulaActivities; }
        }
*/

        public void InsertNewPeriod(DateTime start, DateTime end)
        {
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void addToDayList(int id, string activity, string NumberOfDays)
        {
            string addition = id + " - " + activity + " - " + NumberOfDays;
            dayActivities.Add(addition);
        }
        public void InsertDayActivities(List<string> activityList)
        {
            MessageBox.Show("not implemented");
        }
        public void InsertestimationActivities(List<string> activityList)
        {
            MessageBox.Show("not implemented");
        }
        public void InsertFormulaActivities(List<string> activityList)
        {
            MessageBox.Show("not implemented");
        }






        private void executeQuery(SqlCommand cmd)
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
    }
}
