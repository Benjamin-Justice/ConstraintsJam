using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGuiManager : MonoBehaviour
{
	public Image[] player = new Image[6];

	// Use this for initialization
	void Start ()
	{
		foreach (Image image in player) {
			image.gameObject.SetActive (false);
		}
	}

	public void showPlayerImage (int playerArrayPosition, Sprite playerSprite)
	{
		player [playerArrayPosition].gameObject.SetActive (true);
		player [playerArrayPosition].sprite = playerSprite;

	}
}
