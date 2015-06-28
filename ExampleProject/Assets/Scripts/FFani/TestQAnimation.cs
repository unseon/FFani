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

	FFaniMomentMap momentMap = new FFaniMomentMap();



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
		anim.to = 350.0f;
		anim.delayTime = 3.0f;
		
		//anim.valueTo = new Vector3(5.0f, 0, 0);
		anim.duration = 5.0f;


		//FFaniAnimation.Callback finishCallback = () => {Debug.Log ("onFinishCallback lambda called");};

		//anim.onStartCallback = () => {Debug.Log ("onStartCallback lambda called");};
		//anim.onFinishCallback = finishCallback;
		
		//anim.start();

		FFaniMemberAnimation anim01 = new FFaniMemberAnimation();
		anim01.targetComponent = GameObject.Find ("Cube").transform;
		anim01.propertyName = "px";
		anim01.to = 10.0f;
		anim01.duration = 3.0f;
		//anim01.easingFunction = Easing.InElastic;

		FFaniMemberAnimation anim02 = new FFaniMemberAnimation();
		anim02.targetComponent = GameObject.Find ("Cube").transform;
		anim02.propertyName = "position.y";
		anim02.to = 5.0f;
		anim02.duration = 3.0f;
		anim02.delayTime = 1.0f;

//		FFaniSequentialAnimation seqAnim = new FFaniSequentialAnimation();
//		seqAnim.add (anim01);
//		seqAnim.add (anim02);
//		seqAnim.start ();

		FFaniParallelAnimation parAnim = new FFaniParallelAnimation();
		parAnim.add (anim01);
		parAnim.add (anim02);
		parAnim.start ();


//		anim01.start ();
//		anim02.start ();

//		FFaniMoment moment01 = new FFaniMoment();
//
//		FFaniMemberValue mv01 = new FFaniMemberValue();
//
//		mv01.member = FFani.getTargetMember(tr, "position.x");
//		mv01.value  = 10.0f; 
//
//		moment01.name = "moment01";
//		moment01.add (mv01);
//
//		momentMap.add (moment01);
//
//		FFaniMoment moment02 = new FFaniMoment();
//
//		FFaniMemberValue mv02 = new FFaniMemberValue();
//		
//		mv02.member = FFani.getTargetMember(tr, "position.x");
//		mv02.value  = 0.0f; 
//
//		moment02.name = "moment02";
//		moment02.add (mv02);
//		momentMap.add (moment02);
//
//		FFaniMomentLink link = new FFaniMomentLink();
//
//		link.from = "moment01";
//		link.to = "moment02";
//
//		link.animList.Add(anim01);
//
//		momentMap.addLink(link);
//
//
//		Debug.Log ("Member compare:" + (anim01.member.isEqual(mv01.member)));
//		
//		momentMap.moment = "moment01";


	}
	// Update is called once per frame
	void Update () {
	
		
	}
	
	int ParameterFunc(int t1 = 0, int t2 = 0) {
		return t1 * t2;
	}

	public void onChangeMoment() {
		Debug.Log ("onChangeMoment");
		momentMap.moment = "moment02";
	}
}
