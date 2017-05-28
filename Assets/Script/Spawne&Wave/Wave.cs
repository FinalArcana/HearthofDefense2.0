using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {
	
	private int spawnCount; 
	private float delayPerSpawn;
	private float length;
	private EnemyController[] enemies;



	public Wave(int spawnCount, EnemyController enemy, float length,float delayPerSpawn) {
		this.spawnCount = spawnCount;
		enemies = new EnemyController[spawnCount];
		this.length = length;
		this.delayPerSpawn = delayPerSpawn;
		setEnemies (enemy);
	}

	public void setSpawnCount(int spawnCount) {
		this.spawnCount = spawnCount;
	}

	public void setEnemies(EnemyController enemy) {
		for (int i = 0; i < spawnCount; i++) {
			enemies [i] = enemy;
		}
	}
	public EnemyController[] getEnemies() {
		return enemies;
	}

	public int getSpawnCount() {
		return this.spawnCount;
	}
		
	public EnemyController spawnEnemy() {
		return this.enemies [--spawnCount];
	}

	public float getDelayPerSpawn() {
		return this.delayPerSpawn;
	}

	public float waveLength() {
		return this.length;
	}
}
