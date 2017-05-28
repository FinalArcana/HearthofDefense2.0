using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerEnemy : Enemy {

	private float health;
	private float speed;
	private int money;
	private int score;

	public MultiplayerEnemy (float health,float speed,int money,int attack,int score ): base("Player",speed,health,money,attack,score) {

	}

	public override void specialAbility() {

	}



}
