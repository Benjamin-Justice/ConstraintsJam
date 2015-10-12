using UnityEngine;
using System.Collections;

// TODO implement pause between waves, have them started by the player
public class EnemySpawner : MonoBehaviour
{
	public GameManager gameManager;
	public Transform EnemyFinishLocation;
	public EnemyWave[] EnemyWaves;

	private int waveNumber = 0;
	private float timeSinceSpawn = 0f;
	private int enemiesSpawnedInCurrentWave = 0;
	private bool fullySpawned;
	private EnemyWave CurrentWave {
		get {
			return EnemyWaves [waveNumber];
		}
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (fullySpawned) {
			if (this.transform.childCount == 0) {
				gameManager.Win ();
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
		GameObject enemy = (GameObject)Object.Instantiate (CurrentWave.EnemyPrefab, this.transform.position, this.transform.rotation);
		enemy.transform.parent = this.transform;
		var navMeshAgent = enemy.GetComponent<NavMeshAgent> ();
		navMeshAgent.updateRotation = true;
		navMeshAgent.SetDestination (EnemyFinishLocation.position);
		if (enemiesSpawnedInCurrentWave >= CurrentWave.EnemyCount) {
			WaveComplete ();
		}
		timeSinceSpawn -= CurrentWave.Interval;
	}

	private void WaveComplete ()
	{
		timeSinceSpawn = 0f;
		enemiesSpawnedInCurrentWave = 0;
		if (waveNumber + 1 < EnemyWaves.Length) {
			waveNumber++;
		} else {
			fullySpawned = true;
		}
	}

	public void Reset ()
	{
		waveNumber = 0;
		timeSinceSpawn = 0f;
		enemiesSpawnedInCurrentWave = 0;
		fullySpawned = false;
	}
}
