using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class OpponentScoreManager : MonoBehaviour {
	public int lives;
	public GameObject winScreen;
	public SocketIOComponent socket;

	GameManager gm;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager>();
		socket.On("LOSE_LIVES", decreaseEnemyLife);
		socket.On("WIN_GAME", win);
	}
	void win(SocketIOEvent evt) {
		Dictionary<string, string> data = new Dictionary<string, string>();
		data["playerno"] = gm.player.ToString();
		socket.Emit("STORE_SCORE", new JSONObject(data));
		winScreen.SetActive(true);
		Time.timeScale = 0;
	}
	void decreaseEnemyLife(SocketIOEvent evt) {
		lives--;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
