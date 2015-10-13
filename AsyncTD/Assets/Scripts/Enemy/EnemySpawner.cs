using System;
using UnityEngine;
using System.Collections;

// TODO implement pause between waves, have them started by the player
public class EnemySpawner : MonoBehaviour
{
	public GameManager gameManager;
	public Transform EnemyFinishLocation;
	public EnemyWave[] EnemyWaves;
	private int currentWaveNumber = 0;
	private float timeSinceSpawn = 0f;
	private int enemiesSpawnedInCurrentWave = 0;
	private bool roundFullySpawned;
	private bool waveFullySpawned;

	private EnemyWave CurrentWave {
		get {
			return EnemyWaves [currentWaveNumber];
		}
	}

	void Update ()
	{
		if (roundFullySpawned) {
			if (this.transform.childCount == 0) {
				gameManager.Win ();
			}
		} else if (waveFullySpawned) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				waveFullySpawned = false;
			}
		} else {
			if (timeSinceSpawn > CurrentWave.Interval) {
				SpawnEnemy ();
			}
			timeSinceSpawn += Time.deltaTime;
		}
	}

	private void SpawnEnemy ()
	{
		enemiesSpawnedInCurrentWave++;
		GameObject enemy = (GameObject)UnityEngine.Object.Instantiate (CurrentWave.EnemyPrefab, this.transform.position, this.transform.rotation);
		enemy.transform.parent = this.transform;
		OnDestroyDelegateHolder onDestroyDelegateHolder = enemy.GetComponent<OnDestroyDelegateHolder> ();
		onDestroyDelegateHolder.WaveNumber = currentWaveNumber;
		onDestroyDelegateHolder.OnDestroyEnemy += onDestroyEnemy;
		NavMeshAgent navMeshAgent = enemy.GetComponent<NavMeshAgent> ();
		navMeshAgent.updateRotation = true;
		navMeshAgent.SetDestination (EnemyFinishLocation.position);
		if (enemiesSpawnedInCurrentWave >= CurrentWave.EnemyCount) {
			WaveSpawnComplete ();
		}
		timeSinceSpawn -= CurrentWave.Interval;
	}

	private void WaveSpawnComplete ()
	{
		timeSinceSpawn = 0f;
		enemiesSpawnedInCurrentWave = 0;
		if (currentWaveNumber + 1 < EnemyWaves.Length) {
			waveFullySpawned = true;
			currentWaveNumber++;
		} else {
			roundFullySpawned = true;
		}
	}

	public void Reset ()
	{
		currentWaveNumber = 0;
		timeSinceSpawn = 0f;
		enemiesSpawnedInCurrentWave = 0;
		waveFullySpawned = false;
		roundFullySpawned = false;
		foreach (EnemyWave wave in EnemyWaves) {
			wave.DeathCount = 0;
		}
	}

	private  void onDestroyEnemy (int waveNumber)
	{
		EnemyWave enemyWave = EnemyWaves [waveNumber];
		enemyWave.DeathCount++;
		Debug.Assert (enemyWave.DeathCount <= enemyWave.EnemyCount, "number " + waveNumber + " death" + enemyWave.DeathCount + " count" + enemyWave.EnemyCount);
		if (enemyWave.DeathCount == enemyWave.EnemyCount) {
			Gold.addGold (enemyWave.GoldReward);
		}
	}
}
