using UnityEngine;
using System.Collections;

public class RotatingObject : TimeDependingObject {
	
	public Vector3 rotationAxis = new Vector3 (0, 0, -1);
	public Quaternion rotationModifier = Quaternion.identity;

	protected override void Start () {
		base.Start();
	}
	
	//protected override void Update () {
		//base.Update();
	//}
	
	protected override void mwOnUpdate(float time) {
		transform.localRotation = rotationModifier * Quaternion.AngleAxis(360*time, rotationAxis);
	}
	
	public void addRotationX(float degree) {
		rotationModifier *= Quaternion.Euler(degree, 0, 0);
		updateWithWatch();
	}
	
	public void addRotationY(float degree) {
		rotationModifier *= Quaternion.Euler(0, degree, 0);
		updateWithWatch();
	}
	
}
