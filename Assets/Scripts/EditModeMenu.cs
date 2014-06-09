using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class EditModeMenu : MonoBehaviour {

	public PlayMakerFSM editWatchFSM;
	public MechanicalWatch watch;

	public Transform buttonFocus;
	public GameObject runningIndicator;
	public KMenu gearsMenu;
	public KMenu modifyMenu;
	public KNumberField toothCountNF;
	public KNumberField speedNumeratorNF;
	public KNumberField speedFactorNF;

	public Transform away;

	GameObject watchPart;
	Gear gear;

	// Use this for initialization
	void Start () {
		runningIndicator.SetActive(watch.isRunning);

		GameObject[] gearButtons = Resources.LoadAll<GameObject>("Prefabs/GearButtons");

		Vector3 pos = new Vector3(0, 1.2f, 0);
		Vector3 diff = new Vector3(0, 1.2f, 0);
		foreach (GameObject gearButton in gearButtons) {
			Transform btn = ((GameObject) Instantiate (gearButton)).transform;
			btn.name = gearButton.name;
			btn.parent = gearsMenu.transform;
			btn.localPosition = pos;

			KButton button = btn.gameObject.AddComponent<KButton>();
			button.target = this;
			button.callbackName = "gearButtonOnPressed";

			pos += diff;
		}
		
		Component[] buttons = GetComponentsInChildren<KMenuButton>(true);
		foreach (KMenuButton button in buttons) {
			ButtonFocusGrabber buttonFocusGrabber = button.gameObject.AddComponent<ButtonFocusGrabber>();
			buttonFocusGrabber.buttonFocus = buttonFocus;
		}

		buttons = GetComponentsInChildren<KButton>(true);
		foreach (KButton button in buttons) {
			button.gameObject.AddComponent<KHideSubMenuButton>();
		}
		
		buttons = GetComponentsInChildren<KPMButton>(true);
		foreach (KPMButton button in buttons) {
			ButtonFocusGrabber buttonFocusGrabber = button.gameObject.GetComponent<ButtonFocusGrabber>();
			if (buttonFocusGrabber == null) {
				buttonFocusGrabber = button.gameObject.AddComponent<ButtonFocusGrabber>();
				buttonFocusGrabber.buttonFocus = buttonFocus;
			}

			if (button.gameObject.GetComponent<KHideSubMenuButton>() == null &&
			    button.gameObject.GetComponent<KMenuButton>() == null)
				button.gameObject.AddComponent<KHideSubMenuButton>();
		}
	
	}

	public void hideButtonFocus() {
		buttonFocus.transform.parent = away;
	}

	public void resetToRootMenu() {
		hideButtonFocus ();
	}

	public void onChooseLocationFinish(GameObject watchPart) {
		selectWatchPart (watchPart);
		hideButtonFocus();
	}

	public void onScaleWatchPartFinish() {
		hideButtonFocus();
	}
	
	public void onSetWatchPartDepthFinish() {
		hideButtonFocus();
	}

	public void gearButtonOnPressed(KButton sender) {
		FsmGameObject targetPrefab = editWatchFSM.Fsm.GetFsmGameObject("targetPrefab");
		targetPrefab.Value = (GameObject) Resources.Load("Prefabs/Gears/"+sender.name);
		FsmGameObject currentObject = editWatchFSM.Fsm.GetFsmGameObject("currentObject");
		currentObject.Value = null;
		FsmGameObject returnReceiver = editWatchFSM.Fsm.GetFsmGameObject("returnReceiver");
		returnReceiver.Value = gameObject;
		editWatchFSM.Fsm.BroadcastEvent("CHOOSE_LOC");

		gearsMenu.hide();
		hideButtonFocus();
	}

	public void selectWatchPart(GameObject watchPart) {
		this.watchPart = watchPart;
		gear = watchPart.GetComponent<Gear>();
		
		if (gear != null) {
			toothCountNF.setValue(gear.getToothCount());
			speedNumeratorNF.setValue(gear.getToothPeriodNumerator());
			speedFactorNF.setValue(gear.getToothPeriodFactor());
			
			modifyMenu.show();
		}
	}

	public void toothCountOnChange(KNumberField numberField) {
		gear.setToothCount(numberField.getValue());
	}

	public void runButtonOnPressed(KButton sender) {
		watch.setIsRunning(!watch.isRunning);
		runningIndicator.SetActive(watch.isRunning);
	}
	
	public void speedNumeratorOnChange(KNumberField numberField) {
		gear.setToothPeriodNumerator(numberField.getValue());
	}
	
	public void speedFactorOnChange(KNumberField numberField) {
		gear.setToothPeriodFactor(numberField.getValue());
	}

}
