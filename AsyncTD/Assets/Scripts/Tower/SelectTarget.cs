using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

[RequireComponent (typeof(AttackTarget))]
public class SelectTarget : MonoBehaviour
{
	public Collider attackRange;
	public LayerMask mask;
	public TargetSelectionMode mode;
	List<EnemyHealth> enemiesInRange;
	AttackTarget weapon;

	void Start ()
	{
		enemiesInRange = new List<EnemyHealth> ();
		weapon = GetComponent<AttackTarget> ();
	}

	static float distanceToCorner (EnemyHealth enemyHealth)
	{
		return Vector3.Distance (enemyHealth.gameObject.GetComponent<NavMeshAgent> ().path.corners [0], enemyHealth.gameObject.GetComponent<NavMeshAgent> ().path.corners [1]);
	}

	static int pathLength (EnemyHealth enemyHealth)
	{
		return enemyHealth.gameObject.GetComponent<NavMeshAgent> ().path.corners.Length;
	}

	void FixedUpdate ()
	{   
		enemiesInRange.RemoveAll ((EnemyHealth obj) => obj == null || obj.Health == 0);
		if (enemiesInRange.Count == 0) {
			weapon.Target = null;
			return;
		}
		EnemyHealth target = null;
		switch (mode) {
		case TargetSelectionMode.FIRST:
			int lowestCornerCount = enemiesInRange.Min (enemyHealth => pathLength (enemyHealth));
			List<EnemyHealth> nearest = enemiesInRange.FindAll ((EnemyHealth enemyHealth) => pathLength (enemyHealth) == lowestCornerCount);				
			target = nearest.Aggregate ((EnemyHealth oldItem, EnemyHealth newItem) => distanceToCorner (oldItem) < distanceToCorner (newItem) ? oldItem : newItem);
			break;
		case TargetSelectionMode.LAST:
			int highestCornerCount = enemiesInRange.Max (enemyHealth => pathLength (enemyHealth));
			List<EnemyHealth> furthest = enemiesInRange.FindAll ((EnemyHealth arg) => pathLength (arg) == highestCornerCount);				
			target = furthest.Aggregate ((EnemyHealth oldItem, EnemyHealth newItem) => distanceToCorner (oldItem) > distanceToCorner (newItem) ? oldItem : newItem);
			break;
		case TargetSelectionMode.HIGH_HP:
			target = enemiesInRange.Aggregate ((EnemyHealth oldItem, EnemyHealth newItem) => oldItem.Health > newItem.Health ? oldItem : newItem);
			break;
		case TargetSelectionMode.LOW_HP:
			target = enemiesInRange.Aggregate ((EnemyHealth oldItem, EnemyHealth newItem) => oldItem.Health < newItem.Health ? oldItem : newItem);
			break;
		}
		weapon.Target = target;

	}



	void OnTriggerEnter (Collider other)
	{
		EnemyHealth otherEnemy = other.GetComponent<EnemyHealth> ();
		if (otherEnemy != null) {
			enemiesInRange.Add (otherEnemy);
			//Debug.Log ("Added " + otherEnemy.gameObject.name);
		}
	}

	void OnTriggerExit (Collider other)
	{
		EnemyHealth otherEnemy = other.GetComponent<EnemyHealth> ();
		if (otherEnemy != null) {
			enemiesInRange.Remove (otherEnemy);
			//Debug.Log ("Removed " + otherEnemy.gameObject.name);
		}
	}
}
