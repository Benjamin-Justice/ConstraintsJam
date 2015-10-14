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

	void UpdateA ()
	{
		NavMeshAgent navAgent = this.GetComponent<NavMeshAgent> ();
		string a = "";
		foreach (Vector3 corner in navAgent.path.corners) {
			a += corner + " ";
			Debug.DrawRay (corner, Vector3.up);
		}
		Debug.Log (a);
	}
}
