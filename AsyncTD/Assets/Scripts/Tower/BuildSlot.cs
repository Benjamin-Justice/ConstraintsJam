using UnityEngine;
using System;

public class BuildSlot : MonoBehaviour
{
	public BuildableTower[] BuildableTowers;

	public GameObject Build (TowerType requestedType)
	{
		BuildableTower requestedTowerBlueprint = 
			Array.Find (BuildableTowers, (tower) => tower.towerType == requestedType);
		if (Gold.useGold (requestedTowerBlueprint.cost)) {
			requestedTowerBlueprint.towerObject.SetActive (true);
			gameObject.SetActive (false);
			return requestedTowerBlueprint.towerObject;
		} else {
			Debug.Log ("Moar gold is required");
			return this.gameObject;
		}
	}
}
