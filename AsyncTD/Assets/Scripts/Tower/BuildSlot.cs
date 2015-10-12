using UnityEngine;
using System;

public class BuildSlot : MonoBehaviour
{
	public BuildableTower[] BuildableTowers;

	void Start ()
	{

	}

	public GameObject Build (TowerType requestedType)
	{
		GameObject requestedTowerObject = 
			Array.Find (BuildableTowers, (tower) => tower.towerType == requestedType).towerObject;
		requestedTowerObject.SetActive (true);
		gameObject.SetActive (false);
		return requestedTowerObject;
	}
}
