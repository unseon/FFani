using UnityEngine;
using System.Collections;
using System;

public class TestQAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FFAnim anim = new FFAnim();
		anim.target = GameObject.Find ("Cube");
		anim.componentName = "Transform";
		anim.propertyName = "position";
		anim.valueTo = new Vector3(10, 10, 10);
		anim.duration = 1.0f;
		
		anim.start();
		
		object a = 10.0f;
		
		Debug.Log (a.ToString());
		Debug.Log (a.GetType() == typeof(float));
		object b = new Vector3(1.0f, 2.0f, 3.0f);
		Vector3 c = (Vector3)b;
		Debug.Log (c);
		
		Debug.Log ("parameter test: " + ParameterFunc(t1: 2, t2:3));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	int ParameterFunc(int t1 = 0, int t2 = 0) {
		return t1 * t2;
	}
}
