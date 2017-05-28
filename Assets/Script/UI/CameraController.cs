using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	private Vector3 player1View = new Vector3(2.7f, 27.73f, -0.02f);
	private Vector3 player2View = new Vector3(2.7f, 27.73f, 58.5f);

	public GameObject uibottom;
	public GameObject uitop;
	public GameObject uiBottomEnemy;
	public GameObject uiTopEnemy;

	Camera mainCam;
	private bool changeView = false;


	public int num_player;
	public Button playerButton;
	public Button opponentButton;
	GameManager gm;

	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ();
		mainCam = Camera.main;
		gm.player = num_player;
		if (num_player == 2) {
			playerButton.GetComponentInChildren<Text> ().text = 1 + "\'s Field";
			opponentButton.GetComponentInChildren<Text> ().text = 2 + "\'s Field";

		} else {
			playerButton.GetComponentInChildren<Text> ().text = 2 + "\'s Field";
			opponentButton.GetComponentInChildren<Text> ().text = 1 + "\'s Field";
		}

//		updateCameraToView ();

	}
	
	// Update is called once per frame
	void Update () {
		if (changeView) {
			switch (num_player) {
			case 1:
//				mainCam.transform.position = player2View;
				mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, player2View, 60*Time.deltaTime);
//				num_player = 2;
				break;
			case 2:
//				mainCam.transform.position = player1View;
				mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, player1View, 60*Time.deltaTime);
//				num_player = 1;
				break;

			}
			if (num_player == 1 && Vector3.Distance (mainCam.transform.position, player2View) <= 1) {
				num_player = 2;
				changeView = false;
			} else if (num_player == 2 && Vector3.Distance (mainCam.transform.position, player1View) <= 1) {
				num_player = 1;
				changeView = false;
			}
//			changeView = false;
		}
		
	}

	public void updateCameraToView () {
//		button.GetComponentInChildren<Text> ().text = num_player + "\'s Field";
		changeView = true;
	
		if(num_player == gm.player){
			uibottom.SetActive(false);
			uitop.SetActive(false);
			uiTopEnemy.SetActive(true);
			uiBottomEnemy.SetActive(true);
		} else {
			uibottom.SetActive(true);
			uitop.SetActive(true);
			uiTopEnemy.SetActive(false);
			uiBottomEnemy.SetActive(false);
		}
	}
}
