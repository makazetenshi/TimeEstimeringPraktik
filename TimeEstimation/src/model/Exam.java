package model;

public class Exam {
	
	private String edu, exam;
	private int students, projects, days;
	private long id;
	private double estimation;

	public Exam() {
		this.edu = null;
		this.exam = null;
		this.students = 0;
		this.projects = 0;
		this.days = 0;
		this.id = 0;
		this.estimation = 0;
	}
		
	public double getEstimation() {
		return estimation;
	}

	public void setEstimation(double estimation) {
		this.estimation = estimation;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public String getEdu() {
		return edu;
	}

	public void setEdu(String edu) {
		this.edu = edu;
	}

	public String getExam() {
		return exam;
	}

	public void setExam(String exam) {
		this.exam = exam;
	}

	public int getStudents() {
		return students;
	}

	public void setStudents(int students) {
		this.students = students;
	}

	public int getProjects() {
		return projects;
	}

	public void setProjects(int projects) {
		this.projects = projects;
	}

	public int getDays() {
		return days;
	}

	public void setDays(int days) {
		this.days = days;
	}	
}
