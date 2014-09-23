package com.example.timeestimation;

import service.Service;
import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

public class DayCalcFragment extends Fragment{

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.day_screen, container, false);
		
		final Service service = Service.getInstance(getActivity());
		
		Button btnSave = (Button)view.findViewById(R.id.day_btnSave);
		final EditText holiday = (EditText)view.findViewById(R.id.day_etHoliday);
		final EditText vacation = (EditText)view.findViewById(R.id.day_etVaction);
		final EditText absence = (EditText)view.findViewById(R.id.day_etAbsence);
		final EditText timeEst = (EditText)view.findViewById(R.id.day_etTimeEst);
		
		btnSave.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				
				if(!holiday.getText().toString().isEmpty()){
					service.addDayActivity(new DayActivity("HOL", Double.parseDouble(holiday.getText().toString())));
				}
				if(!vacation.getText().toString().isEmpty()){
					service.addDayActivity(new DayActivity("VAC", Double.parseDouble(vacation.getText().toString())));
				}
				if(!absence.getText().toString().isEmpty()){					
					service.addDayActivity(new DayActivity("", Double.parseDouble(absence.getText().toString())));
				}
				if(!timeEst.getText().toString().isEmpty()){
				service.getEstimated().setTime(Double.parseDouble(timeEst.getText().toString()));
				}
				
				Log.d("IT", "ITEMS: " + service.getDayActivities().size());
				
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new EstimationFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		return view;
	}
	
}
