using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour
{

	public StartGuiManager startGuiManager;
	public GameGuiManager gameGuiManager;

	void Start ()
	{
		EnableStartGui ();
		DisableGameGui ();
	}

	public void EnableStartGui ()
	{
		startGuiManager.gameObject.SetActive (true);
	}

	public void DisableStartGui ()
	{
		startGuiManager.gameObject.SetActive (false);
	}

	public void EnableGameGui ()
	{
		gameGuiManager.gameObject.SetActive (true);
	}

	public void DisableGameGui ()
	{
		gameGuiManager.gameObject.SetActive (false);
	}
	
}
