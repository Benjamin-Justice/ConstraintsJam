using UnityEngine;
using System;

[Serializable]
public class EnemyWave
{
	public int EnemyCount;
	public GameObject EnemyPrefab;
	public float Interval;
	public int GoldReward;
	[HideInInspector]
	public int DeathCount;
}
