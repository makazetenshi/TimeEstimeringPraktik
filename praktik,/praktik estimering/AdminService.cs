using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace praktik_estimering
{
    class AdminService
    {
        private static AdminService instance;
        private static SqlConnection con;

        public DataTable users { get; set; }
        public DataTable day { get; set; }
        public DataTable estimate { get; set; }
        public DataTable formula { get; set; }
        public DataTable exam { get; set; }
        public DataTable period { get; set; }
        public DataTable other { get; set; }

        private SqlDataAdapter dataAdapterUsers;
        private SqlDataAdapter dataAdapterDays;
        private SqlDataAdapter dataAdapterEstimate;
        private SqlDataAdapter dataAdapterFormula;
        private SqlDataAdapter dataAdapterExam;
        private SqlDataAdapter dataAdapterPeriod;
        private SqlDataAdapter dataAdapterOther;

        public AdminService()
        {
            DataLink dl = new DataLink();
            con = dl.getConnection();
            day = new DataTable();
            estimate = new DataTable();
            formula = new DataTable();
            exam = new DataTable();
            period = new DataTable();
            other = new DataTable();
            users = new DataTable();
        }

        public static AdminService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdminService();
                }
                return instance;
            }
        }

        private void getUsers()
        {
            dataAdapterUsers = new SqlDataAdapter();

            string sql = "SELECT * FROM person";
            SqlCommand selectcommand = new SqlCommand(sql, con);
            dataAdapterUsers.SelectCommand = selectcommand;

            SqlCommand updatecommand = new SqlCommand(
                    "UPDATE person "+
                    "SET initials = @init, password =@pass, firstname = @first, lastname = @last " +
                    "WHERE initials = @oldinit", con);
            updatecommand.Parameters.Add("@init", SqlDbType.VarChar, 4, "initials");
            updatecommand.Parameters.Add("@pass", SqlDbType.VarChar, 15, "password");
            updatecommand.Parameters.Add("@first", SqlDbType.VarChar, 15, "firstname");
            updatecommand.Parameters.Add("@last", SqlDbType.VarChar, 15, "lastname");
            updatecommand.Parameters.Add("@oldinit", SqlDbType.VarChar, 4, "initials").SourceVersion = DataRowVersion.Original;
            dataAdapterUsers.UpdateCommand = updatecommand;

            SqlCommand deletecommand = new SqlCommand(
                    "DELETE FROM person " +
                    "WHERE initials = @init", con);
            deletecommand.Parameters.Add("@init", SqlDbType.VarChar, 4, "initials");
            dataAdapterUsers.DeleteCommand = deletecommand;

            SqlCommand insertcommand = new SqlCommand(
                    "INSERT INTO person " +
                    "VALUES(@init, @pass, @first, @last)", con);
            insertcommand.Parameters.Add("@init", SqlDbType.VarChar, 4, "initials");
            insertcommand.Parameters.Add("@pass", SqlDbType.VarChar, 15, "password");
            insertcommand.Parameters.Add("@first", SqlDbType.VarChar, 15, "firstname");
            insertcommand.Parameters.Add("@last", SqlDbType.VarChar, 15, "lastname");
            dataAdapterUsers.InsertCommand = insertcommand;

            dataAdapterUsers.Fill(users);
        }
        private void getDays()
        {
            string sql = "SELECT * FROM dayActivities";
            dataAdapterDays = new SqlDataAdapter();
            SqlCommand selectcommand = new SqlCommand(sql, con);
            dataAdapterDays.SelectCommand = selectcommand;

            SqlCommand updatecommand = new SqlCommand(
                    "UPDATE dayActivities " +
                    "SET activity = @activity " +
                    "WHERE activity = @oldactivity", con);
            updatecommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            updatecommand.Parameters.Add("@oldactivity", SqlDbType.VarChar, 50, "activity").SourceVersion = DataRowVersion.Original;
            dataAdapterDays.UpdateCommand = updatecommand;

            SqlCommand deletecommand = new SqlCommand(
                    "DELETE FROM dayActivities " +
                    "WHERE activity = @activity", con);
            deletecommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            dataAdapterDays.DeleteCommand = deletecommand;

            SqlCommand insertcommand = new SqlCommand(
                    "INSERT INTO dayActivities " +
                    "VALUES(@activity)", con);
            insertcommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            dataAdapterDays.InsertCommand = insertcommand;

            dataAdapterDays.Fill(day);
        }
        private void getEstimate()
        {
            string sql = "SELECT * FROM estimateActivities";
            dataAdapterEstimate = new SqlDataAdapter();
            SqlCommand selectcommand = new SqlCommand(sql, con);
            dataAdapterEstimate.SelectCommand = selectcommand;

            SqlCommand updatecommand = new SqlCommand(
                    "UPDATE estimateActivities " +
                    "SET activity = @activity " +
                    "WHERE activity = @oldactivity", con);
            updatecommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            updatecommand.Parameters.Add("@oldactivity", SqlDbType.VarChar, 50, "activity").SourceVersion = DataRowVersion.Original;
            dataAdapterEstimate.UpdateCommand = updatecommand;

            SqlCommand deletecommand = new SqlCommand(
                    "DELETE FROM estimateActivities " +
                    "WHERE activity = @activity", con);
            deletecommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            dataAdapterEstimate.DeleteCommand = deletecommand;

            SqlCommand insertcommand = new SqlCommand(
                    "INSERT INTO estimateActivities " +
                    "VALUES(@activity)", con);
            insertcommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            dataAdapterEstimate.InsertCommand = insertcommand;

            dataAdapterEstimate.Fill(estimate);
        }
        private void getFormula()
        {
            string sql = "SELECT * FROM formulaActivities";
            dataAdapterFormula = new SqlDataAdapter();
            SqlCommand selectcommand = new SqlCommand(sql, con);
            dataAdapterFormula.SelectCommand = selectcommand;

            SqlCommand updatecommand = new SqlCommand(
                    "UPDATE formulaActivities " +
                    "SET activity = @activity , formulamultiplier = @formulamultiplier " +
                    "WHERE activity = @oldactivity", con);
            updatecommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            updatecommand.Parameters.Add("@oldactivity", SqlDbType.VarChar, 50, "activity").SourceVersion = DataRowVersion.Original;
            updatecommand.Parameters.Add("@formulamultiplier", SqlDbType.Float, 10, "formulamultiplier");
            dataAdapterFormula.UpdateCommand = updatecommand;

            SqlCommand deletecommand = new SqlCommand(
                    "DELETE FROM formulaActivities " +
                    "WHERE activity = @activity", con);
            deletecommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            dataAdapterFormula.DeleteCommand = deletecommand;

            SqlCommand insertcommand = new SqlCommand(
                    "INSERT INTO formulaActivities " +
                    "VALUES(@activity, @formulamultiplier)", con);
            insertcommand.Parameters.Add("@activity", SqlDbType.VarChar, 50, "activity");
            insertcommand.Parameters.Add("@formulamultiplier", SqlDbType.VarChar, 10, "formulamultiplier");
            dataAdapterFormula.InsertCommand = insertcommand;

            dataAdapterFormula.Fill(formula);
        }
        private void getExam()
        {
            dataAdapterExam = new SqlDataAdapter();
            string sql = "SELECT * FROM examActivities";
            SqlCommand selectcommand = new SqlCommand(sql, con);
            dataAdapterExam.SelectCommand = selectcommand;

            SqlCommand updatecommand = new SqlCommand(
                    "UPDATE examActivities " +
                    "SET name = @name , studentsmultiplier = @studentsmultiplier , projectsmultiplier = @projectsmultiplier , daysmultiplier = @daysmultiplier " +
                    "WHERE name = @oldname", con);
            updatecommand.Parameters.Add("@name", SqlDbType.VarChar, 50, "name");
            updatecommand.Parameters.Add("@studentsmultiplier", SqlDbType.Float, 10, "studentsmultiplier");
            updatecommand.Parameters.Add("@projectsmultiplier", SqlDbType.Float, 10, "projectsmultiplier");
            updatecommand.Parameters.Add("@daysmultiplier", SqlDbType.Float, 10, "daysmultiplier");
            updatecommand.Parameters.Add("@oldname", SqlDbType.VarChar, 50, "name").SourceVersion = DataRowVersion.Original;
            dataAdapterExam.UpdateCommand = updatecommand;

            SqlCommand deletecommand = new SqlCommand(
                    "DELETE FROM examActivities " +
                    "WHERE name = @name", con);
            deletecommand.Parameters.Add("@name", SqlDbType.VarChar, 50, "name");
            dataAdapterExam.DeleteCommand = deletecommand;

            SqlCommand insertcommand = new SqlCommand(
                    "INSERT INTO examActivities " +
                    "VALUES(@name , @studentsmultiplier , @projectsmultiplier , @daysmultiplier)", con);
            insertcommand.Parameters.Add("@name", SqlDbType.VarChar, 50, "name");
            insertcommand.Parameters.Add("@studentsmultiplier", SqlDbType.Float, 10, "studentsmultiplier");
            insertcommand.Parameters.Add("@projectsmultiplier", SqlDbType.Float, 10, "projectsmultiplier");
            insertcommand.Parameters.Add("@daysmultiplier", SqlDbType.Float, 10, "daysmultiplier");
            dataAdapterExam.InsertCommand = insertcommand;

            dataAdapterExam.Fill(exam);
        }
        private void getPeriod()
        {
            string sql = "SELECT * FROM period";
            dataAdapterPeriod = new SqlDataAdapter(sql, con);
            dataAdapterPeriod.Fill(period);
        }
        private void getOther()
        {
            string sql = "SELECT * FROM meetingVariable";
            dataAdapterOther = new SqlDataAdapter();
            SqlCommand selectcommand = new SqlCommand(sql, con);
            dataAdapterOther.SelectCommand = selectcommand;

            SqlCommand updatecommand = new SqlCommand(
                    "UPDATE meetingVariable " +
                    "SET name = @name , value = @value " +
                    "WHERE name = @oldname", con);
            updatecommand.Parameters.Add("@name", SqlDbType.VarChar, 15, "name");
            updatecommand.Parameters.Add("@oldname", SqlDbType.VarChar, 15, "name").SourceVersion = DataRowVersion.Original;
            updatecommand.Parameters.Add("@value", SqlDbType.Float, 10, "value");
            dataAdapterOther.UpdateCommand = updatecommand;

            SqlCommand deletecommand = new SqlCommand(
                    "DELETE FROM meetingVariable " +
                    "WHERE name = @name", con);
            deletecommand.Parameters.Add("@name", SqlDbType.VarChar, 15, "name");
            dataAdapterOther.DeleteCommand = deletecommand;

            SqlCommand insertcommand = new SqlCommand(
                    "INSERT INTO meetingVariable " +
                    "VALUES(@name , @value)", con);
            insertcommand.Parameters.Add("@name", SqlDbType.VarChar, 15, "name");
            insertcommand.Parameters.Add("@value", SqlDbType.Float, 10, "value");
            dataAdapterOther.InsertCommand = insertcommand;

            dataAdapterOther.Fill(other);
        }

        public void updateTables()
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                foreach (DataRow row in users.Rows)
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added)
                    {   
                        dataAdapterUsers.UpdateCommand.Transaction = tran;
                        dataAdapterUsers.InsertCommand.Transaction = tran;
                        dataAdapterUsers.DeleteCommand.Transaction = tran;
                        dataAdapterUsers.Update(users);
                    }
                }
                foreach (DataRow row in day.Rows)
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                    {
                        dataAdapterDays.UpdateCommand.Transaction = tran;
                        dataAdapterDays.InsertCommand.Transaction = tran;
                        dataAdapterDays.DeleteCommand.Transaction = tran;
                        dataAdapterDays.Update(day);
                    }
                }
                foreach (DataRow row in estimate.Rows)
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                    {
                        dataAdapterEstimate.UpdateCommand.Transaction = tran;
                        dataAdapterEstimate.InsertCommand.Transaction = tran;
                        dataAdapterEstimate.DeleteCommand.Transaction = tran;
                        dataAdapterEstimate.Update(estimate);
                    }
                }
                foreach (DataRow row in formula.Rows)
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                    {
                        dataAdapterFormula.UpdateCommand.Transaction = tran;
                        dataAdapterFormula.InsertCommand.Transaction = tran;
                        dataAdapterFormula.DeleteCommand.Transaction = tran;
                        dataAdapterFormula.Update(formula);
                    }
                }
                foreach (DataRow row in exam.Rows)
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                    {
                        dataAdapterExam.UpdateCommand.Transaction = tran;
                        dataAdapterExam.InsertCommand.Transaction = tran;
                        dataAdapterExam.DeleteCommand.Transaction = tran;
                        dataAdapterExam.Update(exam);
                    }
                }
                foreach (DataRow row in other.Rows )
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                    {
                        dataAdapterOther.UpdateCommand.Transaction = tran;
                        dataAdapterOther.InsertCommand.Transaction = tran;
                        dataAdapterOther.DeleteCommand.Transaction = tran;
                        dataAdapterOther.Update(other);
                    }
                }

                tran.Commit();
                MessageBox.Show("update done");
            }
            catch (Exception e)
            {
                tran.Rollback();
                MessageBox.Show(e.Message);
                // MessageBox.Show("something went wrong with the new data, please check your data and try again");
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public void getAllTables()
        {
            getDays();
            getEstimate();
            getExam();
            getFormula();
            getUsers();
            getPeriod();
            getOther();
        }
    }
}

