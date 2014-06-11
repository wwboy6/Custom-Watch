using UnityEngine;
using System.Collections;

public class KNumberField : MonoBehaviour {

	public MonoBehaviour target;
	public string callbackName;

	public TextMesh label = null;

	[SerializeField]
	int value = 5;
	public int getValue () { return value; }
	public void updateValue(int v) {
		value = Mathf.Clamp(v, min, max);
		updateUI();
	}
	public void setValue (int v) {
		updateValue(v);
		target.SendMessage(callbackName, this);
	}

	public int min = 1;
	public int max = 10;
	public float draggingScale = 50;
	public float draggingSpeedMin = 5;

	Vector2 startPos;
	float draggingSpeed;
	int startValue;

	void Awake () {
		if (label == null) {
			label = transform.GetChild(0).GetComponent<TextMesh>();
		}

		updateUI();
	}

	void OnMouseDown() {
		startPos = Input.mousePosition;
		draggingSpeed = Mathf.Min(value, draggingSpeedMin);
		startValue = value;
	}

	void OnMouseDrag() {
		float dist = (Input.mousePosition.y - startPos.y) / draggingScale;
		setValue (startValue + (int) (dist * draggingSpeed));
	}

	void updateUI() {
		if (gameObject.activeInHierarchy) label.text = ""+value;
	}

}
