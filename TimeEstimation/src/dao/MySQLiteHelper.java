package dao;


import java.util.Date;

import model.Estimation;
import model.Exam;
import model.Period;
import service.Service;
import android.content.Context;
import android.database.Cursor;
import android.database.CursorWrapper;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class MySQLiteHelper extends SQLiteOpenHelper  {
	
	public final static String DBNAME = "praktik_estimate.db";
	public final static int DBVERSION = 4;
	
	public final static String TABLE_PERIOD = "period";
	public final static String TABLE_EXAMS = "exams";
	public final static String TABLE_EXAM_CONSTANTS = "examconstants";
	public final static String TABLE_MEETING = "meeting";
	public final static String TABLE_EXAMPERIOD = "examperiod";
	public final static String TABLE_EXAM = "exam";
	public final static String TABLE_DAYPERIOD = "dayperiod";
	public final static String TABLE_DAYACTIVITY = "dayactivity";
	public final static String TABLE_ESTIMATEPERIOD = "estimateperiod";
	public final static String TABLE_ESTIMATEACTIVITY = "estimateactivity";
	public final static String TABLE_FORMULAPERIOD = "formulaperiod";
	public final static String TABLE_FORMULA = "formula";
	public final static String TABLE_ESTIMATION = "estimation";
	public final static String TABLE_CONSTANTS = "constants";
	
	public final static String COLUMN_PERIODID = "_id";
	public final static String COLUMN_STARTDATE = "startdate";
	public final static String COLUMN_ENDDATE = "enddate";
	public final static String COLUMN_INITIALS = "initials";
	public final static String COLUMN_NORM = "norm";
	
	public final static String COLUMN_PERIOD = "period";
	public final static String COLUMN_ESTACTIVITY = "estactivity";
	public final static String COLUMN_HOURS = "hours";
	
	public final static String COLUMN_ACTIVITY = "actvitity";
	
	public final static String COLUMN_EXAM = "exam";
	public final static String COLUMN_STUDENTS = "students";
	public final static String COLUMN_PROJECTS = "projects";
	public final static String COLUMN_DAYS = "days";
	
	public final static String COLUMN_NAME = "name";
	public final static String COLUMN_M1 = "m1";
	public final static String COLUMN_M2 = "m2";
	public final static String COLUMN_M3 = "m3";
	
	public final static String COLUMN_DAYACTIVITY = "dayactivity";
	
	public final static String COLUMN_FORMULA = "formula";
	public final static String COLUMN_VARIABLE = "variable";
	
	public final static String COLUMN_MULTIPLIER = "multiplier";
	public final static String COLUMN_IMPVARIABLE = "impvariable";
	
	public final static String COLUMN_TYPE = "type";
	public final static String COLUMN_EDU = "edu";
	public final static String COLUMN_EST = "estimation";
	
	public final static String COLUMN_CONSTANT = "constant";
	
	public final static String COLUMN_C1 = "c1";
	public final static String COLUMN_C2 = "c2";
	public final static String COLUMN_C3 = "c3";
	
	public final static String CREATE_TABLE_PERIOD = "CREATE TABLE IF NOT EXISTS " + TABLE_PERIOD + "(" + COLUMN_PERIODID + " integer primary key autoincrement, "
			+ COLUMN_STARTDATE + " date, " + COLUMN_ENDDATE + " date, " + COLUMN_INITIALS + " varchar(5), " + COLUMN_EST + " INTEGER, " + COLUMN_NORM + "INTEGER);";
	
	public final static String CREATE_TABLE_MEETING = "CREATE TABLE IF NOT EXISTS " + TABLE_MEETING + "(" + COLUMN_PERIOD + " integer primary key, " + COLUMN_HOURS + 
			" int, FOREIGN KEY(" + COLUMN_PERIOD + ") REFERENCES " + TABLE_PERIOD + "(" + COLUMN_PERIODID + "));";
	
	public final static String CREATE_TABLE_EXAMPERIOD = "CREATE TABLE IF NOT EXISTS " + TABLE_EXAMPERIOD + "(" + COLUMN_PERIOD + " integer, " + COLUMN_EXAM +
			COLUMN_STUDENTS + " integer, " + COLUMN_PROJECTS + " integer, " + COLUMN_DAYS + " integer, FOREIGN KEY(" + COLUMN_PERIOD + ") REFERENCES "
			+ TABLE_PERIOD + "(" + COLUMN_PERIODID + "), FOREIGN KEY(" + COLUMN_EXAM + ") REFERENCES " + TABLE_EXAM + "(" + COLUMN_NAME + "));";
	
	public final static String CREATE_TABLE_EXAM = "CREATE TABLE IF NOT EXISTS " + TABLE_EXAM + "(" + COLUMN_NAME + " varchar(20) primary key, " + COLUMN_M1 + 
			" double, " + COLUMN_M2 + " double, " + COLUMN_M3 + " double);";
	
	public final static String CREATE_TABLE_ESTIMATEPERIOD = "CREATE TABLE IF NOT EXISTS " + TABLE_ESTIMATEPERIOD + "(" + COLUMN_PERIOD + " integer, " + 
			COLUMN_ESTACTIVITY + " varchar(20), " + COLUMN_HOURS + " double, FOREIGN KEY(" + COLUMN_PERIOD + ") REFERENCES " + TABLE_PERIOD
			+ "(" + COLUMN_PERIODID + "), FOREIGN KEY(" + COLUMN_ESTACTIVITY + ") REFERENCES " + TABLE_ESTIMATEACTIVITY + "(" + COLUMN_ACTIVITY + "));";

	public final static String CREATE_TABLE_ESTIMATEACTIVITY = "CREATE TABLE IF NOT EXISTS " + TABLE_ESTIMATEACTIVITY + "(" + COLUMN_ACTIVITY + " varchar(20) primary key);";
	
	public final static String CREATE_TABLE_DAYPERIOD = "CREATE TABLE IF NOT EXISTS " + TABLE_DAYPERIOD + "(" + COLUMN_PERIOD + " integer, " + COLUMN_DAYACTIVITY 
			+ " varchar(20), " + COLUMN_DAYS + " integer, FOREIGN KEY(" + COLUMN_PERIOD + ") REFERENCES " + TABLE_PERIOD + "(" + COLUMN_PERIODID + "),"
					+ " FOREIGN KEY(" + COLUMN_DAYACTIVITY + ") REFERENCES " + TABLE_DAYACTIVITY + "(" + COLUMN_ACTIVITY + "));";
	
	public final static String CREATE_TABLE_DAYACTIVITIES = "CREATE TABLE IF NOT EXISTS " + TABLE_DAYACTIVITY + "(" + COLUMN_ACTIVITY + " varchar(20));";
	
	public final static String CREATE_TABLE_FORMULAPERIOD = "CREATE TABLE IF NOT EXISTS " + TABLE_FORMULAPERIOD + "(" + COLUMN_PERIOD + " integer, " + 
	COLUMN_FORMULA + " varchar(30), " + COLUMN_VARIABLE + " double, FOREIGN KEY(" + COLUMN_PERIOD + ") REFERENCES " + TABLE_PERIOD + "(" +
			COLUMN_PERIODID + "), FOREIGN KEY (" + COLUMN_FORMULA + ") REFERENCES " + TABLE_FORMULA + "(" + COLUMN_NAME + "));";
	
	public final static String CREATE_TABLE_FORMULA = "CREATE TABLE IF NOT EXISTS " + TABLE_FORMULA + "(" + COLUMN_NAME + " varchar(30), " + COLUMN_MULTIPLIER 
			+ " double, " + COLUMN_IMPVARIABLE  + " varchar(20));";
	
	public final static String CREATE_TABLE_ESTIMATION = "CREATE TABLE IF NOT EXISTS " + TABLE_ESTIMATION + "(" + COLUMN_PERIODID + " INTEGER, " +
	COLUMN_TYPE + " varchar(20), " +  COLUMN_EDU + " varchar(20), " + COLUMN_EST + " decimal, PRIMARY KEY (" + COLUMN_PERIODID + "));";
	
	public final static String CREATE_TABLE_CONSTANT = "CREATE TABLE IF NOT EXISTS " + TABLE_CONSTANTS + "(" + COLUMN_EDU + " varchar(20), " +
	COLUMN_TYPE + " varchar(20), " + COLUMN_CONSTANT + " decimal, PRIMARY KEY (" + COLUMN_EDU + ", " + COLUMN_TYPE + "));";
	
	public final static String CREATE_TABLE_EXAMS = "CREATE TABLE IF NOT EXISTS " + TABLE_EXAMS + "(" + COLUMN_PERIODID + " INTEGER, " +
	COLUMN_TYPE + " VARCHAR(20), " + COLUMN_EDU + " VARCHAR(20), " + COLUMN_STUDENTS + " INTEGER, " + COLUMN_PROJECTS + " INTEGER, " +
			COLUMN_DAYS + " INTEGER, " + COLUMN_EST + " DECIMAL, PRIMARY KEY (" + COLUMN_PERIODID + "));";
	
	public final static String CREATE_TABLE_EXAMCONSTANTS = "CREATE TABLE IF NOT EXISTS " + TABLE_EXAM_CONSTANTS + "(" + COLUMN_TYPE +
			" VARCHAR(20), " + COLUMN_EDU + " VARCHAR(20), " + COLUMN_C1 + " DECIMAL, " + COLUMN_C2 + " DECIMAL, " + COLUMN_C3 + " DECIMAL, " +
			"PRIMARY KEY (" + COLUMN_TYPE + "," + COLUMN_EDU + "));";
	
	public MySQLiteHelper(Context context) {
		super(context, DBNAME, null, DBVERSION);
		// TODO Auto-generated constructor stub
	}

	@Override
	public void onCreate(SQLiteDatabase db) {
		db.execSQL(CREATE_TABLE_PERIOD);
		db.execSQL(CREATE_TABLE_ESTIMATION);
		db.execSQL(CREATE_TABLE_CONSTANT);
		db.execSQL(CREATE_TABLE_EXAMS);
		db.execSQL(CREATE_TABLE_EXAMCONSTANTS);
		//db.execSQL(CREATE_TABLE_MEETING);
		//db.execSQL(CREATE_TABLE_ESTIMATEPERIOD);
//		db.execSQL(CREATE_TABLE_EXAM);
//		db.execSQL(CREATE_TABLE_FORMULAPERIOD);
//		db.execSQL(CREATE_TABLE_FORMULA);
//		db.execSQL(CREATE_TABLE_ESTIMATEPERIOD);
//		db.execSQL(CREATE_TABLE_ESTIMATEACTIVITY);
//		db.execSQL(CREATE_TABLE_DAYPERIOD);
//		db.execSQL(CREATE_TABLE_DAYACTIVITIES);
	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_DAYACTIVITY);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_DAYPERIOD);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_ESTIMATEACTIVITY);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_ESTIMATEPERIOD);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_EXAM);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_EXAMPERIOD);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_FORMULA);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_FORMULAPERIOD);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_MEETING);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_PERIOD);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_CONSTANTS);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_ESTIMATION);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_EXAMS);
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_EXAM_CONSTANTS);
		onCreate(db);
	}
	
	public PeriodCursor queryPeriods(){
		Cursor wrapped = getReadableDatabase().query(TABLE_PERIOD, null, null, null, null, null, null);
		return new PeriodCursor(wrapped);
	}
	
	public PeriodCursor queryPeriod(String init){
		Log.d("Cursor", "Query Initiated");
		String[] args = {init};
		Cursor wrapped = getReadableDatabase().query(TABLE_PERIOD, null, COLUMN_INITIALS + " = ?", args, null, null, null);
		return new PeriodCursor(wrapped);
	}
	
	public EstimationCursor queryEstimations(){
		Cursor wrapped = getReadableDatabase().query(TABLE_ESTIMATION, null, null, null, null, null, null);
		return new EstimationCursor(wrapped);
	}
	
	public EstimationCursor queryEstimation(String id){
		String[] args = {id};
		Cursor wrapped = getReadableDatabase().query(TABLE_ESTIMATION, null, COLUMN_PERIODID + " = ?", args, null, null, null);
		return new EstimationCursor(wrapped);
	}
	
	public Cursor queryConstant(String type, String education){
		String[] args = {education, type};
		final String sql = "SELECT " + COLUMN_CONSTANT + " FROM " + TABLE_CONSTANTS + " WHERE " + 
		COLUMN_EDU + " = ?" + " AND " + COLUMN_EDU + " = ?";
		//Cursor cursor = getReadableDatabase().query(TABLE_CONSTANTS, null, COLUMN_EDU + " = ? AND " + COLUMN_TYPE + " = ?", args, null, null, null);
		Cursor cursor = getReadableDatabase().rawQuery(sql, args);
		return cursor;
	}
	
	public ExamCursor queryExams(){
		Cursor wrapped = getReadableDatabase().query(TABLE_EXAMS, null, null, null, null, null, null);
		return new ExamCursor(wrapped);
	}
	
	public ExamCursor queryExam(String id){
		String[] args = {id};
		Cursor wrapped = getReadableDatabase().query(TABLE_EXAMS, null, COLUMN_PERIODID + " = ?", args, null, null, null);
		return new ExamCursor(wrapped);
	}
	
	public Cursor queryExamConstants(){
		Cursor wrapped = getReadableDatabase().query(TABLE_EXAM_CONSTANTS, null, null, null, null, null, null);
		return new ExamConstantsCursor(wrapped);
	}
	
	public Cursor queryExamConstant(String type, String edu){
		String[] args = {type, edu};
		Cursor wrapped = getReadableDatabase().query(TABLE_EXAM_CONSTANTS, null, COLUMN_TYPE + " = ? AND " + COLUMN_EDU + " = ?", args, null, null, null);
		return new ExamConstantsCursor(wrapped);
	}
	

	public static class PeriodCursor extends CursorWrapper{

		public PeriodCursor(Cursor cursor) {
			super(cursor);
			// TODO Auto-generated constructor stub
		}
		
		public Period getPeriod(){
			if(isBeforeFirst()||isAfterLast()){
				return null;
			}
			//Service service = Service.getInstance();
			Period period = new Period();
			period.setId(getLong(getColumnIndex(COLUMN_PERIODID)));
			period.setStartDate(new Date(getLong(getColumnIndex(COLUMN_STARTDATE))));
			period.setEndDate(new Date(getLong(getColumnIndex(COLUMN_ENDDATE))));
			//period.setLoggedIn(service.getLoggedInUser().getInitials());
			
			return period;
		}
		
	}
	
	public static class EstimationCursor extends CursorWrapper{
		
		public EstimationCursor(Cursor cursor) {
			super(cursor);
		}
		
		public Estimation getEstimation(){
			if(isBeforeFirst()||isAfterLast()){
				return null;
			}
			Estimation estimation = new Estimation(null, null, 0);
			estimation.setEducation(getString(getColumnIndex(COLUMN_EDU)));
			estimation.setType(getString(getColumnIndex(COLUMN_TYPE)));
			estimation.setTime(getDouble(getColumnIndex(COLUMN_EST)));
			
			return estimation;
		}
		
	}
	
	public static class ExamCursor extends CursorWrapper{
		
		public ExamCursor(Cursor cursor){
			super(cursor);
		}
		
		public Exam getExam(){
			if(isBeforeFirst()||isAfterLast()){
				return null;
			}
			Exam exam = new Exam();
			exam.setExam(getString(getColumnIndex(COLUMN_TYPE)));
			exam.setEdu(getString(getColumnIndex(COLUMN_EDU)));
			exam.setStudents(getInt(getColumnIndex(COLUMN_STUDENTS)));
			exam.setProjects(getInt(getColumnIndex(COLUMN_PROJECTS)));
			exam.setDays(getInt(getColumnIndex(COLUMN_DAYS)));
			
			return exam;
		}
		
	}
	
	public static class ExamConstantsCursor extends CursorWrapper{
		
		public ExamConstantsCursor(Cursor cursor){
			super(cursor);
		}
		
		public double[] getExamConstants(){
			if(isBeforeFirst()||isAfterLast()){
				return null;
			}
			double[] constants = new double[3];
			constants[0] = getDouble(getColumnIndex(COLUMN_C1));
			constants[1] = getDouble(getColumnIndex(COLUMN_C2));
			constants[2] = getDouble(getColumnIndex(COLUMN_C3));
			
			return constants;
		}
	}
	
	
	
}
