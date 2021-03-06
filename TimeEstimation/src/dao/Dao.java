package dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;

import model.DayActivity;
import model.EstimatedActivity;
import model.Estimation;
import model.Exam;
import model.FormulaActivity;
import model.Period;
import model.User;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;
import dao.MySQLiteHelper.EstimationCursor;
import dao.MySQLiteHelper.ExamCursor;
import dao.MySQLiteHelper.PeriodCursor;

public class Dao {

	private User loggedIn;
	private EstimatedActivity estimated;
	private ArrayList<User> loggedInUsers;
	private ArrayList<FormulaActivity> formulaActivities;
	private ArrayList<DayActivity> dayActivities;
	private ArrayList<Exam> exams;
	private ArrayList<Estimation> estimations;
	private PreparedStatement preparedStatement = null;
	private ResultSet resultSet = null;
	private Connection connect;
	private SQLiteDatabase database;
	private MySQLiteHelper dbHelper;
	private String[] testData = { MySQLiteHelper.COLUMN_INITIALS };
	private static Dao instance = null;
	private Period currentPeriod;
	private double meetings;

	protected Dao(Context context) {
		this.loggedIn = null;
		this.loggedInUsers = new ArrayList<User>();
		this.connect = null;
		dbHelper = new MySQLiteHelper(context);
		currentPeriod =  new Period();
		this.formulaActivities = new ArrayList<FormulaActivity>();
		this.exams = new ArrayList<Exam>();
		this.dayActivities = new ArrayList<DayActivity>();
		this.estimated = new EstimatedActivity();
		this.estimations = new ArrayList<Estimation>();
		this.meetings = 0;
	}

	public static Dao getInstance(Context context) {
		if (instance == null) {
			instance = new Dao(context);
		}
		return instance;
	}

	public void testData() {
		Period p = new Period();
		open();
		Cursor cursor = database.query(
				MySQLiteHelper.TABLE_PERIOD,
				testData,
				MySQLiteHelper.COLUMN_INITIALS + " = '"
						+ loggedIn.getInitials() + "'", null, null, null, null);
		cursor.moveToFirst();
		if (cursor.getCount() > 0) {
			Log.d("Cursor",
					"Found matches for initials: " + loggedIn.getInitials());
			Log.d("Cursor", "Cursor has: " + cursor.getColumnCount() + " columns.");
			p.setInitials("N/A");
		}
		cursor.close();
		if (p.getInitials() == null) {
			Log.d("Cursor", "P had no matches");
			ContentValues values = new ContentValues();
			values.put(MySQLiteHelper.COLUMN_STARTDATE,
					System.currentTimeMillis());
			values.put(MySQLiteHelper.COLUMN_ENDDATE, 1419405715);
			values.put(MySQLiteHelper.COLUMN_INITIALS, loggedIn.getInitials());
			database.insert(MySQLiteHelper.TABLE_PERIOD, null, values);
		}
		close();
	}
	
	public long createPeriod(Period period){
		long id = 0;
		open();
		ContentValues values = new ContentValues();
		values.put(MySQLiteHelper.COLUMN_STARTDATE, period.getStartDate().toString());
		values.put(MySQLiteHelper.COLUMN_ENDDATE, period.getEndDate().toString());
		values.put(MySQLiteHelper.COLUMN_INITIALS, period.getInitials());
		values.put(MySQLiteHelper.COLUMN_EST, period.getEstimate());
		values.put(MySQLiteHelper.COLUMN_NORM, period.getNorm());
		
		id = database.insert(MySQLiteHelper.TABLE_PERIOD, null, values);
		close();
		
		return id;
	}
	
	public void createEstimation(Estimation estimation){
		open();
		ContentValues values = new ContentValues();
		values.put(MySQLiteHelper.COLUMN_PERIODID, estimation.getId());
		values.put(MySQLiteHelper.COLUMN_TYPE, estimation.getType());
		values.put(MySQLiteHelper.COLUMN_EDU, estimation.getEducation());
		values.put(MySQLiteHelper.COLUMN_EST, estimation.getTime());
		
		database.insert(MySQLiteHelper.TABLE_ESTIMATION, null, values);
		
		close();
	}
	
	public void createExam(Exam exam){
		open();
		ContentValues values = new ContentValues();
		values.put(MySQLiteHelper.COLUMN_PERIODID, exam.getId());
		values.put(MySQLiteHelper.COLUMN_TYPE, exam.getExam());
		values.put(MySQLiteHelper.COLUMN_EDU, exam.getEdu());
		values.put(MySQLiteHelper.COLUMN_STUDENTS, exam.getStudents());
		values.put(MySQLiteHelper.COLUMN_PROJECTS, exam.getProjects());
		values.put(MySQLiteHelper.COLUMN_DAYS, exam.getDays());
		values.put(MySQLiteHelper.COLUMN_EST, exam.getEstimation());
		
		database.insert(MySQLiteHelper.TABLE_EXAM, null, values);
		
		close();
	}

	public void open() {
		database = dbHelper.getWritableDatabase();
	}

	public void close() {
		dbHelper.close();
	}

