using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using SocketIO;

public class EnemyTowerSpawn : MonoBehaviour {

	public SocketIOComponent socket;
	public Tower sniperTower;
	public Tower explodyTower;
	public Tower slowTower;
	public Tower rapidTower;
	public Tower heavyTower;

	// Use this for initialization
	void Start () {
		socket.On("CREATE_TOWERS", OnCreateTower);
		socket.On ("SOLD_TOWER", SoldTower);
		socket.On("UPGRADED_TOWER_ELEMENT", changeTowerColor);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void changeTowerColor(SocketIOEvent evt) {
		int towerTag = int.Parse(evt.data.GetField ("tag").ToString ().Trim ('"'));
		string elementToChange = evt.data.GetField("element").ToString();
		Debug.Log("Opponent changed tower " + towerTag.ToString() + " to element " + elementToChange);
		foreach(Tower t in FindObjectsOfType<Tower>()) {
			if(t.towerTag == towerTag) {
				if(elementToChange == "\"Water\"") 
					t.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
				else if(elementToChange == "\"Fire\"") 
					t.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
				else if(elementToChange == "\"Earth\"") 
					t.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
			}
		}
	}
	void SoldTower(SocketIOEvent evt) {
		int towerTag = int.Parse(evt.data.GetField ("tag").ToString ().Trim ('"'));
		foreach (Tower t in FindObjectsOfType<Tower>()) {
			if (t.towerTag == towerTag) {
				t.gameObject.SetActive (false);
				Destroy (t.gameObject);
			}
		}
	
	
	}

	void OnCreateTower(SocketIOEvent evt){
		
		string type = evt.data.GetField("type").ToString();
		Debug.Log("Enemy Have Created Tower"+ type+" "+evt.data.GetField("position").ToString());
		if (type.Equals ("\"Sniper\"")) {
			Debug.Log ("Created Snipper Tower " + JsonToString (evt.data.GetField ("position").ToString (), "\""));
			Tower placedTower = Instantiate (sniperTower, JsonToVecter3 (JsonToString (evt.data.GetField ("position").ToString (), "\"")), Quaternion.identity);
			placedTower.towerTag = int.Parse (evt.data.GetField ("tag").ToString ().Trim ('"'));
			this.recalculatePath ();

		} else if (type.Equals ("\"Explore\"")) {
			Tower placedTower = Instantiate (explodyTower, JsonToVecter3 (JsonToString (evt.data.GetField ("position").ToString (), "\"")), Quaternion.identity);
			placedTower.towerTag = int.Parse (evt.data.GetField ("tag").ToString ().Trim ('"'));

			this.recalculatePath ();
		} else if (type.Equals ("\"Slow\"")) {
			Tower placedTower = Instantiate (slowTower, JsonToVecter3 (JsonToString (evt.data.GetField ("position").ToString (), "\"")), Quaternion.identity);
			placedTower.towerTag = int.Parse (evt.data.GetField ("tag").ToString ().Trim ('"'));

			this.recalculatePath ();
		} else if (type.Equals ("\"Rapid\"")) {
			Tower placedTower = Instantiate (rapidTower, JsonToVecter3 (JsonToString (evt.data.GetField ("position").ToString (), "\"")), Quaternion.identity);
			placedTower.towerTag = int.Parse (evt.data.GetField ("tag").ToString ().Trim ('"'));

			this.recalculatePath ();
		} else if (type.Equals ("\"Heavy\"")) {
			Tower placedTower = Instantiate (heavyTower, JsonToVecter3 (JsonToString (evt.data.GetField ("position").ToString (), "\"")), Quaternion.identity);

			placedTower.towerTag = int.Parse (evt.data.GetField ("tag").ToString ().Trim ('"'));
			this.recalculatePath ();
		}
		// GameObject placedTower = Instantiate(bm.selectedTower,  new Vector3(transform.parent.position.x - blockSizeX/2, transform.parent.position.y + blockSizeY, transform.parent.position.z - blockSizeZ/2), transform.parent.rotation);
	}
	public void recalculatePath() {
		Debug.Log("Recalculate Path");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject enemy in enemies) {
			enemy.GetComponent<Pather>().calculatePath();
		}
	}

	string  JsonToString( string target, string s){

		string[] newString = Regex.Split(target,s);

		return newString[1];

	}

	Vector3 JsonToVecter3(string target ){

		Vector3 newVector;
		string[] newString = Regex.Split(target,",");
		newVector = new Vector3( float.Parse(newString[0]), float.Parse(newString[1]), float.Parse(newString[2]));

		return newVector;

	}
}
