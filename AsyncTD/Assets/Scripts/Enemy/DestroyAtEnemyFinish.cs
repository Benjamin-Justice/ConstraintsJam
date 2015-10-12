using UnityEngine;
using System.Collections;

public class DestroyAtEnemyFinish : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		NavMeshAgent navAgent = this.GetComponent<NavMeshAgent> ();
		navAgent.updateRotation = true;
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<EnemyFinish> () != null) {
			Object.Destroy (this.gameObject);
		}
	}
}
