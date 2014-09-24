package com.example.timeestimation;

import model.Exam;
import service.Service;
import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

public class ExamFragment extends Fragment {

	private String edu = "";
	private String exm = "";
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.exam_screen, container, false);
		
		final Service service = Service.getInstance(getActivity());
		
		Button btnAdd = (Button)view.findViewById(R.id.exam_add);
		Button btnDone = (Button)view.findViewById(R.id.exam_done);
		final Spinner exam_edu = (Spinner)view.findViewById(R.id.exam_edu);
		final Spinner exam_exam = (Spinner)view.findViewById(R.id.exam_exam);
		final EditText etStud = (EditText)view.findViewById(R.id.exam_etStudents);
		final EditText etProj = (EditText)view.findViewById(R.id.exam_etProjects);
		final EditText etDays = (EditText)view.findViewById(R.id.exam_etDays);

		final ArrayAdapter<CharSequence> edu_adapter = ArrayAdapter.createFromResource(getActivity(), R.array.Uddannelser, android.R.layout.simple_spinner_item);
		
		edu_adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		
		final ArrayAdapter<CharSequence> exam_adapter = ArrayAdapter.createFromResource(getActivity(), R.array.Eksamener, android.R.layout.simple_spinner_item);
		
		exam_adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		
		exam_exam.setAdapter(exam_adapter);
		exam_edu.setAdapter(edu_adapter);
		
		exam_exam.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view,
					int position, long id) {
				exm = exam_adapter.getItem(position).toString();
			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub
				
			}
		});
		
		exam_edu.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view,
					int position, long id) {
				edu = edu_adapter.getItem(position).toString();
			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub
				
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
				Exam exam = new Exam();
				exam.setDays(Integer.parseInt(etDays.getText().toString()));
				exam.setEdu(edu);
				exam.setExam(exm);
				exam.setProjects(Integer.parseInt(etProj.getText().toString()));
				exam.setStudents(Integer.parseInt(etStud.getText().toString()));
				
				service.addExam(exam);
				
				int duration = Toast.LENGTH_SHORT;
				String text = "Eksamen tilføjet";
				Toast toast = Toast.makeText(getActivity(), text, duration);
				toast.show();
				
				
				etDays.setText("");
				etProj.setText("");
				etStud.setText("");
				exam_edu.setSelection(0);
				exam_exam.setSelection(0);
				
				Log.d("EX", "EDU: " + exam.getEdu() + " EXM: " + exam.getExam() + " PROJ: " + exam.getProjects() + " STUD: " + exam.getStudents() + " DAYS: " + exam.getDays());
				Log.d("EX", "Exams: " + service.getExams().size());
				
			}
		});
		
		return view;
	}
	
}
