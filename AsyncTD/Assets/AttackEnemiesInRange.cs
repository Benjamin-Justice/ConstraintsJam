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
	
	// Update is called once per frame
	void Update ()
	{
		enemiesInRange.ForEach ((enemy) => enemy.Health -= 1);
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
