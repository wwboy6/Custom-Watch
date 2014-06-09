using UnityEngine;
using System.Collections;

public class KPMButton : MonoBehaviour {

	public PlayMakerFSM fsm;
	public string eventName = "";
	
	void Start () {
		if (fsm == null || eventName == "") {
			this.enabled = false;
		}
	}
	
	public void OnMouseUpAsButton() {
		fsm.Fsm.BroadcastEvent(eventName);
	}
	
}
