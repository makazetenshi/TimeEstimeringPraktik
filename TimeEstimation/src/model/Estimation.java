package model;

public class Estimation {
	
	private String type, education;
	private double time;
	private long id;
	
	public Estimation(String type, String education, double time) {
		this.type = type;
		this.education = education;
		this.time = time;
		this.id = 0;
	}
	
	

	public long getId() {
		return id;
	}



	public void setId(long id) {
		this.id = id;
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
