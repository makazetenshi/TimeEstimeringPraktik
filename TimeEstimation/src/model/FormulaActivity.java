package model;

public class FormulaActivity {

	private double constant;
	private String edu, type;
	private int time;
	private boolean english = false;
	
	
	public FormulaActivity() {
		this.constant = 0;
		this.edu = null;
		this.time = 0;
		this.type = null;
	}


	public double getConstant() {
		return constant;
	}


	public void setConstant(double constant) {
		this.constant = constant;
	}


	public String getEdu() {
		return edu;
	}


	public void setEdu(String edu) {
		this.edu = edu;
	}


	public int getTime() {
		return time;
	}


	public void setTime(int time) {
		this.time = time;
	}


	public boolean isEnglish() {
		return english;
	}


	public void setEnglish(boolean english) {
		this.english = english;
	}
	
	public String getType(){
		return type;
	}
	
	public void setType(String type){
		this.type = type;
	}
	
	
}
