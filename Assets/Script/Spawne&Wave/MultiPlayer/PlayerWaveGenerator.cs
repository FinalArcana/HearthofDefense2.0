using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaveGenerator {

	private Wave wave;
	private float delay;	
	private EnemyController enemy;

	public PlayerWaveGenerator(EnemyController enemy) {
		this.enemy = enemy;
		delay = 1.0f;
		generate ();
	}

	public void generate() {
		wave = new Wave (5, enemy, delay * 5 + 5, delay);
	}
		
	public float getDelayPerSpawn() {
		return wave.getDelayPerSpawn ();
	}
	public int getSpawnCount() {
		return wave.getSpawnCount ();
	}
	public EnemyController spawnEnemy() {
		return wave.spawnEnemy ();
	}
	public float waveLength() {
		return wave.waveLength ();
	}
	public EnemyController[] getEnemies() {
		return wave.getEnemies ();
	}

	
}
