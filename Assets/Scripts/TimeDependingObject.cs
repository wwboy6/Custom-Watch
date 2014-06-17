using UnityEngine;
using System.Collections;

public abstract class TimeDependingObject : MonoBehaviour {

	public MechanicalWatch mw;
	public float timeOffset = 0;

	[SerializeField]
	protected float cycle = 1;
	public virtual float getCycle() { return cycle; }
	public virtual void setCycle(float value) { cycle = value; }
	
	[SerializeField]
	protected float period = .25f;
	public virtual float getPeriod() { return period; }
	public virtual void setPeriod(float value) { period = value; }

	public Transform model;

	protected virtual void Start () {
		if (mw == null) mw = MechanicalWatch.currentMW;
		
		if (model == null) model = transform.Find("model");
		if (model == null) model = transform;
	}
	
	protected virtual void Update () {
		if (mw.isRunning) {
			updateWithWatch();
		}
	}
	
	public virtual void updateWithWatch() {
		float time = mw.timePassed+timeOffset;
		if (period != 0) time = ((int) (time/period)) * period;
		time /= cycle;
		time = time - ((int)time);
		mwOnUpdate(time);
	}
	
	protected abstract void mwOnUpdate(float time);
	
}