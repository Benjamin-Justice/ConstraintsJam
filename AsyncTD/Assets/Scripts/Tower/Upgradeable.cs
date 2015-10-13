using UnityEngine;

public class Upgradeable : MonoBehaviour
{
	public GameObject UpgradeTarget;
	public int cost;

	public GameObject Upgrade ()
	{
		if (Gold.useGold (cost)) {
			UpgradeTarget.SetActive (true);
			gameObject.SetActive (false);
			return UpgradeTarget;
		} else {
			Debug.Log ("More Gold is required");
			return this.gameObject;
		}
	}
}
