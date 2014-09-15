package com.example.timeestimation;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

public class EstimationFragment extends Fragment{

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.estimation_screen, container, false);
		
		Button btnMenu = (Button)view.findViewById(R.id.btnMenu);
		
		btnMenu.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new MenuFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		return view;
	}
	
}
