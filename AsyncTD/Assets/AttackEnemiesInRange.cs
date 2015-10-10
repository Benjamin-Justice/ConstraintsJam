using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackEnemiesInRange : MonoBehaviour
{
	public Collider attackRange;
	private List<EnemyHealth> enemiesInRange;

	// Use this for initialization
	void Start ()
	{
		enemiesInRange = new List<EnemyHealth> ();
	}

	void FixedUpdate ()
	{
		if (enemiesInRange.Count > 0) {
			enemiesInRange [0].Health -= 1;
			if (enemiesInRange [0].Health <= 0) {
				enemiesInRange.Remove (enemiesInRange [0]);
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		EnemyHealth otherEnemy = other.GetComponent<EnemyHealth> ();
		if (otherEnemy != null) {
			enemiesInRange.Add (otherEnemy);
			Debug.Log ("Added " + otherEnemy.gameObject.name);
		}
	}

	void OnTriggerExit (Collider other)
	{
		EnemyHealth otherEnemy = other.GetComponent<EnemyHealth> ();
		if (otherEnemy != null) {
			enemiesInRange.Remove (otherEnemy);
			Debug.Log ("Removed " + otherEnemy.gameObject.name);
		}
	}
}
