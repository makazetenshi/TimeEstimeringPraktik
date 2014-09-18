package com.example.timeestimation;

import java.sql.SQLException;

import service.Service;
import android.app.Fragment;
import android.app.FragmentTransaction;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

public class LoginFragment extends Fragment{
	
	private Service service;
	private Context context;
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		context = getActivity();
		View view = inflater.inflate(R.layout.login_screen, container, false);
		service = Service.getInstance(context);
		Button btnLogin = (Button)view.findViewById(R.id.btnLogin);
		final EditText etPass = (EditText)view.findViewById(R.id.etPass);
		final EditText etName = (EditText)view.findViewById(R.id.etName);
		
		
		btnLogin.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) { 
				//new LoginTask().execute(etName.getText().toString(), etPass.getText().toString());
				
				service.setUser(new User("TST", "1"));
				service.testData();
				
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new MenuFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		return view;
	}
	
	private class LoginTask extends AsyncTask<String, Void, Void>{

		@Override
		protected Void doInBackground(String... params) {
			String edName = params[0];
			String edPass = params[1];
			boolean foundUser = false;
			try {
				foundUser = service.logIn(edName, edPass);
			} catch (SQLException e) {
				e.printStackTrace();
			}
			if(foundUser){
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new MenuFragment());
				ft.addToBackStack(null);
				ft.commit();
			}else{
				Log.d("user", "User not found");
			}
			return null;
		}

	
	}
	
}	
	