using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KMenu : MonoBehaviour {

	public bool isRootMenu = false;

	public List<KMenu> subMenus = new List<KMenu>();
	public KMenu superMenu = null;
	
	bool isAwaken = false;
	void Awake () {
		if (isAwaken) return;
		isAwaken = true;
		
		foreach (Transform child in transform) {
			KMenu subMenu = child.GetComponent<KMenu>();
			if (subMenu != null) {
				subMenus.Add(subMenu);
				subMenu.superMenu = this;
				subMenu.Awake();
			}
		}
		
		if (isRootMenu) show(); else hide();
	}
	
	public void show() {
		if (gameObject.activeSelf) return;
		
		superMenu.hideAllSubMenus();
		gameObject.SetActive(true);
	}
	
	public void hide() {
		if (!gameObject.activeSelf) return;
		
		hideAllSubMenus();
		gameObject.SetActive(false);
	}
	
	public void hideAllSubMenus() {
		foreach (KMenu subMenu in subMenus) {
			subMenu.hide();
		}
	}
	
	public void resetToRootMenu() {
		if (isRootMenu) {
			show();
		
			foreach (KMenu subMenu in subMenus) {
				subMenu.resetToRootMenu();
			}
		} else {
			hide();
		}
	}
	
}
