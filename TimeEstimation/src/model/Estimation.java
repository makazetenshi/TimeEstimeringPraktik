package model;

public class Estimation {
	
	private String type, education;
	private double time;
	
	public Estimation(String type, String education, double time) {
		this.type = type;
		this.education = education;
		this.time = time;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	public String getEducation() {
		return education;
	}

	public void setEducation(String education) {
		this.education = education;
	}

	public double getTime() {
		return time;
	}

	public void setTime(double time) {
		this.time = time;
	}

}
