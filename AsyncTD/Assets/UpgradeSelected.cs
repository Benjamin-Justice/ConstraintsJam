using UnityEngine;
using System.Collections;

public class UpgradeSelected : MonoBehaviour {
	public CurrentSelection selected;
	// Use this for initialization
	void Start () {
		
	}

	void Upgrade(int upgrade)
	{
		if (upgrade < 0) {
			return;
		}
		if (upgrade > -1) {
			Debug.Log (upgrade);
		}
		if (selected.currentSelectedObject == null) {
			Debug.Log ("no selection");
			return;
		}
		Upgrades upgrades = selected.currentSelectedObject.GetComponent<Upgrades> ();
		if (upgrades == null) {
			Debug.Log ("no upgrades");
			return;
		}
		if (upgrade >= upgrades.UpgradeTargets.Length) {
			Debug.Log ("upgrade to high " + upgrades.UpgradeTargets.Length);
			return;
		}
		selected.currentSelectedObject.SetActive (false);
		upgrades.UpgradeTargets [upgrade].SetActive (true);
		selected.Select (upgrades.UpgradeTargets [upgrade]);
	}
	
	// Update is called once per frame
	void Update () {
		int upgrade = -1;
		//TODO better input
		if(Input.GetKeyDown(KeyCode.F1)){
			upgrade = 0;
		}
		if(Input.GetKeyDown(KeyCode.F2)){
			upgrade = 1;
		}
		if(Input.GetKeyDown(KeyCode.F3)){
			upgrade = 2;
		}
		Upgrade(upgrade);
	}
}
