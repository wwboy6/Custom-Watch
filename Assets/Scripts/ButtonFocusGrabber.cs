using UnityEngine;
using System.Collections;

public class ButtonFocusGrabber : MonoBehaviour {

	public Transform buttonFocus;
	
	void Start () {
	}
	
	public void OnMouseUpAsButton() {
		buttonFocus.parent = transform;
		buttonFocus.localPosition = Vector3.zero;
	}

}
