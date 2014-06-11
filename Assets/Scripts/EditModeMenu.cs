using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class EditModeMenu : MonoBehaviour {

	public PlayMakerFSM editWatchFSM;
	public MechanicalWatch watch;

	public Transform buttonFocus;
	public GameObject runningIndicator;
	public KMenu topMenu;
	public KMenu gearsMenu;
	public KMenu rosMenu;
	public KMenu sosMenu;
	public KMenu oosMenu;
	public KMenu modifyMenu;
	public GameObject toothCountButton;
	public GameObject speedButton;
	public KNumberField toothCountNF;
	public KNumberField speedNumeratorNF;
	public KNumberField speedFactorNF;

	public Transform away;

	GameObject watchPart;
	Gear gear;
	RotatingObject rotatingObject;

	void prepareItemMenu(string buttonPrefabFolder, KMenu menu, string callbackName) {
		GameObject[] buttonPrefabs = Resources.LoadAll<GameObject>("Prefabs/"+buttonPrefabFolder);

		Vector3 pos = new Vector3(0, 1.2f, 0);
		Vector3 diff = new Vector3(0, 1.2f, 0);
		foreach (GameObject buttonPrefab in buttonPrefabs) {
			Transform btn = ((GameObject) Instantiate (buttonPrefab)).transform;
			btn.name = buttonPrefab.name;
			btn.parent = menu.transform;
			btn.localPosition = pos;
			
			KButton button = btn.gameObject.AddComponent<KButton>();
			button.target = this;
			button.callbackName = callbackName;
			
			pos += diff;
		}
	}

	// Use this for initialization
	void Start () {
		topMenu = GetComponent<KMenu>();

		runningIndicator.SetActive(watch.isRunning);

		prepareItemMenu ("GearButtons", gearsMenu, "gearButtonOnPressed");
		prepareItemMenu ("RotatingObjectButtons", rosMenu, "roButtonOnPressed");
		prepareItemMenu ("StaticObjectButtons", sosMenu, "sosButtonOnPressed");
		prepareItemMenu ("OtherObjectButtons", oosMenu, "oosButtonOnPressed");
		
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
		selectWatchPart(null);
		hideButtonFocus();
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
	
	public void roButtonOnPressed(KButton sender) {
		FsmGameObject targetPrefab = editWatchFSM.Fsm.GetFsmGameObject("targetPrefab");
		targetPrefab.Value = (GameObject) Resources.Load("Prefabs/RotatingObjects/"+sender.name);
		FsmGameObject currentObject = editWatchFSM.Fsm.GetFsmGameObject("currentObject");
		currentObject.Value = null;
		FsmGameObject returnReceiver = editWatchFSM.Fsm.GetFsmGameObject("returnReceiver");
		returnReceiver.Value = gameObject;
		editWatchFSM.Fsm.BroadcastEvent("CHOOSE_LOC");
		
		rosMenu.hide();
		hideButtonFocus();
	}

	public void updateMaterial(GameObject wp) {
		bool isSelect = wp == this.watchPart;

		Behaviour[] materialBanks = wp.transform.GetComponentsInChildren<MaterialBank>();
		foreach (MaterialBank materialBank in materialBanks) {
			if (isSelect) materialBank.select();
			else materialBank.reset();
		}
	}

	public void selectWatchPart(GameObject watchPart) {
		GameObject prevWP = this.watchPart;
		
		this.watchPart = watchPart;

		//deselect effect

		updateMaterial(prevWP);

		//check null

		if (watchPart == null) {
			gear = null;
			rotatingObject = null;
			return;
		}
		
		//select effect

		updateMaterial(watchPart);

		//check part type

		gear = watchPart.GetComponent<Gear>();
		rotatingObject = watchPart.GetComponent<RotatingObject>();
		
		if (gear != null) {
			toothCountButton.SetActive(true);
			speedButton.SetActive(true);

			toothCountNF.updateValue(gear.getToothCount());
			speedNumeratorNF.updateValue(gear.getToothPeriodNumerator());
			speedFactorNF.updateValue(gear.getToothPeriodFactor());
			
			modifyMenu.show();
		} else if (rotatingObject != null) {
			toothCountButton.SetActive(false);
			speedButton.SetActive(false);

			modifyMenu.show();
		}
	}

	public void toothCountOnChange(KNumberField numberField) {
		gear.setToothCount(numberField.getValue());
		updateMaterial(watchPart);
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

	public void rotateXButtonOnPressed(KButton sender) {
		gear.addRotationX(90);
	}
	
	public void rotateYButtonOnPressed(KButton sender) {
		gear.addRotationY(90);
	}

	public void deleteButtonOnPressed(KButton sender) {
		topMenu.resetToRootMenu();
		Destroy(watchPart);
	}

}
