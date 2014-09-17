package service;

import java.sql.SQLException;

import android.content.Context;

import com.example.timeestimation.User;

import dao.Dao;
import dao.MySQLiteHelper.PeriodCursor;

public class Service {
	
	private static Service instance = null;
	private Dao dao;
	private Context context;
	
	protected Service(Context context) {
		this.context = context;
		dao = new Dao(context);
	}
	
	public static Service getInstance(Context context){
		if(instance == null){
			instance = new Service(context);
		}
		return instance;
	}
	
	public void setUser(User user){
		dao.setLoggedIn(user);
	}
	
	public void addUser(User user){
		dao.addLoggedInUser(user);
	}
	
	public User getLoggedInUser(){
		return dao.getLoggedIn();
	}
	
	public boolean logIn(String initials, String password) throws SQLException{
		return dao.logIn(initials, password);
	}
	
	public PeriodCursor getPeriods(){
		return dao.getPeriod(getLoggedInUser().getInitials());
	}

}
