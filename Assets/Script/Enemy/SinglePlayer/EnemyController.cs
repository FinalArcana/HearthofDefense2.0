using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	private Enemy enemy;

	float defaultSpeed;
	public float speed;

	float currentSpeed;
	public float health;
	public int money;
	public int attack;
	public int score;
	public bool dead;
	public string element;
	public AudioSource audioSource;
	public GameObject healthBar;
	public GameObject BlueAura;
	public GameObject RedAura;
	public GameObject GreenAura;
	public GameObject deadEffect;
	ScoreManager sm;
	MultiplayerManager mm;
	GameManager gm;
	CreepController cc;
	public int tagID;

	bool isSlowed;

	float slowDuration;
	

	void Start () {
		slowDuration = 0;
		defaultSpeed = speed;
		sm = FindObjectOfType<ScoreManager> ();
		mm = FindObjectOfType<MultiplayerManager> ();
		gm = FindObjectOfType<GameManager> ();
		cc = FindObjectOfType<CreepController> ();
		int round = sm.stage / 5 + 1;
		tagID = gm.GetTag ();

		dead = false;
		if (gm.isMulti) {
			
			if (this.name.Equals ("playerenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
				this.health += mm.extraHealth;
				this.speed += mm.extraSpeed;
				this.money += mm.extraMoney;
				this.score += mm.extraScore;
				enemy = new PlayerEnemy (this.health, this.speed, this.money, this.attack, this.score);
				this.element = mm.opponentElement;
				SetAura ();
			} else if(this.name.Equals("opponentenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
				this.speed += cc.extraSpeed;
				enemy = new MultiplayerEnemy (this.health, this.speed, this.money, this.attack, this.score);
				this.element = mm.playerElement;
				SetAura ();

			}


		} else {
			this.health += (sm.level * 2f * round);
			this.money += sm.level * sm.stage;
			if (this.name.Equals ("simpleenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
				enemy = new SimpleEnemy ("Simple",this.speed, this.health, this.money, this.attack, this.score);
			} else if (this.name.Equals ("advanceenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
				enemy = new AdvanceEnemy ("Advance",this.speed, this.health, this.money, this.attack, this.score);
			} 
			else if (this.name.Equals ("specialenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
				enemy = new SpecialEnemy ("Special",this.speed, this.health , this.money, this.attack, this.score);
			} 
			else if (this.name.Equals ("epicenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
				enemy = new EpicEnemy ("Epic",this.speed, this.health, this.money, this.attack, this.score);
			}
			else if (this.name.Equals ("legendaryenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
				enemy = new LegendaryEnemy ("Legendary",this.speed, this.health, this.money, this.attack, this.score);
			}
			else {
				enemy = new SimpleEnemy ("Simple",this.speed, this.health + (sm.level*2f * round), this.money+sm.level*sm.stage , this.attack, this.score);
			}
			RandomElement ();
		}

		audioSource = FindObjectOfType<AudioSource> ();
		if (!this.name.Equals ("opponentenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
			float percent;
			if (gm.isMulti) {
				percent = enemy.getHealth () / (this.health + mm.extraHealth);
			} else {
				percent = enemy.getHealth()/(this.health + (sm.level*2f*(sm.stage / 5 + 1)));
			}
			float realValue = percent * 1.761128f;

			healthBar.transform.localScale = new Vector3 (healthBar.transform.localScale.x, healthBar.transform.localScale.y, realValue);
		}


		this.gameObject.GetComponent<Pather> ().updateSpeed ();
		//Let's illustrate inheritance with the 
		//default constructors.

	}

	bool IsDead() {
		return enemy.getHealth() <= 0;
	}

	void RandomElement() {
		
		int random = (int) Mathf.Floor (Random.Range (0, 4)) + 1;
		if (random == 1) {
			SetElement ("Ice");

		} else if (random == 2) {
			SetElement ("Fire");

		} else if (random == 3) {
			SetElement ("Earth");
			
		} else {
			SetElement ("None");
		}
		SetAura ();
	}

	void SetAura() {
		BlueAura.SetActive (false);
		RedAura.SetActive (false);
		GreenAura.SetActive (false);
		if (this.element.Equals ("Ice")) {
			BlueAura.SetActive (true);
		} else if (this.element.Equals ("Fire")) {
			RedAura.SetActive (true);
		} else if (this.element.Equals ("Earth")) {
			GreenAura.SetActive (true);
		}
	
	}

	void increaseMoneyAndScore() {
		sm.money += enemy.getMoney ();
		sm.score += enemy.getScore ();
		sm.SetText ();
		if (gm.isMulti) {
			mm.DeadEnemy (this.tagID);

		}
		gm.GainMoney (this.gameObject.transform.position,this.money);

	}

	public void destroy() {
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
		
	void Update() {
		if(isSlowed) {
			gameObject.GetComponent<Pather>().updateSpeed();
			slowDuration -= Time.deltaTime;
			if(slowDuration <= 0) {
				speed = defaultSpeed;
				isSlowed = false;
				gameObject.GetComponent<Pather>().updateSpeed();
			}
		}
	}
		

	public void TakeDamage(float damage){
		if(this.name.Equals("opponentenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
			return;
		}

		enemy.decreaseHealth(damage);


		if (IsDead () && !dead) {
			dead = true;
			increaseMoneyAndScore ();
			destroy();
	
		}
		float percent;
		if (gm.isMulti) {
			percent = enemy.getHealth () / (this.health + mm.extraHealth);
		} else {
			percent = enemy.getHealth()/(this.health + (sm.level*2f*(sm.stage / 5 + 1)) );

		}

		float realValue = percent * 1.761128f;

		healthBar.transform.localScale = new Vector3 (healthBar.transform.localScale.x, healthBar.transform.localScale.y, realValue);



	}
	public void Slow(float percent,float duration) {
		if(!isSlowed) {
			isSlowed = true;
			speed *= (100f-percent) / 100;
			slowDuration = duration;
			Debug.Log("SLOWED!");
		}
	}

	public void SetElement(string element) {
		this.element = element;
	
	}

	void FixedUpdate() {
		
	}
	public override bool Equals(object obj)
	{
		if (obj == null) return false;
		EnemyController objAsEC = obj as EnemyController;
		if (objAsEC == null)
			return false;
		else if (this.tag != objAsEC.tag)
			return false;
		else
			return true;
	}


		
}