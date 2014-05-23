using UnityEngine;
using System.Collections;

public class MechanicalWatch : MonoBehaviour {

	public static MechanicalWatch currentMW;
	
	protected float _timePassed;
	public float timePassed {
		get { return _timePassed; }
		protected set { _timePassed = value; }
	}
	
	[SerializeField]
	protected bool _isRunning = false;
	public bool isRunning {
		get { return _isRunning; }
		protected set { _isRunning = value; }
	}
	public bool getIsRunning() { return isRunning; }
	public void setIsRunning(bool value) { isRunning = value; }

	// Use this for initialization
	void Start () {
		currentMW = this;
		isRunning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning) {
			timePassed += Time.deltaTime;
		}
	}
	
	public void run() {
		isRunning = true;
	}
	
	public void stop() {
		isRunning = false;
	}
	
}
