﻿using UnityEngine;
using System.Collections;

public abstract class TimeDependingObject : MonoBehaviour {

	public MechanicalWatch mw;
	public float timeOffset = 0;
	public float cycle = 1;
	public float period = .25f;

	protected virtual void Start () {
		if (mw == null) mw = MechanicalWatch.currentMW;
	}
	
	protected virtual void Update () {
		if (mw.isRunning) {
			updateWithWatch();
		}
	}
	
	protected virtual void updateWithWatch() {
		float time = mw.timePassed+timeOffset;
		if (period != 0) time = ((int) (time/period)) * period;
		time /= cycle;
		time = time - ((int)time);
		mwOnUpdate(time);
	}
	
	protected abstract void mwOnUpdate(float time);
	
}