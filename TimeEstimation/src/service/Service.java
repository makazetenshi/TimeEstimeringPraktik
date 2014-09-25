package service;

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
import android.content.Context;
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
	
	public void addDayActivity(DayActivity da){
		dao.addDayActivity(da);
	}
	
	public ArrayList<DayActivity> getDayActivities(){
		return dao.getDayActivities();
	}
	
	public EstimatedActivity getEstimated(){
		return dao.getEstimatedActivity();
	}
	
	public void addEstimation(Estimation estimation){
		dao.addEstimation(estimation);
	}
	
	public ArrayList<Estimation> getEstimations(){
		return dao.getEstimations();
	}
	
	public Estimation getEstimation(long id){
		return dao.cursorToEstimation(dao.getEst(id+""));
	}
	
	public double getConstant(String type, String education){
		return dao.cursorToConstant(dao.getConstant(type, education));
	}

	public double getMeetings(){
		return dao.getMeetings();
	}
	
	public void setMeetings(double meetings){
		dao.setMeetings(meetings);
	}
	
	public Exam getDBExams(){
		return dao.cursorToExam(dao.getDBExams());
	}
	
	public Exam getDBExam(String id){
		return dao.cursorToExam(dao.getDBExam(id));
	}
	
	public double[] getExamConstants(){
		return dao.cursorToExamConstant(dao.getExamConstants());
	}
	
	public double[] getExamConstant(String type, String edu){
		return dao.cursorToExamConstant(dao.getExamConstant(type,edu));
	}
	
	public void createExam(Exam exam){
		dao.createExam(exam);
	}
	
	public void createEstimation(Estimation estimation){
		dao.createEstimation(estimation);
	}
	
	public long createPeriod(Period period){
		return dao.createPeriod(period);
	}
	
}
