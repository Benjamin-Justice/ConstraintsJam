using UnityEngine;
using System.Collections;

[RequireComponent (typeof(OnDestroyDelegateHolder))]
public class DestroyAtEnemyFinish : MonoBehaviour
{
	void Start ()
	{
		NavMeshAgent navAgent = this.GetComponent<NavMeshAgent> ();
		navAgent.updateRotation = true;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<EnemyFinish> () != null) {
			this.GetComponent<OnDestroyDelegateHolder> ().OnEnemyDestroy ();
			Object.Destroy (this.gameObject);
		}
	}
}
