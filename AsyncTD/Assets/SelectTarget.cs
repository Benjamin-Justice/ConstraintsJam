using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(AttackTarget))]
public class SelectTarget : MonoBehaviour
{
    public Collider attackRange;
    private List<EnemyHealth> enemiesInRange;
    public LayerMask mask;
    float delta;
    private AttackTarget weapon;
    // Use this for initialization
    void Start ()
    {
        enemiesInRange = new List<EnemyHealth> ();
        weapon = GetComponent<AttackTarget> ();
    }

    void FixedUpdate ()
    {   
        while (enemiesInRange.Count > 0 && (enemiesInRange[0] == null || enemiesInRange[0].Health ==0) ) {
            enemiesInRange.RemoveAt (0);
        }
        if (enemiesInRange.Count == 0) {
            weapon.Target = null;
        } else {
            weapon.Target = enemiesInRange [0];
        }

    }



    void OnTriggerEnter (Collider other)
    {
        EnemyHealth otherEnemy = other.GetComponent<EnemyHealth> ();
        if (otherEnemy != null) {
            enemiesInRange.Add (otherEnemy);
            //Debug.Log ("Added " + otherEnemy.gameObject.name);
        }
    }

    void OnTriggerExit (Collider other)
    {
        EnemyHealth otherEnemy = other.GetComponent<EnemyHealth> ();
        if (otherEnemy != null) {
            enemiesInRange.Remove (otherEnemy);
            //Debug.Log ("Removed " + otherEnemy.gameObject.name);
        }
    }
}
