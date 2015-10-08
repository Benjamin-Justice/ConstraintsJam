using UnityEngine;
using System.Collections;

// TODO implement pause between waves, have them started by the player
public class EnemySpawner : MonoBehaviour
{
	public Transform EnemyFinishLocation;
	public EnemyWave[] EnemyWaves;

	private int waveNumber = 0;
	private float timeSinceSpawn = 0f;
	private int enemiesSpawnedInCurrentWave = 0;

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
		if (timeSinceSpawn > CurrentWave.Interval) {
			SpawnEnemy ();
		}
		timeSinceSpawn += Time.deltaTime;
	}

	private void SpawnEnemy ()
	{
		enemiesSpawnedInCurrentWave++;
		GameObject enemy = (GameObject)Object.Instantiate (CurrentWave.EnemyPrefab, this.transform.position, Quaternion.identity);
		enemy.GetComponent <NavMeshAgent> ().SetDestination (EnemyFinishLocation.position);
		if (enemiesSpawnedInCurrentWave >= CurrentWave.EnemyCount) {
			WaveComplete ();
		}
		timeSinceSpawn -= CurrentWave.Interval;
	}

	private void WaveComplete ()
	{
		timeSinceSpawn = 0f;
		enemiesSpawnedInCurrentWave = 0;
		if (waveNumber + 1 < EnemyWaves.Length)
			waveNumber++;
	}
}
