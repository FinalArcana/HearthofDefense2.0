using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class PlayerSpawnerController : MonoBehaviour {

	private PlayerWaveGenerator waveGenerator;
	private bool changingWave;
	public EnemyController sample;


	private float spawnLast;
	private float waveLast;
//	public SocketIOComponent socket;

	// Use this for initialization
	void Start () {
//		socket.On ("CREEP_LEVEL", levelUp);
		waveGenerator = new PlayerWaveGenerator (sample);
	}


	
	// Update is called once per frame
	void Update () {
		waveLast-=Time.deltaTime;
		spawnLast -= Time.deltaTime;
		if (spawnLast < 0 && waveGenerator.getSpawnCount() > 0) {
			spawnLast = waveGenerator.getDelayPerSpawn ();

			EnemyController enemy = waveGenerator.spawnEnemy ();
			Instantiate (enemy, this.gameObject.transform.position, Quaternion.identity);

		}
		if (waveLast < 0 && !changingWave ) {
			changingWave = true;

			waveGenerator.generate ();
			waveLast = waveGenerator.waveLength ();
			changingWave = false;
		}
	}
}
