using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {


	private bool changingWave;

	private WaveGenerator waveGenerator;


	private static float waveLast;
	private float spawnLast;
	private const int SIZE = 5;
	public EnemyController[] enemies = new EnemyController[SIZE];
	private ScoreManager sm;
	GameManager gm;




	// Use this for initialization
	void Start () {
		sm = FindObjectOfType<ScoreManager> ();
		gm = FindObjectOfType<GameManager> ();
		waveGenerator = new WaveGenerator (enemies[0]);
		waveLast = waveGenerator.waveLength ();
		spawnLast = waveGenerator.getDelayPerSpawn ();
		changingWave = false;

	}

	public static int TimeLeft() {
		return Mathf.CeilToInt (waveLast);
	}

	public void levelUpWave() {
		sm.level++;

		if (sm.level % 5 == 0) {
			sm.stage++;
			sm.level = 1;
			if (sm.stage >= SIZE) {

				waveGenerator.levelUp (enemies[SIZE-1]);
			} else {

				waveGenerator.levelUp (enemies[sm.stage-1]);
			}

		}
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
			if (!gm.isMulti) {
				levelUpWave ();
			}

			waveGenerator.generate ();
			waveLast = waveGenerator.waveLength ();
			changingWave = false;
			sm.SetText ();
		}
	}
		
}
