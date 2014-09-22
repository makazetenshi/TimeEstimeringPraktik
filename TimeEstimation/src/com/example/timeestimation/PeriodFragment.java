package com.example.timeestimation;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

import service.Service;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.DialogFragment;
import android.app.FragmentTransaction;
import android.content.DialogInterface;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.widget.DatePicker;
import android.widget.Toast;

public class PeriodFragment extends DialogFragment implements DatePickerDialog.OnDateSetListener{
	
	private Service service = Service.getInstance(getActivity());
	
	@Override
	public Dialog onCreateDialog(Bundle savedInstanceState) {
//		String title = "Vælg Periode";
//		
//		AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
//		
//		LayoutInflater inflater = getActivity().getLayoutInflater();
//		
//		builder.setView(inflater.inflate(R.layout.new_screen, null)).setTitle(title)
//		.setPositiveButton(R.string.save, new DialogInterface.OnClickListener() {
//			@Override
//			public void onClick(DialogInterface dialog, int which) {
//				// TODO Auto-generated method stub
//				
//			}
//		});
//		
//		
//		return builder.create();
		
		final Calendar c = Calendar.getInstance();
		int year = c.get(Calendar.YEAR);
		int month = c.get(Calendar.MONTH);
		int day = c.get(Calendar.DAY_OF_MONTH);
		
		return new DatePickerDialog(getActivity(), this, year, month, day);
	}

	@Override
	public void onDateSet(DatePicker view, int year, int monthOfYear,
			int dayOfMonth) {
		// TODO Auto-generated method stub
		
		Calendar c = GregorianCalendar.getInstance();
		c.set(year, monthOfYear, dayOfMonth);
		c.set(Calendar.HOUR_OF_DAY, 0);
		c.set(Calendar.MINUTE, 0);
		c.set(Calendar.SECOND, 0);
		c.set(Calendar.MILLISECOND,0);
		
		Date date =  c.getTime();
		
		int duration = Toast.LENGTH_SHORT;
		Toast toast = Toast.makeText(getActivity(), date.toString(), duration);
		toast.show(); 
		if(service.getCurrentPeriod().getStartDate() != null){
			service.setEndTime(date);
			FragmentTransaction ft = getFragmentManager().beginTransaction();
			ft.replace(R.id.container, new ActivitiesFragment());
			ft.addToBackStack(null);
			ft.commit();
			Log.d("Set", "End");
		}else{
			service.setStartTime(date);
			Log.d("Set", "Start");
			DialogFragment newFragment = new PeriodFragment();
			newFragment.show(getFragmentManager(), "datePicker");
		}
	}
}
