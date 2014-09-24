package model;

public class EstimatedActivity {
	
	private double time;
	private String type;
	
	public EstimatedActivity() {
		this.type = null;
		this.time = 0;
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
