package com.example.timeestimation;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

public class ExamFragment extends Fragment {

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.exam_screen, container, false);
		
		Button btnAdd = (Button)view.findViewById(R.id.exam_add);
		Button btnDone = (Button)view.findViewById(R.id.exam_done);
		
		btnDone.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new ActivitiesFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		return view;
	}
	
}
