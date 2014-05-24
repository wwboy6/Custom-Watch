using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;

public class Gear : RotatingObject {
	
	[SerializeField]
	protected int _toothCount = 0;
	public int toothCount {
		get {
			return _toothCount;
		}

		set {
			cycle *= value/_toothCount;
			_toothCount = value;
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
		cycle = toothCount;
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
			tooth.parent = transform;
			tooth.localPosition = Vector3.zero;
			tooth.localScale = Vector3.one;
			tooth.localRotation = Quaternion.identity;
			tooth.Rotate(new Vector3(0, 0, 360f*i/toothCount));
			toothes.Add(tooth);
		}
	}
}
