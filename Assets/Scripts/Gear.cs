using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gear : RotatingObject {
	
	[SerializeField]
	protected int _toothCount = 0;
	public int toothCount {
		get {
			return _toothCount;
		}

		set {
			cps *= value/_toothCount;
			_toothCount = value;
			refresh();
		}
	}
	
	[SerializeField]
	protected List<Transform> toothes = new List<Transform>();

	// Use this for initialization
	protected override void Start () {
		base.Start();
		refresh();
		cps = toothCount*2f;
	}

	//protected override void Update () {
		//base.Update();
	//}
	
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

		GameObject toothPrefab = (GameObject) Resources.Load("Prefabs/SquareGearTooth");
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
