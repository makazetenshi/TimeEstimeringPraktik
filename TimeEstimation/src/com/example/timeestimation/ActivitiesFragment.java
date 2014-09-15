package com.example.timeestimation;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

public class ActivitiesFragment extends Fragment {
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.activities_screen, container, false);
		
		Button btnClasses = (Button)view.findViewById(R.id.btnClasses);
		Button btnOLC = (Button)view.findViewById(R.id.btnOLC);
		Button btnInt = (Button)view.findViewById(R.id.btnInternship);
		Button btnFinals = (Button)view.findViewById(R.id.btnFinals);
		Button btnExams = (Button)view.findViewById(R.id.btnExams);
		
		Button bntSave = (Button)view.findViewById(R.id.btnSave);
		
		bntSave.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new DayCalcFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		btnClasses.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new CourseFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		btnOLC.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new OLCFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		btnInt.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new InternshipFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		btnFinals.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new FinalsFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		btnExams.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new ExamFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		
		
		return view;
	}

}
