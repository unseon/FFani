using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class MyClass{
	public float myValue;
}

public class TestQAnimation : MonoBehaviour {

	public float myValue = 2.0f;
	
	public Vector3 myVector = new Vector3();
	
	public MyClass myObj = new MyClass();

	// Use this for initialization
	void Awake () {
		Transform tr = GameObject.Find ("Cube").transform;
	
		FFaniPropertyAnimation anim = new FFaniPropertyAnimation();
		anim.targetComponent = GameObject.Find ("Cube").transform;
		anim.propertyName = "position.x";
		anim.valueTo = 15.0f;

//		anim.propertyName = "localRotation";
//		anim.valueTo = Quaternion.Euler(179.0f, 0, 0);
		
		//anim.valueTo = new Vector3(5.0f, 0, 0);
		anim.duration = 10.0f;
		
		anim.start();
		
		//tr.localRotation = Quaternion.Euler(new Vector3(45.0f, 0, 0));
		//tr.localRotation.w = 1.0f;
		
		Quaternion q = new Quaternion();
		q.w = 1.0f;
		
		Vector3 v = new Vector3();
		v.x = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	int ParameterFunc(int t1 = 0, int t2 = 0) {
		return t1 * t2;
	}
}
