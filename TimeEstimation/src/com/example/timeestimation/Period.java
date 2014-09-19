package com.example.timeestimation;

import java.util.Date;

public class Period {
	
	private Date startDate, endDate;
	private String loggedIn, initials;
	private long id;
	
	public Period() {
		this.startDate = null;
		this.endDate = null;
		this.loggedIn = null;
		this.id = 0;
		this.initials = null;
	}
	
	
	public String getInitials() {
		return initials;
	}


	public void setInitials(String initials) {
		this.initials = initials;
	}



	public Date getStartDate() {
		return startDate;
	}

	public void setStartDate(Date startDate) {
		this.startDate = startDate;
	}

	public Date getEndDate() {
		return endDate;
	}

	public void setEndDate(Date endDate) {
		this.endDate = endDate;
	}

	public String getLoggedIn() {
		return loggedIn;
	}

	public void setLoggedIn(String loggedIn) {
		this.loggedIn = loggedIn;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}
	
	

}
