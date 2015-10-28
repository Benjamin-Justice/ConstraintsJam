using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;
using BrutalHack.AsyncTD;
using System;

public class GameManager : MonoBehaviour
{
	public EnemySpawner spawner;
	public GameObject TowerPositions;
	public int StartLivesAmount;
	public int StartGoldAmount;
	GameState gameState;
	InitDelegate initDelegate;
	HideUnityDelegate hideUnityDelegate;
	OnRoundInformationReceivedDelegate onRoundInformationReceivedDelegate;

	void Awake ()
	{
		initDelegate = OnFacebookInitialized;
		hideUnityDelegate = OnHideUnity;
		onRoundInformationReceivedDelegate = OnRoundInformationReceived;
		FB.Init (OnFacebookInitialized, OnHideUnity);
	}

	void OnFacebookLogin (IResult result)
	{
		if (result == null) {
			Debug.Log ("Facebook Response: null?!");
			return;
		}

		if (!string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Facebook Response: Error");
			Debug.Log (result.Error);
		} else if (result.Cancelled) {
			Debug.Log ("Facebook Response: Cancelled");
			Debug.Log (result.RawResult);
		} else if (!string.IsNullOrEmpty (result.RawResult)) {
			Debug.Log ("Facebook Response: Success");
			Debug.Log (result.RawResult);
		} else {
			Debug.Log ("Facebook Response: Empty");
			Debug.Log (result.RawResult);
		}
	}

	void ResetGame ()
	{
		Lives.Remaining = StartLivesAmount;
		Gold.StartAmount = StartGoldAmount;
		resetTowerPositions ();
		spawner.Reset ();
		gameState = GameState.SETUP;
	}

	void OnFacebookInitialized ()
	{
		Debug.Log ("Facebook initialized");
		FB.GetAppLink (StoreUrlWithParameters);
		gameState = GameState.PRESETUP;
		Lives.OnZeroLives += Lose;
		ResetGame ();
		StartCoroutine (GameServerRestApi.TestGet (onRoundInformationReceivedDelegate));
		StartCoroutine (GameServerRestApi.TestPost ());
		Debug.Log ("Logged in? " + FB.IsLoggedIn);
		var permissions = new List<string> () {
			"public_profile",
			"email",
			"user_friends"
		};
		FB.LogInWithReadPermissions (permissions, this.OnFacebookLogin);
	}

	void StoreUrlWithParameters (IAppLinkResult result)
	{
		var delimiters = new char[] { '?' };
		string[] splitUrl = result.Url.Split (delimiters, 2);

		GameServerRestApi.BaseUrl = splitUrl [0];
		GameServerRestApi.UrlParameters = splitUrl [1];
		Debug.Log (result.Url);
	}

	void OnRoundInformationReceived (RoundInformation roundInformation)
	{
		Debug.Log ("I have round information!");
	}

	void OnHideUnity (bool isUnityShown)
	{
		Debug.Log ("OnHideUnity: " + isUnityShown);
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
			UnityEngine.Object.Destroy (child.gameObject);
		}
	}
}
