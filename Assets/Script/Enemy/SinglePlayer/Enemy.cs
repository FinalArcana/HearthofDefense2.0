using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy {

	private string name;
	private float speed;
	private float health;
	private int money;
	private int attack;
	private int score;

	public Enemy() {
		speed = 1f;

	}
	public Enemy(string name,float speed, float health, int money, int attack, int score) {
		this.name = name;
		this.speed = speed;	
		this.health = health;
		this.money = money;
		this.attack = attack;
		this.score = score;

		
	}

	public float getSpeed() {
		return this.speed;
	}

	public void setSpeed(float speed) {
		this.speed = speed;
	}

	public float getHealth() {
		return this.health;
	}
	public void decreaseHealth(float damage) {
		this.health -= damage;
	}
	public int getMoney() {
		return this.money;
	}

	public int getAttack() {
		return this.attack;
	}
	public string getName() {
		return this.name;
	}

	public int getScore() {
		return this.score;
	}

	abstract public void specialAbility();

//	public void specialAbility() {
//		Debug.Log ("Do Nothing");
//	}
}
