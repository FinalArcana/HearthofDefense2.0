using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingManager : MonoBehaviour {
	public GameObject selectedTower;
	public GameObject selectedDummyTower;
	public GameObject pathChecker;
	private GameObject tempDummyTower;
	GameManager gm;

	// Use this for initialization
	void Start () {
		tempDummyTower = Instantiate(selectedDummyTower, new Vector3(0,0,0), Quaternion.identity);
		gm = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SelectTowerType(GameObject prefab) {
		selectedTower = prefab;
		
	}
	public void SelectDummyTowerType(GameObject dummyTower){
		selectedDummyTower = dummyTower;
		// if(tempDummyTower = null){
		// 	tempDummyTower = Instantiate(selectedDummyTower, new Vector3(0,0,0), Quaternion.identity);
		// } else {
			Destroy(tempDummyTower);
			tempDummyTower = Instantiate(selectedDummyTower, new Vector3(0,0,0), Quaternion.identity);
		// }
	}
	public void rescanPath() {
		AstarPath.active.Scan();
	}
	public void recalculatePath() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject enemy in enemies) {
			enemy.GetComponent<Pather>().calculatePath();
		}
	}
	public void SellBuilding(Tower tower) {
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		MultiplayerManager mm = GameObject.FindObjectOfType<MultiplayerManager> ();
		GameManager gm = GameObject.FindObjectOfType<GameManager>();
		sm.money += tower.sellCost;
		sm.usedMoney = tower.sellCost;
		sm.SetUsedMoneyText ();
		sm.SetText ();
		if(gm.isMulti) {
			Debug.Log("Towertag tell me plz" + tower.towerTag);
			mm.SellTower (tower.towerRef);
		}
		tower.SellTower();

	}
	public void UpgradeBuilding(Tower tower){
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		if(sm.money < tower.cost) {
			Debug.Log("Not enough money!");
			return;
		}
		sm.money -= tower.cost;
		sm.usedMoney = -tower.cost;
		sm.SetUsedMoneyText ();
		sm.SetText ();
		tower.UpgradeTower();
	}
	public void UpgradeBuildingFireElement(Tower tower){
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		if(sm.money < tower.cost) {
			Debug.Log("Not enough money!");
			return;
		}
		tower.SetElement("Fire");
		tower.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
		sm.money -= tower.cost;
		sm.usedMoney = -tower.cost;
		sm.SetUsedMoneyText ();
		sm.SetText ();
		GameManager gm = GameObject.FindObjectOfType<GameManager>();
		if(gm.isMulti) {
			MultiplayerManager mm = GameObject.FindObjectOfType<MultiplayerManager> ();
			mm.upgradeTower(tower.towerRef, "Fire");
		}
	}
	public void UpgradeBuildingWaterElement(Tower tower){
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		if(sm.money < tower.cost) {
			Debug.Log("Not enough money!");
			return;
		}
		tower.SetElement("Ice");
		tower.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
		sm.money -= tower.cost;
		sm.usedMoney = -tower.cost;
		sm.SetUsedMoneyText ();
		sm.SetText ();
		GameManager gm = GameObject.FindObjectOfType<GameManager>();
		if(gm.isMulti) {
			MultiplayerManager mm = GameObject.FindObjectOfType<MultiplayerManager> ();
			mm.upgradeTower(tower.towerRef, "Water");
		}
	}
	public void UpgradeBuildingEarthElement(Tower tower){
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		if(sm.money < tower.cost) {
			Debug.Log("Not enough money!");
			return;
		}
		tower.SetElement("Earth");
		tower.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
		sm.money -= tower.cost;
		sm.usedMoney = -tower.cost;
		sm.SetUsedMoneyText ();
		sm.SetText ();
		GameManager gm = GameObject.FindObjectOfType<GameManager>();
		if(gm.isMulti) {
			MultiplayerManager mm = GameObject.FindObjectOfType<MultiplayerManager> ();
			mm.upgradeTower(tower.towerRef, "Earth");
		}
	}
	public void SetTowerInformation(GameObject tower){
		TowerInfoCanvas canvas = tower.GetComponent<TowerInfoCanvas>();
		canvas.FadeText();
	}
	public GameObject getDummyTower(){
		return this.tempDummyTower;
	}
}
