using UnityEngine;
using System.Collections;

public class DestroyAtEnemyFinish : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
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
