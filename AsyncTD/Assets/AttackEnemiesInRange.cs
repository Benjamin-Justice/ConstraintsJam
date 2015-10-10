using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackEnemiesInRange : MonoBehaviour
{
	public Collider attackRange;
	private List<EnemyHealth> enemiesInRange;
	private int mask;
	Splash splash;
	Damage damage;
	AttackRate attackRate;
	float delta;
	// Use this for initialization
	void Start ()
	{
		enemiesInRange = new List<EnemyHealth> ();
		splash=GetComponent<Splash> ();
		damage=GetComponent<Damage> ();
		attackRate = GetComponent<AttackRate> ();
		delta = attackRate.attackRate;
		mask = 1 << LayerMask.NameToLayer ("Enemy");
	}

	void FixedUpdate ()
	{	
		delta -= Time.deltaTime;
		if (delta > 0) {
			return;
		}
		delta += attackRate.attackRate;
		if (enemiesInRange.Count == 0) {
			return;
		}
		while (enemiesInRange.Count > 0 && enemiesInRange[0] == null) {
			enemiesInRange.RemoveAt (0);
		}
		if (enemiesInRange.Count == 0) {
			return;
		}
		EnemyHealth target = enemiesInRange[0];
		if (splash != null) {
			Collider[] hitColliders =Physics.OverlapSphere (target.gameObject.transform.position, splash.radius, mask);
			foreach (Collider hitCollider in hitColliders) {
				EnemyHealth subtarget = hitCollider.gameObject.GetComponent<EnemyHealth> ();
				attackTarget (subtarget);
			}

		} else {
			attackTarget (target);
		}
	}

	private void attackTarget (EnemyHealth target)
	{
		if (damage != null) {
			target.Health -= damage.damage;
			if (target.Health <= 0) {
				enemiesInRange.Remove (target);
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
