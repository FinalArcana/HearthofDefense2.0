using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemy : Enemy {

	private float health;
	private float speed;
	private int money;
	private int score;
	// Use this for initialization
	public PlayerEnemy (float health,float speed,int money,int attack,int score ): base("Player",speed,health,money,attack,score) {

	}

	public override void specialAbility() {
	
	}

//	public float getHealth() {
//	
//		return health;
//	}
//
//	public float getSpeed() {
//
//		return speed;
//	}
//
//	public int getMoney() {
//	
//		return money;
//	}
//
//	public int getScore() {
//		return score;
//	}
//
//	public void decreaseHealth(float damage) {
//		this.health -= damage;
//	}
	

}
