using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;

public class Gear : RotatingObject {

	public static int defaultToothCount = 12;
	
	[SerializeField]
	protected int toothPeriodNumerator;
	public int getToothPeriodNumerator() { return toothPeriodNumerator; }
	public void setToothPeriodNumerator(int numerator) {
		this.toothPeriodNumerator = numerator;
		updateCycle();
	}
	
	[SerializeField]
	protected int toothPeriodFactor;
	public int getToothPeriodFactor() { return toothPeriodFactor; }
	public void setToothPeriodFactor(int factor) {
		this.toothPeriodFactor = factor;
		updateCycle();
	}
	
	[SerializeField]
	protected int _toothCount = 0;
	public int toothCount {
		get {
			return _toothCount;
		}

		set {
			//cycle *= (value*1.0f)/_toothCount;
			_toothCount = value;
			updateCycle();
			refresh();
		}
	}
	public void setToothCount(int i) { toothCount = i; }
	public int getToothCount() { return toothCount; }
	
	[SerializeField]
	protected string _toothName = "SquareGearTooth";
	public string toothName {
		get {
			return _toothName;
		}
		
		set {
			_toothName = value;
			refresh();
		}
	}
	
	[SerializeField]
	protected List<Transform> toothes = new List<Transform>();

	// Use this for initialization
	protected override void Start () {
		base.Start();
		refresh();
		
		//cycle = toothCount;
		toothPeriodNumerator = CustomWatchRuntime.sharedInstance.currentGearToothPeroidNumerator;
		toothPeriodFactor = CustomWatchRuntime.sharedInstance.currentGearToothPeroidFactor;
		updateCycle();
	}
	
	protected void updateCycle() {
		cycle = toothCount * toothPeriodNumerator / toothPeriodFactor;
	}

	//protected override void Update () {
		//base.Update();
	//}
	
	protected GameObject getToothPrefab() {
		return (GameObject) Resources.Load("Prefabs/GearTooths/"+toothName);
	}
	
	[ContextMenu("Refresh")]
	public void refresh() {
		foreach(Transform tooth in toothes) {
			if (Application.isPlaying) {
				Destroy(tooth.gameObject);
			} else {
				DestroyImmediate(tooth.gameObject);
			}
		}
		toothes.Clear();

		GameObject toothPrefab = getToothPrefab();
		for (int i=0; i<toothCount; ++i) {
			Transform tooth = ((GameObject) Instantiate(toothPrefab)).transform;
			tooth.parent = model;
			tooth.localPosition = Vector3.zero;
			tooth.localScale = Vector3.one;
			float toothScale = 1.0f * defaultToothCount / toothCount;
			tooth.Find("modelWrapper").localScale = new Vector3(toothScale, toothScale, 1);
			tooth.localRotation = Quaternion.identity;
			tooth.Rotate(new Vector3(0, 0, 360f*i/toothCount));
			toothes.Add(tooth);
		}
	}
}
