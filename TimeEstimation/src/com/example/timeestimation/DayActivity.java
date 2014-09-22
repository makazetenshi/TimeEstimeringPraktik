package com.example.timeestimation;

public class DayActivity {
	
	private int days;
	private String type;
	
	public DayActivity(String type, int days) {
		this.type = type;
		this.days = days;
	}

	public int getDays() {
		return days;
	}

	public void setDays(int days) {
		this.days = days;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}
	
}
