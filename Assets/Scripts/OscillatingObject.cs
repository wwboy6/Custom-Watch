using UnityEngine;
using System.Collections;

public class OscillatingObject : TimeDependingObject {
	
	public override void setCycle(float value) {
		base.setCycle(value);
		if (period != 0) period = cycle/2;
	}
	public override void setPeriod(float value) {
		base.setPeriod(value);
		if (value != 0) {
			cycle = period*2;
		}
	}

	public float oscillateAngleMax = 120;

	protected override void Start () {
		base.Start();

		if (period != 0) cycle = period*2;
	}

	protected override void mwOnUpdate(float time) {
		//map 0 -> 1 to 0 -> max -> -max -> 0
		float angle = Mathf.PingPong(time*2, 1)*oscillateAngleMax*2-oscillateAngleMax;

		Debug.Log ("OscillatingObject::mwOnUpdate "+time+" "+angle);

		model.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

}
