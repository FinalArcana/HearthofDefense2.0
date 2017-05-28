using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class OpponentSpawnerController : MonoBehaviour {

	private bool changingWave;
	public EnemyController sample;

	private MultiplayerWaveGenerator waveGenerator;
	private float spawnLast;
	private float waveLast;

//	public SocketIOComponent socket;

	// Use this for initialization
	void Start () {
		waveGenerator = new MultiplayerWaveGenerator (sample);
		waveLast = waveGenerator.waveLength ();
		spawnLast = waveGenerator.getDelayPerSpawn ();
		changingWave = false;
	}
	
	// Update is called once per frame
	void Update () {
		waveLast-=Time.deltaTime;
		spawnLast -= Time.deltaTime;
		if (spawnLast < 0 && waveGenerator.getSpawnCount() > 0) {
			spawnLast = waveGenerator.getDelayPerSpawn ();

			EnemyController enemy = waveGenerator.spawnEnemy ();
			EnemyController newEnemy = Instantiate (enemy, this.gameObject.transform.position, Quaternion.identity);

			newEnemy.GetComponentInParent<Pather> ().target = GameObject.Find("OpponentEND").transform;

		}
		if (waveLast < 0 && !changingWave ) {
			changingWave = true;
		

			waveGenerator.generate ();
			waveLast = waveGenerator.waveLength ();
			changingWave = false;
		}
	}

}
