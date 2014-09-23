package service;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;

import android.content.Context;

import com.example.timeestimation.Exam;
import com.example.timeestimation.FormulaActivity;
import com.example.timeestimation.Period;
import com.example.timeestimation.User;

import dao.Dao;
import dao.MySQLiteHelper.PeriodCursor;

public class Service {
	
	private static Service instance = null;
	private Dao dao;
	private Context context;
	
	protected Service(Context context) {
		this.context = context;
		dao = Dao.getInstance(context);
	}
	
	public static Service getInstance(Context context){
		if(instance == null){
			instance = new Service(context);
		}
		return instance;
	}
	
	public void testData(){
		dao.testData();
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
		return dao.getPeriod();
	}
	
	public Period getCurrentPeriod(){
		return dao.getCurrentPeriod();
	}
	
	public void setStartTime(Date startDate){
		dao.setStartTime(startDate);
	}
	
	public void setEndTime(Date endDate){
		dao.setEndTime(endDate);
	}
	
	public void addFormulaActivity(FormulaActivity fa){
		dao.addFormulaActivity(fa);
	}
	
	public ArrayList<FormulaActivity> getFormulaActivities(){
		return dao.getFormulaActivities();
	}
	
	public void addExam(Exam exam){
		dao.addExam(exam);
	}
	
	public ArrayList<Exam> getExams(){
		return dao.getExams();
	}

}
