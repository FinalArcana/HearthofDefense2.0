using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SocketIO;
public class ScoreManager : MonoBehaviour {

	public int lives = 5;
	public int money = 100;
	public int usedMoney = 0;
	public int score = 0;
	public int level = 1;
	public int stage = 1;

	public Text moneyText;
	public Text livesText;
	public Text timeText;
	public Text scoreText;
	public Text stageText;
	public Text levelText;
	public Text moneyUsedText;
	float moneyUsedTextCooldown = 1f;


	public GameObject winScreen;
	public GameObject loseScreen;
	GameManager gm;
	MultiplayerManager mm;

	public SocketIOComponent socket;
	void Start() {
		
		gm = FindObjectOfType<GameManager> ();
		mm = FindObjectOfType<MultiplayerManager> ();
		SetText();
	}
	//Suggest (Trong) : Enemy may have attack point and lose life depend on attack point instead
	public void LoseLife() {
		lives -= 1;
		if (gm.isMulti) {
			socket.Emit("LOSE_LIFE");

		}
		if(lives <= 0) {
			if (gm.isMulti) {
				socket.Emit("LOSE_GAME");

			}
			GameOver();
		}
	}
	public void GameOver() {
		Debug.Log("Game Over! You lost!");
		loseScreen.SetActive(true);
		Time.timeScale = 0;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void loadMainMenu() {
		if(gm.isMulti) socket.Emit("disconnect");
		SceneManager.LoadScene("MainMenu");
	}

	public void LevelUp() {
		level++;
	}

	public void ChangeStage() {
		level = 1;
		stage++;
	}

	void Update() {
		// FIXME: This doesn't actually need to update the text every frame.
		timeText.text = "Next wave : " + SpawnerController.TimeLeft().ToString();
		if (!moneyUsedText.text.Equals ("")) {
			moneyUsedTextCooldown -= Time.deltaTime;
		}
		if (moneyUsedTextCooldown <= 0f) {
			moneyUsedText.text = "";
			moneyUsedTextCooldown = 1f;
		}
	
	}

	public void SetText() {
		moneyText.text = money.ToString ();
		livesText.text = lives.ToString ();
		scoreText.text = score.ToString ();
		stageText.text = "Stage: " + stage.ToString ();

		if (gm.isMulti) {
			levelText.text = "Level: " + mm.level.ToString();
		} else {
			levelText.text = "Level: " + level.ToString ();
		}

	
	}

	public void SetUsedMoneyText() {
		if (usedMoney >= 0) {
			
			moneyUsedText.text = "+" + usedMoney.ToString ();
		} else {
			moneyUsedText.text = usedMoney.ToString ();
		}
	}


}
