using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator {

	private int level;
	private EnemyController enemy;
	private Wave wave;
	private float delay;


	public WaveGenerator(EnemyController enemy) {
		this.level = 1;
		this.enemy = enemy;
		this.delay = 1.5f;
		generate ();

	}

	public void setLevel(int level) {
		this.level = level;
	}

	public void generate() {
		wave = new Wave (level * 5, enemy,delay*level*5+10,delay);
	}
	public int getSpawnCount() {
		return wave.getSpawnCount ();
	}

	public float getDelayPerSpawn() {
		return wave.getDelayPerSpawn ();
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

	public void levelUp(EnemyController enemy) {
		this.level += 1;
		if(this.delay >= 0.5f)
			this.delay -= 0.2f;
		this.enemy = enemy;
	}


}
