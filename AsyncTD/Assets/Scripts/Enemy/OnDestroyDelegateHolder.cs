using UnityEngine;
using System.Collections;

public class OnDestroyDelegateHolder : MonoBehaviour
{
	public int WaveNumber;

	public delegate void OnDestroyEnemyDelegate (int waveNumber);

	public OnDestroyEnemyDelegate OnDestroyEnemy = delegate {
		
	};

	public void OnEnemyDestroy ()
	{
		OnDestroyEnemy (WaveNumber);
	}
}
