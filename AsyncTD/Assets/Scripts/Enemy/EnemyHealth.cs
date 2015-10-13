using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent (typeof(OnDestroyDelegateHolder))]
public class EnemyHealth : MonoBehaviour
{
	[SerializeField]
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

	public void onDeathAnimationComplete ()
	{
		this.GetComponent<OnDestroyDelegateHolder> ().OnEnemyDestroy ();
		Object.Destroy (this.gameObject);
	}
}
