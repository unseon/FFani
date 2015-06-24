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
	
		FFaniMemberAnimation anim = new FFaniMemberAnimation();
		anim.targetComponent = GameObject.Find ("Cube").transform;
//		anim.propertyName = "position.x";
//		anim.valueTo = 15.0f;

//		anim.propertyName = "localRotation";
//		anim.valueTo = Quaternion.Euler(179.0f, 0, 0);

		anim.propertyName = "localRotation.eulerAngles.x";
		anim.valueTo = 350.0f;
		anim.delayTime = 3.0f;
		
		//anim.valueTo = new Vector3(5.0f, 0, 0);
		anim.duration = 5.0f;


		//FFaniAnimation.Callback finishCallback = () => {Debug.Log ("onFinishCallback lambda called");};

		//anim.onStartCallback = () => {Debug.Log ("onStartCallback lambda called");};
		//anim.onFinishCallback = finishCallback;
		
		//anim.start();

		FFaniMemberAnimation anim01 = new FFaniMemberAnimation();
		anim01.targetComponent = GameObject.Find ("Cube").transform;
		anim01.propertyName = "position.x";
		anim01.valueTo = 10.0f;
		anim01.duration = 5.0f;

		FFaniMemberAnimation anim02 = new FFaniMemberAnimation();
		anim02.targetComponent = GameObject.Find ("Cube").transform;
		anim02.propertyName = "position.y";
		anim02.valueTo = 5.0f;
		anim02.duration = 5.0f;

		FFaniSequentialAnimation seqAnim = new FFaniSequentialAnimation();
		seqAnim.add (anim01);
		seqAnim.add (anim02);
		seqAnim.start ();

//		anim01.start ();
//		anim02.start ();
	}
	// Update is called once per frame
	void Update () {
	
		
	}
	
	int ParameterFunc(int t1 = 0, int t2 = 0) {
		return t1 * t2;
	}
}
