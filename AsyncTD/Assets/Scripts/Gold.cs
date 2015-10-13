using UnityEngine;
using System.Collections;
using System;

public static class Gold
{
	private static int startAmount;

	public static int StartAmount {
		get { 
			return startAmount;		
		}
		set { 
			startAmount = value;
			GoldCount = value;
		}
	}

	private static int GoldCount;

	public static  bool useGold (int amount)
	{
		if (amount > GoldCount) {
			return false;
		} else {
			GoldCount -= amount;
			return true;
		}
	}

	public static bool hasGold (int amount)
	{
		return amount <= GoldCount;
	}

	public  static void addGold (int amount)
	{
		if (amount < 0) {
			throw new ArgumentOutOfRangeException ("Can't add negative amount of Gold");
		} else {
			GoldCount += amount;
			Debug.Log ("new gold" + GoldCount);
		}
	}
}
