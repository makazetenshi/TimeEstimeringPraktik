package model;

public class DayActivity {
	
	private double days;
	private String type;
	
	public DayActivity(String type, double days) {
		this.type = type;
		this.days = days;
	}

	public double getDays() {
		return days;
	}

	public void setDays(double days) {
		this.days = days;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}
	
}
