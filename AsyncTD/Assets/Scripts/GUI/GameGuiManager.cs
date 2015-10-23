using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameGuiManager : MonoBehaviour
{
	public Image playerImage;
	public Image buildTowerPanel;
	public Image upgradeTowerPanel;
	public Button startButton;
	public Text levelDescription;
	public Image enemyImage;
	public Text healthValue;
	public Text moneyValue;
	private int level = 0;

	// Use this for initialization
	void Start ()
	{
		buildTowerPanel.gameObject.SetActive (false);
		upgradeTowerPanel.gameObject.SetActive (false);
	}

	public void updatePlayerImage (Sprite playerSprite)
	{
		playerImage.sprite = playerSprite;
	}

	public void showNextLevelGui (Sprite enemySprite)
	{
		level++;
		levelDescription.text = "Next Level: " + level;
		enemyImage.sprite = enemySprite;
		startButton.gameObject.SetActive (true);
	}

	public void showCurrentLevelGui ()
	{
		levelDescription.text = "Current Level: " + level;
		startButton.gameObject.SetActive (false);
	}

	public void showBuildTowerPanel ()
	{
		buildTowerPanel.gameObject.SetActive (true);
	}

	public void disableBuildTowerPanel ()
	{
		buildTowerPanel.gameObject.SetActive (false);
	}

	public void showUpgradeTowerPanel ()
	{
		upgradeTowerPanel.gameObject.SetActive (true);
	}

	public void disableUpgradeTowerPanel ()
	{
		upgradeTowerPanel.gameObject.SetActive (false);
	}

	public void updateHealthValue (int value)
	{
		healthValue.text = value.ToString ();
	}

	public void updateMoneyValue (int value)
	{
		moneyValue.text = value.ToString ();
	}
}
