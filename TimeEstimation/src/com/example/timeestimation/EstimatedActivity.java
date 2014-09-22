package com.example.timeestimation;

public class EstimatedActivity {
	
	private double time;
	private String type;
	
	public EstimatedActivity(String type, double time) {
		this.type = type;
		this.time = time;
	}

	public double getTime() {
		return time;
	}

	public void setTime(double time) {
		this.time = time;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

}
