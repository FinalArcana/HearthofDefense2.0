using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyController : MonoBehaviour {

	private PlayerEnemy enemy;

	public float speed;
	public float health;
	public int money;
	public int score;
	public bool dead;
	ScoreManager sm;
	MultiplayerManager mm;
	public AudioSource audioSource;
	public GameObject healthBar;
	public GameObject deadEffect;


	// Use this for initialization
	void Start () {
		mm = FindObjectOfType<MultiplayerManager> ();
		sm = FindObjectOfType<ScoreManager> ();
		dead = false;
		enemy = new PlayerEnemy (this.health + mm.extraHealth, this.speed + mm.extraSpeed, this.money + mm.extraMoney,0, this.score + mm.extraScore);
		audioSource = FindObjectOfType<AudioSource> ();
		float percent = enemy.getHealth()/this.health;
		float realValue = percent * 1.761128f;

		healthBar.transform.localScale = new Vector3 (healthBar.transform.localScale.x, healthBar.transform.localScale.y, realValue);



	}

	bool IsDead() {
		return enemy.getHealth () <= 0;
	}

	void increaseMoneyAndScore() {
		sm.money += enemy.getMoney ();
		sm.score += enemy.getScore ();
	}

	void destroy() {
		if (audioSource == null) {
			GameObject dead = Instantiate (deadEffect, new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), transform.rotation);
			transform.position = Vector3.one * -9999999f;
			Destroy (this.gameObject);
			Destroy (dead,2f);
		}
		else {
			GameObject dead = Instantiate (deadEffect, new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), transform.rotation);
			audioSource.Play ();
			transform.position = Vector3.one * -9999999f;
			Destroy (this.gameObject,audioSource.clip.length);
			Destroy (dead,2f);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(float damage){
		enemy.decreaseHealth(damage);

		if (IsDead () && !dead) {
			dead = true;
			increaseMoneyAndScore ();
			destroy();

		}
		float percent = enemy.getHealth()/(this.health + (sm.level*2f*(sm.stage / 5 + 1)) );

		float realValue = percent * 1.761128f;

		healthBar.transform.localScale = new Vector3 (healthBar.transform.localScale.x, healthBar.transform.localScale.y, realValue);



	}
}
