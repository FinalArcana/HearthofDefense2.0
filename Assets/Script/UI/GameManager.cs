using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public bool isMulti;
	public TextMesh moneyText;
	public int player;
	float cooldown = 0f;
	List<EnemyController> trackedEnemy = new List<EnemyController>();
	int tagID = 0;
	int towerTag = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		if (!moneyText.text.Equals ("")) {
//			cooldown += Time.deltaTime;
//		}
//		if (cooldown >= 2f) {
//			moneyText.text = "";
//			cooldown = 0f;
//		}
	}

	public void GainMoney(Vector3 position,int money) {
		
		TextMesh tempText = Instantiate(moneyText,position,Quaternion.Euler(new Vector3(90,270,0)));
		tempText.text = "+ " + money.ToString ();
		Destroy (tempText.gameObject, 1f);
	}

	public void Track(EnemyController enemy) {
		trackedEnemy.Add (enemy);
	}

	public void Untrack(int tag) {

		foreach (EnemyController e in FindObjectsOfType<EnemyController>()) {
			Debug.Log ("Enemy tag " + e.tagID);
			Debug.Log ("Tag sent " + tag);
			if (e.tagID == tag-1) {
				e.destroy ();
			}
		}
		//find a way to bring an enemy
//		trackedEnemy.Remove(trackedEnemy.Find(
	}

	public void isSameTower(int tag) {
		
	}

	public int GetTag() {
		return tagID++;
	}
	public int GetTowerTag() {
		return towerTag++;
	}
}
