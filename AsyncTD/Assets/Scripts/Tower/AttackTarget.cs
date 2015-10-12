using UnityEngine;
using System.Collections;
[RequireComponent (typeof(SelectTarget))]
public class AttackTarget : MonoBehaviour {
	public int Damage=1;
	public float Splash=1;
	public float AttackRate=1;
	public EnemyHealth Target;
	private float delta;
	private LayerMask mask;
	public GameObject Muzzle;
	public ParticleSystem bulletParticleSytem;
	void Start () {
		delta = AttackRate;
		mask = GetComponent<SelectTarget> ().mask;
	}
	
	void Update () {
		delta -= Time.deltaTime;
		if (delta > 0) {
			return;
		}
		delta += AttackRate;
		if (Target ==null) {
			return;
		}
		Muzzle.transform.LookAt (Target.gameObject.transform.position);
		bulletParticleSytem.Play ();
		if (Splash >0) {
			Collider[] hitColliders =Physics.OverlapSphere (Target.gameObject.transform.position, Splash, mask);
			Debug.DrawLine (Target.gameObject.transform.position, Target.gameObject.transform.position + Vector3.left,Color.green,1f);
			foreach (Collider hitCollider in hitColliders) {
				EnemyHealth subtarget = hitCollider.gameObject.GetComponent<EnemyHealth> ();
				attackTarget (subtarget);
			}

		} else {
			attackTarget (Target);
		}
	}
	private void attackTarget (EnemyHealth target)
	{
		if (Damage !=0) {
			target.Health -= Damage;
		}
	}
}
