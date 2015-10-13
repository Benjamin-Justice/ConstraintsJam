using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public EnemySpawner spawner;
	public GameObject TowerPositions;
	public int StartLivesAmount;
	public int StartGoldAmount;
	GameState gameState;

	void Awake ()
	{
		gameState = GameState.PRESETUP;
		Lives.OnZeroLives += Lose;
		ResetGame ();
	}

	void ResetGame ()
	{
		Lives.Remaining = StartLivesAmount;
		Gold.StartAmount = StartGoldAmount;
		resetTowerPositions ();
		spawner.Reset ();
		gameState = GameState.SETUP;
	}

	void Update ()
	{
		if (gameState.Equals (GameState.SETUP) && Input.GetKeyDown (KeyCode.Space)) {
			StartGame ();
		}
		if (gameState.Equals (GameState.FINISHED) && Input.GetKeyDown (KeyCode.Space)) {
			ResetGame ();
		}
	}

	void StartGame ()
	{
		gameState = GameState.RUNNING;
		spawner.gameObject.SetActive (true);

	}

	void resetTowerPositions ()
	{
		foreach (Transform tower in TowerPositions.transform) {
			foreach (Transform part in tower.transform) {
				if (part.GetComponent<BuildSlot> () != null) {
					part.gameObject.SetActive (true);
				} else {
					part.gameObject.SetActive (false);
				}
			}
		}
	}

	public void Lose ()
	{
		Debug.Log ("You lose");
		cleanUp ();
	}

	public void Win ()
	{
		Debug.Log ("You win");
		cleanUp ();
	}

	void cleanUp ()
	{
		gameState = GameState.FINISHED;
		spawner.gameObject.SetActive (false);
		foreach (Transform child in spawner.gameObject.transform) {
			Object.Destroy (child.gameObject);
		}
	}
}
