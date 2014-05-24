using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class EditModeMenu : MonoBehaviour {

	public Transform rootMenu;
	public Transform gearsMenu;

	// Use this for initialization
	void Start () {

		GetComponent<PlayMakerFSM>().Fsm.GetFsmGameObject("rootMenu").Value = rootMenu.gameObject;
		GetComponent<PlayMakerFSM>().Fsm.GetFsmGameObject("gearsMenu").Value = gearsMenu.gameObject;

		GameObject[] gearButtons = Resources.LoadAll<GameObject>("Prefabs/GearButtons");

		Vector3 pos = new Vector3(0, 1.17f, 0);
		Vector3 diff = new Vector3(0, 1.17f, 0);
		foreach(GameObject gearButton in gearButtons) {
			Transform btn = ((GameObject) Instantiate (gearButton)).transform;
			btn.name = gearButton.name;
			btn.parent = gearsMenu;
			btn.localPosition = pos;

			pos += diff;
		}
	
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
}
