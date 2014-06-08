using UnityEngine;
using System.Collections;

public class CustomWatchRuntime : MonoBehaviour {

	public static CustomWatchRuntime sharedInstance;

	public int currentGearToothPeroidNumerator = 1;

	// Use this for initialization
	void Start () {
		sharedInstance = this;
	}
	
}
