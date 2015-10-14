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

	static float distanceToCorner (EnemyHealth arg)
	{
		return Vector3.Distance (arg.gameObject.GetComponent<NavMeshAgent> ().path.corners [0], arg.gameObject.GetComponent<NavMeshAgent> ().path.corners [1]);
	}

	static int pathLength (EnemyHealth arg)
	{
		return arg.gameObject.GetComponent<NavMeshAgent> ().path.corners.Length;
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
			int minLength = enemiesInRange.Min (arg => pathLength (arg));
			List<EnemyHealth> nearest = enemiesInRange.FindAll ((EnemyHealth arg) => pathLength (arg) == minLength);				
			target = nearest.Aggregate ((EnemyHealth oldItem, EnemyHealth newItem) => distanceToCorner (oldItem) < distanceToCorner (newItem) ? oldItem : newItem);
			break;
		case TargetSelectionMode.LAST:
			int maxLength = enemiesInRange.Max (arg => pathLength (arg));
			List<EnemyHealth> furthest = enemiesInRange.FindAll ((EnemyHealth arg) => pathLength (arg) == maxLength);				
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
