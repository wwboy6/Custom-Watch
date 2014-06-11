using UnityEngine;
using System.Collections;

public class Test_ColorPick : MonoBehaviour {

	public Color color0;
	public Color color1;

	// Use this for initialization
	void Start () {
		int count = 10;
		for (int i=0; i<count; i++) {
			SpriteRenderer s = transform.Find("/btn"+i).GetComponent<SpriteRenderer>();
			s.color = color1*i/(count-1) + color0*(count-1-i)/(count-1);
			Debug.Log (i+ " " + s.color*255);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
