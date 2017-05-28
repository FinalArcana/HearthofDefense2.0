using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tower : MonoBehaviour {

	Transform turretTransform;

	public float range = 10f;
	public GameObject bulletPrefab;

	public int cost = 5;
	public int sellCost;

	public float fireCooldown = 0.5f;
	float fireCooldownLeft = 0;

	public float damage = 5f;
	public float radius = 0;

	public int level = 1;
	public string name;

	//public float slow = 0f;
	public bool isSlow;

	public string element = "None";

	// public canvas uiUpgrade;
	public GameObject tempUI;

	GameManager gm;
	public float shootRate;
	private float lastShot;
	private bool allowFire;
	public int towerTag;

	public int towerRef;
	// Use this for initialization
	void Start () {
		turretTransform = transform.Find("Turret");
		sellCost = (int)System.Math.Round(0.8*cost);
		gm = FindObjectOfType<GameManager> ();

		// sellCost = 
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Optimize this!

			EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();

			EnemyController nearestEnemy = null;
			float dist = Mathf.Infinity;

			foreach(EnemyController e in enemies) {
				float d = Vector3.Distance(this.transform.position, e.transform.position);
				if(nearestEnemy == null || d < dist) {
					nearestEnemy = e;
					dist = d;
				}
			}
			if(nearestEnemy == null) {
				//			Debug.Log("No enemies?");
				return;
			}

			Vector3 dir = nearestEnemy.transform.position - this.transform.position;

			Quaternion lookRot = Quaternion.LookRotation( dir );

			//Debug.Log(lookRot.eulerAngles.y);
			turretTransform.rotation = Quaternion.Euler( -90, lookRot.eulerAngles.y, 0 );

			fireCooldownLeft -= Time.deltaTime;
			if(fireCooldownLeft <= 0 && dir.magnitude <= range) {
				fireCooldownLeft = fireCooldown;
					ShootAt(nearestEnemy);
					lastShot = Time.time;
			}
	}

	public override bool Equals(object obj)
	{
		if (obj == null) return false;
		Tower objAsEC = obj as Tower;
		if (objAsEC == null)
			return false;
		else if (this.towerTag != objAsEC.towerTag)
			return false;
		else
			return true;
	}

	void OnMouseEnter() {
		// Debug.Log("Hover tower");
		// GameObject germSpawned = Instantiate(uiUpgrade) as GameObject;
 		// germSpawned.transform.SetParent(canvas.transform);
 		// germSpawned.transform.localPosition = spawnPosition;
 		// germSpawned.transform.localRotation = spawnRotation;
		// Renderer blockRender = blockModel.GetComponent<Renderer>();
		// blockRender.material.color = Color.blue;

	}
	/// <summary>
	/// Called when the mouse is not any longer over the GUIElement or Collider.
	/// </summary>
	void OnMouseExit() {
		// Renderer blockRender = blockModel.GetComponent<Renderer>();
		// blockRender.material.color = originalColor;
		Destroy(tempUI);
	}

	void ShootAt(EnemyController e) {
		// TODO: Fire out the tip!
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = e.transform;
		b.damage = damage;
		b.radius = radius;
		b.isSlow = isSlow;
		b.element = element;
	}

	public void SetElement(string element) {
		this.element = element;
	}

	void ShootAt(PlayerEnemyController e) {
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = e.transform;
		b.damage = damage;
		b.radius = radius;
	}

	public void SellTower(){
		gameObject.SetActive (false);
		Destroy(this.gameObject);
		AstarPath.active.Scan ();
		// GameObject.FindObjectOfType<GroundTileControl>();
	}

	public void UpgradeTower(){
		if(level <= 10){
			level++;
			cost = cost*level;	
			damage = damage*level;
			sellCost = (int)System.Math.Round(0.8*cost);
		}
	}
}
