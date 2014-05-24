using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class EditModeMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Transform gearsMenu = GetComponent<PlayMakerFSM>().Fsm.GetFsmGameObject("gearsMenu").Value.transform;

		GameObject[] gearButtons = Resources.LoadAll<GameObject>("Prefabs/GearButtons");

		Vector3 pos = new Vector3(0, 1.2f, 0);
		Vector3 diff = new Vector3(0, 1.2f, 0);
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
