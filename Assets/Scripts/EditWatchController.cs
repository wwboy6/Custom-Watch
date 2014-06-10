using UnityEngine;
using System.Collections;

public class EditWatchController : MonoBehaviour {

	public MechanicalWatch watch;

	// Use this for initialization
	void Start () {
		watch.gameObject.SetActive(true);
	}

}
