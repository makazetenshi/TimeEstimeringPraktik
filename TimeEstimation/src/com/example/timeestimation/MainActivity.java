package com.example.timeestimation;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

import android.os.Bundle;
import android.app.Activity;
import android.util.Log;
import android.view.Menu;

public class MainActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.activity_main);
		
		if(savedInstanceState == null){
			getFragmentManager().beginTransaction().add(R.id.container, new LoginFragment()).commit();
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
	
	public void connectToDatabase() throws SQLException{
		try {
			
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
		
			String connection = "127.0.0.1";
			String port = "1433";
			String dbName = "defaultName";
			
			Connection DbConn = DriverManager.getConnection("jdbc:sqlserver://" + connection + ":" + port + ";databaseName=" + dbName);
			
			
			
			
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
