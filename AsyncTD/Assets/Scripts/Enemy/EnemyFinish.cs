using UnityEngine;
using System.Collections;

public class EnemyFinish : MonoBehaviour
{

	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<EnemyHealth> () != null) {
			Lives.Remaining--;
		}
	}

}
