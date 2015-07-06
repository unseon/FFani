using UnityEngine;
using UnityEngine.UI;
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

		FFaniDeltaAnimation danim = new FFaniDeltaAnimation();
		danim.targetComponent = GameObject.Find ("Cube").transform;
		danim.propertyName = "position.y";
		danim.to = 5.0f;
		danim.duration = 3.0f;
		//danim.Start();

		FFaniDeltaAnimation danim2 = new FFaniDeltaAnimation();
		danim2.targetComponent = GameObject.Find ("Cube").transform;
		danim2.propertyName = "ry";
		danim2.from = -45.0f;
		danim2.duration = 3.0f;
		//danim2.Start();

		FFani.Serial (
			FFani.Prompt(
				target: GameObject.Find ("Text").GetComponent<Text>(),
				propertyName: "enabled",
				to: false
			),
			FFani.Tween(
				target: GameObject.Find ("Panel").GetComponent<Image>(),
				propertyName: "color.a",
				from: 1.0f,
				to: 0.0f,
				duration: 3.0f
			),
			FFani.Prompt(
				target: GameObject.Find ("Text").GetComponent<Text>(),
				propertyName: "enabled",
				to: true
			),
			FFani.Tween(
				target: GameObject.Find ("Cube").GetComponent<Renderer>(),
				propertyName: "material.color",
				to: new Color(0, 1, 0)
			),
			FFani.Tween(
				target: GameObject.Find ("Cube").GetComponent<Renderer>(),
				propertyName: "material.color.b",
				to: 1.0f
			),
			FFani.Action(
				()=>{
				Debug.Log ("FFani.Action!!!");
				}
			),
			FFani.Tween(
				target: tr,
				propertyName: "pos",
				to: new Vector3(10, 10, 0),
				duration: 3.0f

			),
			FFani.Sleep(
				duration: 2.0f
			),
			FFani.Prompt(
				target: tr,
				propertyName: "py",
				to: 0.0f
			)
		).Remind(
			()=> {
				Debug.Log ("Serial.onFinished Callback lambda called");
			}
		).Start();


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
