package com.example.timeestimation;

import service.Service;
import dao.MySQLiteHelper.PeriodCursor;
import android.app.FragmentTransaction;
import android.app.ListFragment;
import android.app.LoaderManager.LoaderCallbacks;
import android.content.Context;
import android.content.Loader;
import android.database.Cursor;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CursorAdapter;
import android.widget.ListView;
import android.widget.TextView;

public class PeriodListFragment extends ListFragment implements LoaderCallbacks<Cursor> {
	
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

	@Override
	public Loader<Cursor> onCreateLoader(int id, Bundle args) {
		// TODO Auto-generated method stub
		return new PeriodListCursorLoader(getActivity());
	}

	@Override
	public void onLoadFinished(Loader<Cursor> loadxer, Cursor cursor) {
		PeriodCursorAdapter adapter = new PeriodCursorAdapter(getActivity(), (PeriodCursor)cursor);
		setListAdapter(adapter);
	}

	@Override
	public void onLoaderReset(Loader<Cursor> loader) {
		setListAdapter(null);
	}
	
	private static class PeriodListCursorLoader extends SQLiteCursorLoader{

		public PeriodListCursorLoader(Context context) {
			super(context);
		}

		@Override
		protected Cursor loadCursor() {
			return Service.getInstance(getContext()).getPeriods();
		}
		
	}
	
	private static class PeriodCursorAdapter extends CursorAdapter{
		
		private PeriodCursor mPeriodCursor;
		
		public PeriodCursorAdapter(Context context, PeriodCursor c) {
			super(context,c,0);
			mPeriodCursor = c;
		}

		@Override
		public View newView(Context context, Cursor cursor, ViewGroup parent) {
			LayoutInflater inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			return inflater.inflate(android.R.layout.simple_list_item_1, parent,false);
		}

		@Override
		public void bindView(View view, Context context, Cursor cursor) {
			Period period = mPeriodCursor.getPeriod();
			
			TextView text = (TextView) view;
			text.setText("Start: " + period.getStartDate() + " End: " + period.getEndDate() + " Estimate: " + " User: " + 
			Service.getInstance(context).getLoggedInUser().getInitials());
			
		}
	}
}
