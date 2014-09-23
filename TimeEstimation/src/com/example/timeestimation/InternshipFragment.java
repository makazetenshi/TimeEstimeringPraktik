package com.example.timeestimation;

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

public class InternshipFragment extends Fragment{

	private String edu = "";
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.internship_screen, container, false);
		
		final Service service = Service.getInstance(getActivity());
		
		Button btnAdd = (Button)view.findViewById(R.id.int_btnAdd);
		Button btnDone = (Button)view.findViewById(R.id.int_btnDone);
		final EditText etHours = (EditText)view.findViewById(R.id.int_etHours);
		final Spinner int_spinner = (Spinner)view.findViewById(R.id.int_edu);
		
		final ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(getActivity(), R.array.Uddannelser, android.R.layout.simple_spinner_item);
		
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		
		int_spinner.setAdapter(adapter);
		
		int_spinner.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view,
					int position, long id) {
				edu = adapter.getItem(position).toString();
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
				FormulaActivity fa = new FormulaActivity();
				fa.setType("INT");
				fa.setTime(Integer.parseInt(etHours.getText().toString()));
				fa.setEdu(edu);
		
				int duration = Toast.LENGTH_SHORT;
				String text = "Aktivitet tilføjet";
				Toast toast = Toast.makeText(getActivity(), text, duration);
				toast.show();
				
				service.addFormulaActivity(fa);
				
				etHours.setText("");
				int_spinner.setSelection(0);
				
				Log.d("UV", "EDU: " + fa.getEdu() + " TYPE: " + fa.getType() + " ENG: " + fa.isEnglish() + " TIME: " + fa.getTime());
				Log.d("UV","Activity size: " + service.getFormulaActivities().size());
				
			}
		});
		
		return view;
	}
}
