using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_CameraController : MonoBehaviour {

	private Vector3 player2View = new Vector3(2.7f, 27.73f, -0.02f);
	private Vector3 player1View = new Vector3(2.7f, 27.73f, 58.5f);

	private string position;

	Camera mainCam;

	// Use this for initialization
	void Start () {
		position = "player";
		mainCam = Camera.main;
		updateCameraToView ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateCameraToView () {
	
		switch (position) {
		case "player":
			mainCam.transform.position = player2View;
			position = "opponent";
			break;
		case "opponent":
			mainCam.transform.position = player1View;
			position = "player";
			break;
		
		}
	}
}
