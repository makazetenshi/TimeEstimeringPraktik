package com.example.timeestimation;

import service.Service;
import android.app.DialogFragment;
import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

public class MenuFragment extends Fragment {

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.menu_screen, container, false);
		
		Service service = Service.getInstance(getActivity());
		
		Button btnShow = (Button)view.findViewById(R.id.btnShow);
		Button btnNew = (Button)view.findViewById(R.id.btnNew);
		
		btnShow.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				FragmentTransaction ft = getFragmentManager().beginTransaction();
				ft.replace(R.id.container, new PeriodListFragment());
				ft.addToBackStack(null);
				ft.commit();
			}
		});
		
		btnNew.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				DialogFragment newFragment = new PeriodFragment();
				newFragment.show(getFragmentManager(), "datePicker");
			}
		});
		
		return view;
	}

}
