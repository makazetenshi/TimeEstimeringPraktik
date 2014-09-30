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

        private void createUserAdapterCommands()
        {
            dataAdapterUsers = new SqlDataAdapter();
            string sql = "SELECT * FROM person";
            SqlCommand selectcommand = new SqlCommand(sql, con);
            dataAdapterUsers.SelectCommand = selectcommand;

            SqlCommand updatecommand = new SqlCommand(
                    "UPDATE person "+
                    "SET initials = @init, password =@pass, firstname = @first, lastname = @last, admin = @admin " +
                    "WHERE initials = @oldinit", con);
            updatecommand.Parameters.Add("@init", SqlDbType.VarChar, 4, "initials");
            updatecommand.Parameters.Add("@pass", SqlDbType.VarChar, 15, "password");
            updatecommand.Parameters.Add("@first", SqlDbType.VarChar, 15, "firstname");
            updatecommand.Parameters.Add("@last", SqlDbType.VarChar, 15, "lastname");
            updatecommand.Parameters.Add("@admin", SqlDbType.Bit, 4, "admin");
            updatecommand.Parameters.Add("@oldinit", SqlDbType.VarChar, 4, "initials").SourceVersion = DataRowVersion.Original;
            dataAdapterUsers.UpdateCommand = updatecommand;

            SqlCommand deletecommand = new SqlCommand(
                    "DELETE FROM person " +
                    "WHERE initials = @init", con);
            deletecommand.Parameters.Add("@init", SqlDbType.VarChar, 4, "initials");
            dataAdapterUsers.DeleteCommand = deletecommand;

            SqlCommand insertcommand = new SqlCommand(
                    "INSERT INTO person " +
                    "VALUES(@init, @pass, @first, @last, @admin)", con);
            insertcommand.Parameters.Add("@init", SqlDbType.VarChar, 4, "initials");
            insertcommand.Parameters.Add("@pass", SqlDbType.VarChar, 15, "password");
            insertcommand.Parameters.Add("@first", SqlDbType.VarChar, 15, "firstname");
            insertcommand.Parameters.Add("@last", SqlDbType.VarChar, 15, "lastname");
            insertcommand.Parameters.Add("@admin", SqlDbType.Bit, 4, "admin");
            dataAdapterUsers.InsertCommand = insertcommand;

            
        }
        private void createDaysAdapterCommands()
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

            
        }
        private void createEstimateAdapterCommands()
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

            
        }
        private void createFormulaAdapterCommands()
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

            
        }
        private void createExamAdapterCommands()
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

            
        }
        private void createPeriodAdapterCommands()
        {
            dataAdapterPeriod = new SqlDataAdapter();
            string sql = "SELECT * FROM period";
            SqlCommand selectcommand = new SqlCommand(sql, con);
            dataAdapterPeriod.SelectCommand = selectcommand;
        }
        private void createOtherAdapterCommands()
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
        }

        public void updateDatabase()
        {
            int affectedRows = 0;
            con.Open();
            SqlTransaction tran = con.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                for (int i = 0; i < users.Rows.Count; i++ )
                    {
                        if (users.Rows[i].RowState == DataRowState.Modified || users.Rows[i].RowState == DataRowState.Added || users.Rows[i].RowState == DataRowState.Deleted)
                        {
                            dataAdapterUsers.UpdateCommand.Transaction = tran;
                            dataAdapterUsers.InsertCommand.Transaction = tran;
                            dataAdapterUsers.DeleteCommand.Transaction = tran;
                            affectedRows += dataAdapterUsers.Update(users);
                        }
                    }
                for (int i = 0; i < day.Rows.Count; i++)
                {
                    if (day.Rows[i].RowState == DataRowState.Modified || day.Rows[i].RowState == DataRowState.Added || day.Rows[i].RowState == DataRowState.Deleted)
                    {
                        dataAdapterDays.UpdateCommand.Transaction = tran;
                        dataAdapterDays.InsertCommand.Transaction = tran;
                        dataAdapterDays.DeleteCommand.Transaction = tran;
                        affectedRows += dataAdapterDays.Update(day);
                    }
                }
                for (int i = 0; i < estimate.Rows.Count; i++)
                {
                    if (estimate.Rows[i].RowState == DataRowState.Modified || estimate.Rows[i].RowState == DataRowState.Added || estimate.Rows[i].RowState == DataRowState.Deleted)
                    {
                        dataAdapterEstimate.UpdateCommand.Transaction = tran;
                        dataAdapterEstimate.InsertCommand.Transaction = tran;
                        dataAdapterEstimate.DeleteCommand.Transaction = tran;
                        affectedRows += dataAdapterEstimate.Update(estimate);
                    }
                }
                for (int i = 0; i < formula.Rows.Count; i++)
                {
                    if (formula.Rows[i].RowState == DataRowState.Modified || formula.Rows[i].RowState == DataRowState.Added || formula.Rows[i].RowState == DataRowState.Deleted)
                    {
                        dataAdapterFormula.UpdateCommand.Transaction = tran;
                        dataAdapterFormula.InsertCommand.Transaction = tran;
                        dataAdapterFormula.DeleteCommand.Transaction = tran;
                        affectedRows += dataAdapterFormula.Update(formula);
                    }
                }
                for (int i = 0; i < exam.Rows.Count; i++)
                {
                    if (exam.Rows[i].RowState == DataRowState.Modified || exam.Rows[i].RowState == DataRowState.Added || exam.Rows[i].RowState == DataRowState.Deleted)
                    {
                        dataAdapterExam.UpdateCommand.Transaction = tran;
                        dataAdapterExam.InsertCommand.Transaction = tran;
                        dataAdapterExam.DeleteCommand.Transaction = tran;
                        affectedRows += dataAdapterExam.Update(exam);
                    }
                }
                for (int i = 0; i < other.Rows.Count; i++)
                {
                    if (other.Rows[i].RowState == DataRowState.Modified || other.Rows[i].RowState == DataRowState.Added || other.Rows[i].RowState == DataRowState.Deleted)
                    {
                        dataAdapterOther.UpdateCommand.Transaction = tran;
                        dataAdapterOther.InsertCommand.Transaction = tran;
                        dataAdapterOther.DeleteCommand.Transaction = tran;
                        affectedRows += dataAdapterOther.Update(other);
                    }
                }

                tran.Commit();
                MessageBox.Show("Affected Rows("+affectedRows+")");
            }
            catch (Exception e)
            {
                tran.Rollback();
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        public void updateTables()
        {
            day.Clear();
            estimate.Clear();
            formula.Clear();
            exam.Clear();
            users.Clear();
            period.Clear();
            other.Clear();

            dataAdapterUsers.Fill(users);
            dataAdapterDays.Fill(day);
            dataAdapterEstimate.Fill(estimate);
            dataAdapterFormula.Fill(formula);
            dataAdapterExam.Fill(exam);
            dataAdapterPeriod.Fill(period);
            dataAdapterOther.Fill(other);
        }

        public void createAllAdapterCommands()
        {
            createDaysAdapterCommands();
            createEstimateAdapterCommands();
            createExamAdapterCommands();
            createFormulaAdapterCommands();
            createUserAdapterCommands();
            createPeriodAdapterCommands();
            createOtherAdapterCommands();
        }
    }
}

