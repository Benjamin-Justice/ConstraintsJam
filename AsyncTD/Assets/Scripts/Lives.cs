using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour
{
	public int StartAmount;
	private int remaining = 10;

	public int Remaining {
		get {
			return remaining;
		}

		set {
			this.remaining = value;
			if (remaining <= 0) {
				OnZeroLives ();
			}
		}
	}

	public delegate void OnZeroLivesDelegate ();

	public OnZeroLivesDelegate OnZeroLives = delegate {
	};

	public void Reset ()
	{
		Remaining = StartAmount;
	}
}
