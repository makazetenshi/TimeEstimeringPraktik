package com.example.timeestimation;

import android.app.FragmentTransaction;
import android.app.ListFragment;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;

public class PeriodListFragment extends ListFragment {
	
	@Override
	public void onViewCreated(View view, Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onViewCreated(view, savedInstanceState);
	}
	
	@Override
	public void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);
		
		FragmentTransaction ft = getFragmentManager().beginTransaction();
		ft.replace(R.id.container, new EstimationFragment());
		ft.addToBackStack(null);
		ft.commit();
		
	}

}
