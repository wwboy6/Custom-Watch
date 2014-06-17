using UnityEngine;
using System.Collections;

public class CustomWatchRuntime : MonoBehaviour {

	public static CustomWatchRuntime sharedInstance;
	
	public int currentGearToothPeroidNumerator = 1;
	public int currentGearToothPeroidFactor = 1;

	public float currentWatchPartPeriod = .25f;

	// Use this for initialization
	void Awake () {
		sharedInstance = this;
	}
	
}
