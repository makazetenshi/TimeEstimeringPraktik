package dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;

import com.example.timeestimation.Period;
import com.example.timeestimation.User;

import dao.MySQLiteHelper.PeriodCursor;

public class Dao {

	private User loggedIn;
	private ArrayList<User> loggedInUsers;
	private PreparedStatement preparedStatement = null;
	private ResultSet resultSet = null;
	private Connection connect;
	private SQLiteDatabase database;
	private MySQLiteHelper dbHelper;
	private String[] testData = { MySQLiteHelper.COLUMN_INITIALS };
	private static Dao instance = null;

	protected Dao(Context context) {
		this.loggedIn = null;
		this.loggedInUsers = new ArrayList<User>();
		this.connect = null;
		dbHelper = new MySQLiteHelper(context);
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

	public Period cursorToPeriod(Cursor cursor) {
		Period period = new Period();
		period.setId(cursor.getInt(0));
		period.setStartDate(new Date(cursor.getLong(1) * 1000));
		period.setEndDate(new Date(cursor.getLong(2) * 1000));
		period.setInitials(cursor.getString(3));
		return period;
	}

}
