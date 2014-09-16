package dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import com.example.timeestimation.User;

public class Dao {

	private User loggedIn;
	private ArrayList<User> loggedInUsers;
	private PreparedStatement preparedStatement = null;
	private ResultSet resultSet = null;
	private Connection connect;
	
	public Dao() {
		this.loggedIn = null;
		this.loggedInUsers = new ArrayList<User>();
		this.connect = null;
	}
	
	public boolean logIn(String initials, String password) throws SQLException{
		boolean foundUser = false;
		
		try {
			Class.forName("net.sourceforge.jtds.jdbc.Driver");
			
			String connection = "10.10.138.187";
			String port = "1433";
			String dbName = "praktik_estimate";
			
			connect = DriverManager.getConnection("jdbc:jtds:sqlserver://" + connection + ":" + port + ";databaseName=" + dbName + ";user=test;password=1234");
			
			preparedStatement = connect.prepareStatement("SELECT * FROM person WHERE init=? AND pass=?");
			preparedStatement.setString(1, initials);
			preparedStatement.setString(2, password);
			resultSet = preparedStatement.executeQuery();
			writeUser(resultSet);
			if(loggedInUsers.size() > 0){
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
	
private void writeUser(ResultSet resultSet) throws SQLException{
		User logUser = null;
		loggedInUsers.clear();
		
		while(resultSet.next()){
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
	
	public void addLoggedInUser(User user){
		loggedInUsers.add(user);
	}
	
	public ArrayList<User> getLoggedInUsers(){
		return loggedInUsers;
	}
	
}
