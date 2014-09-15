package service;

import java.sql.SQLException;

import com.example.timeestimation.User;

import dao.Dao;

public class Service {
	
	private static Service instance = null;
	private Dao dao;
	
	protected Service() {
		dao = new Dao();
	}
	
	public static Service getInstance(){
		if(instance == null){
			instance = new Service();
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

}
