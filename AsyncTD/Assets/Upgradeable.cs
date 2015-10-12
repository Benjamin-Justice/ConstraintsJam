using UnityEngine;

public class Upgradeable : MonoBehaviour
{
	public GameObject UpgradeTarget;

	void Start ()
	{

	}

	public GameObject Upgrade ()
	{
		UpgradeTarget.SetActive (true);
		gameObject.SetActive (false);
		return UpgradeTarget;
	}
}
