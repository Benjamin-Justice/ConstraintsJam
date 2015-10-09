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
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<EnemyFinish> () != null) {
			Object.Destroy (this.gameObject);
		}
	}
}
