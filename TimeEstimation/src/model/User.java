package model;

public class User {

	private String initials, password, name;
	
	public User(String initials, String password) {
		this.initials = initials;
		this.password = password;
		this.name = "";
	}

	public String getInitials() {
		return initials;
	}

	public void setInitials(String initials) {
		this.initials = initials;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
	
	
}
