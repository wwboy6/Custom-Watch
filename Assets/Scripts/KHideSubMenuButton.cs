using UnityEngine;
using System.Collections;

public class KHideSubMenuButton : MonoBehaviour {

	public KMenu menu = null;

	void Awake () {
		if (menu == null) menu = transform.parent.GetComponent<KMenu>();

		if (menu == null) enabled = false;
	}
	
	void Start () {
	}
	
	public void OnMouseUpAsButton() {
		menu.hideAllSubMenus();
	}
}
