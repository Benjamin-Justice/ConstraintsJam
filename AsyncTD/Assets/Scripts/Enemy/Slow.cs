using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NavMeshAgent))]
public class Slow : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;
	public float slow;
	private float debuffTime;

	public float DebuffTime {
		get {
			return debuffTime;
		}
		set {
			if (value > debuffTime) {
				if (debuffTime <= 0) {
					this.enabled = true;
				}
				debuffTime = value;
			}
		}
	}

	private float originalSpeed;
	private float slowedSpeed;

	void Awake ()
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
		originalSpeed = navMeshAgent.speed;
		slowedSpeed = originalSpeed * slow;
	}

	void OnEnable ()
	{
		navMeshAgent.speed = slowedSpeed;
	}

	void OnDisable ()
	{
		navMeshAgent.speed = originalSpeed;
	}

	void Update ()
	{
		debuffTime -= Time.deltaTime;
		if (debuffTime < 0) {
			this.enabled = false;
		}
	}
}
