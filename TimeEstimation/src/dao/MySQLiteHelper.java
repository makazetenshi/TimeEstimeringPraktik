package dao;


import java.util.Date;

import service.Service;

import com.example.timeestimation.Period;

import android.content.Context;
import android.database.Cursor;
import android.database.CursorWrapper;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class MySQLiteHelper extends SQLiteOpenHelper  {
	
	public final static String DBNAME = "praktik_estimate.db";
	public final static int DBVERSION = 3;
	
	public final static String TABLE_PERIOD = "period";
	public final static String TABLE_MEETING = "meeting";
	public final static String TABLE_EXAMPERIOD = "examperiod";
	public final static String TABLE_EXAM = "exam";
	public final static String TABLE_DAYPERIOD = "dayperiod";
	public final static String TABLE_DAYACTIVITY = "dayactivity";
	public final static String TABLE_ESTIMATEPERIOD = "estimateperiod";
	public final static String TABLE_ESTIMATEACTIVITY = "estimateactivity";
	public final static String TABLE_FORMULAPERIOD = "formulaperiod";
	public final static String TABLE_FORMULA = "formula";
	
	public final static String COLUMN_PERIODID = "_id";
	public final static String COLUMN_STARTDATE = "startdate";
	public final static String COLUMN_ENDDATE = "enddate";
	public final static String COLUMN_INITIALS = "initials";
	
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
	
	public final static String CREATE_TABLE_PERIOD = "CREATE TABLE IF NOT EXISTS " + TABLE_PERIOD + "(" + COLUMN_PERIODID + " integer primary key autoincrement, "
			+ COLUMN_STARTDATE + " date, " + COLUMN_ENDDATE + " date, " + COLUMN_INITIALS + " varchar(5));";
	
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
	
	public MySQLiteHelper(Context context) {
		super(context, DBNAME, null, DBVERSION);
		// TODO Auto-generated constructor stub
	}

	@Override
	public void onCreate(SQLiteDatabase db) {
		db.execSQL(CREATE_TABLE_PERIOD);
		db.execSQL(CREATE_TABLE_MEETING);
		db.execSQL(CREATE_TABLE_ESTIMATEPERIOD);
		db.execSQL(CREATE_TABLE_EXAM);
		db.execSQL(CREATE_TABLE_FORMULAPERIOD);
		db.execSQL(CREATE_TABLE_FORMULA);
		db.execSQL(CREATE_TABLE_ESTIMATEPERIOD);
		db.execSQL(CREATE_TABLE_ESTIMATEACTIVITY);
		db.execSQL(CREATE_TABLE_DAYPERIOD);
		db.execSQL(CREATE_TABLE_DAYACTIVITIES);
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
	
	
}
