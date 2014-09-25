package com.example.timeestimation;

import java.util.Calendar;
import java.util.Date;

import model.DayActivity;
import model.Estimation;
import model.Exam;
import model.Period;
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

	private Service service = Service.getInstance(getActivity());
	
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
					service.addDayActivity(new DayActivity("ABS", Double.parseDouble(absence.getText().toString())));
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
	
	public int getDays(Date startDate, Date endDate){
		
		Calendar startCal;
		Calendar endCal;
		
		startCal = Calendar.getInstance();
		startCal.setTime(startDate);
		endCal = Calendar.getInstance();
		endCal.setTime(endDate);
		
		int workDays = 0;
		
		if(startCal.getTimeInMillis() == endCal.getTimeInMillis()){
			return 0;
		}

		if(startCal.getTimeInMillis() > endCal.getTimeInMillis()){
			startCal.setTime(endDate);
			endCal.setTime(startDate);
		}
		
		do{
			startCal.add(Calendar.DAY_OF_MONTH, 1);
			if(startCal.get(Calendar.DAY_OF_WEEK) != Calendar.SATURDAY
			&& startCal.get(Calendar.DAY_OF_WEEK) != Calendar.SUNDAY){
				++workDays;
			}
		} while (startCal.getTimeInMillis() < endCal.getTimeInMillis());
		
		return workDays;
	}
	
	public double estimateFormulaActivities(){
		double estimateTime = 0;
		
		for(int i = 0; i < service.getFormulaActivities().size(); i++){
			double hours = service.getFormulaActivities().get(i).getTime();
			String type = service.getFormulaActivities().get(i).getType();
			String education = service.getFormulaActivities().get(i).getEdu();
			double c = service.getConstant(type, education);
			double eng = 1;
			
			if(service.getFormulaActivities().get(i).isEnglish()){
				eng = service.getConstant("ENG", education);
			}
			
			Estimation estimation = new Estimation(type, education, 0);
			estimation.setTime(hours*c*eng);
			estimateTime += estimation.getTime();
			service.addEstimation(estimation);
		}
		return estimateTime;
	}
	
	public double estimateDayActivities(){
		double estimatedTime = 0;
		for(int i = 0; i < service.getDayActivities().size(); i++){
			
			Estimation estimation = new Estimation(service.getDayActivities().get(i).getType(),"NONE", service.getDayActivities().get(i).getDays());
			estimatedTime += estimation.getTime();
			service.addEstimation(estimation);
			
		}
		return estimatedTime;
	}
	
	public double meetingSet(){
		double meetings = getDays(service.getCurrentPeriod().getStartDate(), service.getCurrentPeriod().getEndDate())/100*9;
		service.setMeetings(meetings);
		return meetings;
	}
	
	public double exams(){
		double estimatedTime = 0;
		for(int i = 0; i < service.getExams().size(); i++){
			Exam exam = service.getExams().get(i);
			
			double[] constants = service.getExamConstant(exam.getExam(), exam.getEdu());
			
			estimatedTime += exam.getStudents()*(constants[0]+exam.getProjects())*(constants[1]+exam.getDays())*constants[2];
			
		}
		return estimatedTime;
	}
	
	public void setPeriod(){
		Period period = service.getCurrentPeriod();
		period.setEstimate(exams() + meetingSet() + estimateDayActivities() + estimateFormulaActivities());
		period.setNorm(getDays(period.getStartDate(), period.getEndDate()));
		period.setId(service.createPeriod(period));
		service.getCurrentPeriod().setId(period.getId());
		
	}
	
	public void setExams(){
		for(int i = 0; i < service.getExams().size(); i++){
			service.createExam(service.getExams().get(i));
		}
	}
	
	public void setEstimations(){
		for(int i = 0; i < service.getEstimations().size(); i++){
			service.createEstimation(service.getEstimations().get(i));
		}
	}
	
}
