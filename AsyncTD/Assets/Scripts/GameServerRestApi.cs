using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BrutalHack.AsyncTD;

public class GameServerRestApi
{
	static string gameRequest = "game?";
	static string idParameter = "id=";
	static string serverUrl = "http://duemmer.informatik.uni-oldenburg.de:3032/AsyncTDServer/";
	static string AND = "&";

	public static IEnumerator TestGet (OnRoundInformationReceivedDelegate callback)
	{
		string requestStatement = serverUrl + gameRequest + idParameter + "justice";
		Debug.Log ("Requesting GET from: " + requestStatement);
		WWW www = new WWW (requestStatement);
		yield return www;
		RoundInformation roundInformation = new RoundInformation (www.text);
		callback (roundInformation);
	}

	public static IEnumerator TestPost ()
	{
		Dictionary<string, string> headers = new Dictionary<string, string> ();
		headers.Add ("Content-Type", "application/json");

		string json = "{\"playerActions\":\"stuffDoge\",\"facebookId\":\"facebookDude\"}";
		byte[] body = Encoding.UTF8.GetBytes (json);
	
		string requestStatement = serverUrl + gameRequest + idParameter + "justice";
		Debug.Log ("Requesting PUT from: " + requestStatement);
		WWW www = new WWW (requestStatement, body, headers);
		yield return www;
		Debug.Log (www.text);
	}
}