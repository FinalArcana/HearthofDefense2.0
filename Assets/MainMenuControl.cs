using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void loadSinglePlayer() {
		SceneManager.LoadScene("Final");
		Time.timeScale = 1;
	}
	public void loadMultiplayer() {
		SceneManager.LoadScene("MultiplayerTest");
	}
	public void loadHighScore() {
		SceneManager.LoadScene("ScoreScene");
	}
	public void loadQuit() {
		Application.Quit();
	}
}
