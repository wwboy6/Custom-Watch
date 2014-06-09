using UnityEngine;
using System.Collections;

public class KMenuButton : MonoBehaviour {

	public KMenu targetMenu = null;

	void Start () {
		if (targetMenu == null) {
			this.enabled = false;
		}
	}
	
	public void OnMouseUpAsButton() {
		if (targetMenu.gameObject.activeSelf) {
			targetMenu.hide();
		} else {
			targetMenu.show();
		}
	}
	
}
