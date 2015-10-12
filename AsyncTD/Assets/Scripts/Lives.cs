using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {
	public int StartAmount;
	private int remaining = 10;
	public GameManager gameManager;
	public int Remaining {
		get {
			return remaining;
		}

		set {
			this.remaining = value;
			if (remaining <= 0) {
				gameManager.Lose ();
			}
		}
	}

	public void Reset ()
	{
		Remaining = StartAmount;
	}
}
