using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NavMeshAgent))]
public class EnemyHealth : MonoBehaviour
{
	private int health = 100;
	public Animator animator;

	public int Health {
		get {
			return health;
		}

		set {
			this.health = value;
			if (health <= 0) {
				if (animator != null) {
					animator.SetBool ("dead", true);
					this.GetComponent<NavMeshAgent> ().enabled = false;
				} else {
					this.onDeathAnimationComplete ();
				}
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void onDeathAnimationComplete ()
	{
		Object.Destroy (this.gameObject);
	}
}
