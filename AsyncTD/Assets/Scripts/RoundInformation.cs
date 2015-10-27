using SimpleJSON;
using UnityEngine;
using System;

public class RoundInformation
{
	public RoundInformation (string json)
	{
		JSONNode node = JSONNode.Parse (json);
		if (node ["game"] != null) {
			//TODO read round information
		}
		if (node ["enemies"] != null) {
			//TODO read enemies information
		} else {
			throw new NotSupportedException ("Enemies JSON is empty! This game cannot be played!");
		}
		Debug.Log ("JSON Game: " + node ["game"].ToString ());
		Debug.Log ("JSON Enemies: " + node ["enemies"].ToString ());
	}
}
