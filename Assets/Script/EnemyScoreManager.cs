using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SocketIO;

public class EnemyScoreManager : MonoBehaviour {

	public int lives = 5;
	public int money = 100;
	public int level = 1;

	public Text livesText;
	public Text levelText;
	float moneyUsedTextCooldown = 1f;

	GameManager gm;
	MultiplayerManager mm;

	public SocketIOComponent socket;
	void Start() {
		// gm = FindObjectOfType<GameManager> ();
		// mm = FindObjectOfType<MultiplayerManager> ();
		socket.On ("UPGRADE_LIFES", SetLifeText);
		SetText();
	}
	//Suggest (Trong) : Enemy may have attack point and lose life depend on attack point instead

	void Update() {
		// FIXME: This doesn't actually need to update the text every frame.
	}
	public void SetLifeText(SocketIOEvent evt){
		lives--;
		SetText();
	}

	public void SetWaveText(){
		level++;
		SetText();
	}

	public void SetText() {
		livesText.text = lives.ToString ();
		levelText.text = level.ToString ();
	}
}
