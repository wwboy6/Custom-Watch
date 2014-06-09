using UnityEngine;
using System.Collections;

public class KButton : MonoBehaviour {

	public MonoBehaviour target = null;
	public string callbackName = "";
	
	void Start () {
		if (target == null || callbackName == "") {
			this.enabled = false;
		}
	}
	
	public void OnMouseUpAsButton() {
		target.SendMessage(callbackName, this);
	}
	
}
