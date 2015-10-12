using UnityEngine;
using System.Collections;

public class EnemyFinish : MonoBehaviour {
	public Lives lives;
	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<EnemyHealth> () != null) {
			lives.Remaining--;
		}
	}

}
