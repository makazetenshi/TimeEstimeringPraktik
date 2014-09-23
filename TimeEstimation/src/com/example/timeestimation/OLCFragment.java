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

public class OLCFragment extends Fragment {

	private String edu = "";
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.olc_screen, container, false);
		
		final Service service = Service.getInstance(getActivity());
		
		Button btnAdd = (Button)view.findViewById(R.id.olc_btnAdd);
		Button btnDone = (Button)view.findViewById(R.id.olc_btnDone);
		final Spinner olc_spinner = (Spinner)view.findViewById(R.id.olc_edu);
		final EditText etHours = (EditText)view.findViewById(R.id.olc_etHours);
		
		final ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(getActivity(), R.array.Uddannelser, 
				android.R.layout.simple_spinner_item);
		
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		
		olc_spinner.setAdapter(adapter);
		
		olc_spinner.setOnItemSelectedListener(new OnItemSelectedListener() {

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
				fa.setType("OLC");
				fa.setEdu(edu);
				fa.setTime(Integer.parseInt(etHours.getText().toString()));
				
				service.addFormulaActivity(fa);
				
				int duration = Toast.LENGTH_SHORT;
				String text = "Aktivitet tilføjet";
				Toast toast = Toast.makeText(getActivity(), text, duration);
				toast.show();
				
				etHours.setText("");
				olc_spinner.setSelection(0);
				
				Log.d("OLC", "TYPE: " + fa.getType() + " EDU: " + fa.getEdu() + " ENG: " + fa.isEnglish() + " HOURS: " + fa.getTime());
				Log.d("OLC","Activity size: " + service.getFormulaActivities().size());
				
			}
		});
		
		return view;
	}
}
