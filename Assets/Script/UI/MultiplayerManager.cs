using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System.Text.RegularExpressions;

public class MultiplayerManager : MonoBehaviour {

	public int level;
	public SocketIOComponent socket;
	public float extraHealth;
	public float extraSpeed;
	public int extraMoney;
	public int extraScore;
	public string opponentElement;
	public string playerElement;
	GameManager gm;

	public Text fireCostText;
	public Text waterCostText;
	public Text earthCostText;
	int elementCost = 10;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ();
		socket.On("UPGRADE_WAVE", LevelUp);
		socket.On ("ELEMENT_CHANGE", ElementChange);
		socket.On("DEAD_CREEP", DeadCreep);
		socket.On ("HERO_NEVER_DIE", ReceivedDeadEnemy);
		level = 1;
		extraHealth = 0;
		extraSpeed = 0;
		extraMoney = 0;
		opponentElement = "None";
		playerElement = "None";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LevelUp(SocketIOEvent evt) {
		level++;
		extraHealth += 2f;
		extraMoney += 2;
		extraSpeed += 0.2f;
		extraScore += 10;
	}


	void DeadCreep(SocketIOEvent evt) {
		gm.Untrack(int.Parse(evt.data.GetField("tag").ToString()));
	}
	void SetElementCostText(){
		elementCost *= 2;
		Debug.Log("Current element cost "+elementCost);
		fireCostText.text = "$ "+ elementCost;
		waterCostText.text = "$ "+ elementCost;
		earthCostText.text = "$ "+ elementCost;
	}
	void ElementChange(SocketIOEvent evt) {
		string newElement = evt.data.GetField ("element").ToString ();
		if (newElement.Equals ("\"Fire\"")) {
			Debug.Log ("Im on fire");
			opponentElement = "Fire";
		} else if (newElement.Equals ("\"Ice\"")) {
			Debug.Log ("Im on ice");

			opponentElement = "Ice";
		} else if (newElement.Equals ("\"Earth\"")) {
			Debug.Log ("Im on earth");

			opponentElement = "Earth";
		}
		Debug.Log ("Element Change to" + opponentElement);

	}

	public void setLevel(int level) {
		this.level = level;
	}

	public void SetElement(string element) {
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		if(sm.money < elementCost) {
			Debug.Log("Not enough money!");
			return;
		}

		sm.money -= elementCost;
		sm.usedMoney = -elementCost;
		sm.SetUsedMoneyText ();
		sm.SetText ();
		SetElementCostText();

		this.playerElement = element;
		Dictionary<string, string> data = new Dictionary<string, string>();


		data ["element"] = element;

		socket.Emit("ELEMENT_CHANGING", new JSONObject(data));
	}

	public void DeadEnemy(int tag) {
		Dictionary<string, string> data = new Dictionary<string, string>();
		data ["tag"] = tag.ToString ();
		socket.Emit ("DIE_DIE_DIE", new JSONObject (data));
	}

	public void SellTower(int tag) {
		Dictionary<string, string> data = new Dictionary<string, string> ();
		data ["tag"] = tag.ToString ();
		socket.Emit ("SELL_TOWER", new JSONObject (data));
		
	}

	void ReceivedDeadEnemy(SocketIOEvent evt) {
//		string enemyTag = JsonToString("\"",evt.data.GetField ("tag").ToString ());
		int enemyTag = int.Parse(evt.data.GetField("tag").ToString().Trim('"'));
		gm.Untrack (enemyTag);
	}

	string  JsonToString( string target, string s){

		string[] newString = Regex.Split(target,s);

		return newString[1];

	}
	public void upgradeTower(int tag, string element) {
		Dictionary<string, string> data = new Dictionary<string, string> ();
		data ["tag"] = tag.ToString ();
		data ["element"] = element;
		socket.Emit ("UPGRADE_TOWER_ELEMENT", new JSONObject (data));
		Debug.Log("Notified server of tower " + tag.ToString() + " element change to " + element);
	}
}
