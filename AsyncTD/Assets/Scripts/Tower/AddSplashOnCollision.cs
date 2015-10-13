using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]
public class AddSplashOnCollision : MonoBehaviour
{
	public ParticleSystem system;
	public ParticleCollisionEvent[] collisionEvents;
	public ParticleSystem splashParticleSytem;
	public AttackTarget weapon;
	// Use this for initialization
	void Awake ()
	{
		system = GetComponent<ParticleSystem> ();
		collisionEvents = new ParticleCollisionEvent[2];
		splashParticleSytem.startSpeed *= weapon.Splash;
	}

	void OnParticleCollision (GameObject other)
	{
		int safeLength = system.GetSafeCollisionEventSize ();
		if (collisionEvents.Length < safeLength)
			collisionEvents = new ParticleCollisionEvent[safeLength];

		int numCollisionEvents = system.GetCollisionEvents (other, collisionEvents);
		int i = 0;
		while (i < numCollisionEvents) {
			Vector3 pos = collisionEvents [i].intersection;
			pos.y = 0.1f;
			splashParticleSytem.transform.position = pos;
			splashParticleSytem.Play ();
			i++;
		}
	}
}
