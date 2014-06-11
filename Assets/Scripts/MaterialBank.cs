using UnityEngine;
using System.Collections;

public class MaterialBank : MonoBehaviour {
	
	public Material[] normalMaterials;
	public Material[] selectedMaterials;

	int index = 0;
	
	void Start () {
	
	}

	public void reset(int index) {
		this.index = index;
		renderer.material = normalMaterials[index];
	}

	public void reset() { reset(index); }

	public void select(int index) {
		renderer.material = selectedMaterials[index];
	}

	public void select() { select(index); }

}
