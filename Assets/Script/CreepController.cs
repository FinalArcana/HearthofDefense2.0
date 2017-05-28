using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class CreepController : MonoBehaviour {
	public SocketIOComponent socket;
	public float extraSpeed = 0f;
	public int waveCost = 20;
	public Text costui;
	// Use this for initialization
	void Start () {
		socket.On("UPGRADE_WAVE", helloT);
	}
	
	// Update is called once per frame
	public void levelUpWave(){
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		if(sm.money < waveCost) {
			Debug.Log("Not enough money!");
			return;
		}
		
		socket.Emit("CREEP_LEVEL", new JSONObject("true"));
		extraSpeed += 0.2f;
		EnemyScoreManager em = GameObject.FindObjectOfType<EnemyScoreManager>();
		em.SetWaveText();

		
		sm.money -= waveCost;
		sm.usedMoney = -waveCost;
		sm.SetUsedMoneyText ();
		sm.SetText ();
		waveCost *= 2;
		costui.text = "$ " + waveCost;
	}
	void helloT(SocketIOEvent evt){
		Debug.Log("AAAA");
	}
}
