using UnityEngine;
using System.Collections;

public static class Lives
{
	private static int remaining = 10;

	public static int Remaining {
		get {
			return remaining;
		}

		set {
			remaining = value;
			if (remaining <= 0) {
				OnZeroLives ();
			}
		}
	}

	public delegate void OnZeroLivesDelegate ();

	public static OnZeroLivesDelegate OnZeroLives = delegate {
	};
}