	public boolean logIn(String initials, String password) throws SQLException {
		boolean foundUser = false;

		try {
			Class.forName("net.sourceforge.jtds.jdbc.Driver");

			String connection = "10.10.137.145";
			String port = "1433";
			String dbName = "praktik_estimate";

			connect = DriverManager.getConnection("jdbc:jtds:sqlserver://"
					+ connection + ":" + port + "/" + dbName
					+ ";instance=SQLEXPRESS;user=test;password=1234");

			preparedStatement = connect
					.prepareStatement("SELECT * FROM person WHERE init=? AND pass=?");
			preparedStatement.setString(1, initials);
			preparedStatement.setString(2, password);
			resultSet = preparedStatement.executeQuery();
			writeUser(resultSet);
			if (loggedInUsers.size() > 0) {
				loggedIn = loggedInUsers.get(0);
				foundUser = true;
			}

		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		connect.close();
		preparedStatement.close();
		loggedInUsers = new ArrayList<User>();

		return foundUser;
	}

	private void writeUser(ResultSet resultSet) throws SQLException {
		User logUser = null;
		loggedInUsers.clear();

		while (resultSet.next()) {
			String name = resultSet.getString("name");
			String init = resultSet.getString("init");
			String pass = resultSet.getString("pass");

			logUser = new User(init, pass);
			logUser.setName(name);
			loggedInUsers.add(logUser);
		}
	}

	public User getLoggedIn() {
		return loggedIn;
	}

	public void setLoggedIn(User loggedIn) {
		this.loggedIn = loggedIn;
	}

	public void addLoggedInUser(User user) {
		loggedInUsers.add(user);
	}

	public ArrayList<User> getLoggedInUsers() {
		return loggedInUsers;
	}

	public PeriodCursor getPeriod() {
		return dbHelper.queryPeriods();
	}
	
	public EstimationCursor getEstimation(){
		return dbHelper.queryEstimations();
	}
	
	public EstimationCursor getEst(String id){
		return dbHelper.queryEstimation(id);
	}
	
	public Cursor getConstant(String type, String education){
		return dbHelper.queryConstant(type, education);
	}
	
	public ExamCursor getDBExams(){
		return dbHelper.queryExams();
	}
	
	public ExamCursor getDBExam(String id){
		return dbHelper.queryExam(id);
	}
	
	public Cursor getExamConstants(){
		return dbHelper.queryExamConstants();
	}
	
	public Cursor getExamConstant(String type, String edu){
		return dbHelper.queryExamConstant(type,edu);
	}
	
	public Estimation cursorToEstimation(Cursor cursor){
		Estimation estimation = new Estimation(null, null, 0);
		estimation.setType(cursor.getString(1));
		estimation.setEducation(cursor.getString(2));
		estimation.setTime(cursor.getDouble(3));
		
		return estimation;
	}

	public Period cursorToPeriod(Cursor cursor) {
		Period period = new Period();
		period.setId(cursor.getInt(0));
		period.setStartDate(new Date(cursor.getLong(1) * 1000));
		period.setEndDate(new Date(cursor.getLong(2) * 1000));
		period.setInitials(cursor.getString(3));
		return period;
	}
	
	public Exam cursorToExam(Cursor cursor){
		Exam exam = new Exam();
		exam.setExam(cursor.getString(0));
		exam.setEdu(cursor.getString(1));
		exam.setStudents(cursor.getInt(2));
		exam.setProjects(cursor.getInt(3));
		exam.setDays(cursor.getInt(4));
		
		return exam;
	}
	
	public double cursorToConstant(Cursor cursor){
		double k = 0;
		k = cursor.getDouble(0);
		return k;
	}
	
	public double[] cursorToExamConstant(Cursor cursor){
		
		double[] constants = new double[3];
		
		constants[0] = cursor.getDouble(0);
		constants[1] = cursor.getDouble(1);
		constants[2] = cursor.getDouble(2);
		
		return constants;
	}
	
	public void setStartTime(Date startDate){
		currentPeriod.setStartDate(startDate);
	}
	
	public void setEndTime(Date endDate){
		
	}
	
	public Period getCurrentPeriod(){
		return currentPeriod;
	}
	
	public void addFormulaActivity(FormulaActivity fa){
		formulaActivities.add(fa);
	}
	
	public ArrayList<FormulaActivity> getFormulaActivities(){
		return formulaActivities;
	}
	
	public void addExam(Exam exam){
		exams.add(exam);
	}
	
	public ArrayList<Exam> getExams(){
		return exams;
	}
	
	public void addDayActivity(DayActivity da){
		dayActivities.add(da);
	}
	
	public ArrayList<DayActivity> getDayActivities(){
		return dayActivities;
	}
	
	public EstimatedActivity getEstimatedActivity(){
		return estimated;
	}
	
	public void addEstimation(Estimation estimation){
		estimations.add(estimation);
	}
	
	public ArrayList<Estimation> getEstimations(){
		return estimations;
	}

	public double getMeetings() {
		return meetings;
	}

	public void setMeetings(double meetings) {
		this.meetings = meetings;
	}
	
}
