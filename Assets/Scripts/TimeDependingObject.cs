using UnityEngine;
using System.Collections;

public abstract class TimeDependingObject : MonoBehaviour {

	public MechanicalWatch mw;
	public float timeOffset = 0;
	public float cps = 1;
	public float period = 1;

	protected virtual void Start () {
		if (mw == null) mw = MechanicalWatch.currentMW;
	}
	
	protected virtual void Update () {
		if (mw.isRunning) {
			float time = mw.timePassed+timeOffset;
			if (period != 0) time = ((int) (time/period)) * period;
			time /= cps;
			mwOnUpdate(time);
		}
	}
	
	protected abstract void mwOnUpdate(float time);
	
}