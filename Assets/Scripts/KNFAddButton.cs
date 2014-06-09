using UnityEngine;
using System.Collections;

public class KNFAddButton : MonoBehaviour {

	public KNumberField numberField = null;

	//TODO: relative change
	public float valueChange = 1;

	void Start () {
		if (numberField == null) {
			foreach (Transform t in transform.parent) {
				numberField = t.GetComponent<KNumberField>();
				if (numberField != null) break;
			}
		}
	}

	void OnMouseUpAsButton() {
		numberField.setValue(numberField.getValue() + (int)valueChange);
	}

}
