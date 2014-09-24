package com.example.timeestimation;

import model.FormulaActivity;
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
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

public class CourseFragment extends Fragment {
	
	private String edu = "";

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.classes_screen, container, false);
		
		final Service service = Service.getInstance(getActivity());
		
		final EditText etHours = (EditText)view.findViewById(R.id.classes_etHours);
		Button btnAdd = (Button)view.findViewById(R.id.classes_btnAdd);
		Button btnDone = (Button)view.findViewById(R.id.classes_btnDone);
		final CheckBox chkEng = (CheckBox)view.findViewById(R.id.chkEng);
		final Spinner edu_spinner = (Spinner)view.findViewById(R.id.classes_edu);
		
		final ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(getActivity(), R.array.Uddannelser, android.R.layout.simple_spinner_item);
		
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		
		edu_spinner.setAdapter(adapter);
		
		edu_spinner.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view,
					int position, long id) {
				// TODO Auto-generated method stub
				edu = adapter.getItem(position).toString();
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
				fa.setEdu(edu);
				service.addFormulaActivity(fa);
				
				int duration = Toast.LENGTH_SHORT;
				String text = "Aktivitet tilføjet";
				Toast toast = Toast.makeText(getActivity(), text, duration);
				toast.show();
				
				etHours.setText("");
				chkEng.setChecked(false);
				edu_spinner.setSelection(0);
				
				Log.d("UV", "EDU: " + fa.getEdu() + " TYPE: " + fa.getType() + " ENG: " + fa.isEnglish() + " TIME: " + fa.getTime());
				Log.d("UV","Activity size: " + service.getFormulaActivities().size());
			}
		});
		
		return view;
	}
	
}
