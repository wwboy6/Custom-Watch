using UnityEngine;
using System.Collections;

public class RotatingObject : TimeDependingObject {

	public Vector3 rotationAxis = new Vector3 (0, 0, -1);

	protected override void Start () {
		base.Start();
	}
	
	//protected override void Update () {
		//base.Update();
	//}
	
	protected override void mwOnUpdate(float time) {
		model.transform.localRotation = Quaternion.AngleAxis(360*time, rotationAxis);
	}
	
}
