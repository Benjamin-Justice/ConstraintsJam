using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public EnemySpawner spawner;
	public Lives lives;
	bool running = false;

	void Start ()
	{
	
	}

	void Update ()
	{
		if (!running && Input.GetKeyDown (KeyCode.Space)) {
			StartGame ();
		}
	}

	void StartGame ()
	{
		running = true;
		spawner.Reset ();
		lives.Reset ();
		spawner.gameObject.SetActive (true);
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
		running = false;
		spawner.gameObject.SetActive (false);
		foreach (Transform child in spawner.gameObject.transform) {
			Object.Destroy (child.gameObject);
		}
		//TODO reset tower attack things
	}
}
