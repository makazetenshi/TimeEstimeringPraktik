package com.example.timeestimation;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;

public class CourseFragment extends Fragment {

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.classes_screen, container, false);
		
		final EditText etHours = (EditText)view.findViewById(R.id.classes_etHours);
		Button btnAdd = (Button)view.findViewById(R.id.classes_btnAdd);
		Button btnDone = (Button)view.findViewById(R.id.classes_btnDone);
		final CheckBox chkEng = (CheckBox)view.findViewById(R.id.chkEng);
		Spinner edu_spinner = (Spinner)view.findViewById(R.id.classes_edu);
		
		ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(getActivity(), R.array.Uddannelser, android.R.layout.simple_spinner_item);
		
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		
		edu_spinner.setAdapter(adapter);
		
		edu_spinner.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view,
					int position, long id) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				
			}
		});
		
		btnDone.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new ActivitiesFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		btnAdd.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FormulaActivity fa = new FormulaActivity();
				if(chkEng.isChecked()){
					fa.setEnglish(true);
				}
				fa.setTime(Integer.parseInt(etHours.getText().toString()));
				fa.setType("CLS");
			}
		});
		
		return view;
	}
	
}
