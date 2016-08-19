using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class TestMoment : MonoBehaviour {

	FFaniMomentMap momentMap = new FFaniMomentMap();
	// Use this for initialization
	void Awake () {
		Transform cube0 = GameObject.Find("Cube").transform;
		Transform cube1 = GameObject.Find("Cube01").transform;

		FFaniMoment moment01 = new FFaniMoment("moment01");
		moment01.PropertyChange(cube0, "position.x", 10.0f);
		momentMap.AddMoment (moment01);

		FFaniMoment moment02 = new FFaniMoment("moment02");
		moment02.PropertyChange(cube0, "position.x", 0.0f);
		momentMap.AddMoment (moment02);

		FFaniMomentMation link = new FFaniMomentMation();

		link.from = "moment01";
		link.to = "moment02";

		link.animList.Add(
			FFani.Tween(
				target: cube0,
				propertyName: "position.x",
				duration: 0.5f
			)
		);

		momentMap.AddMomentMation(link);

		momentMap.moment = "moment01";
	}

	public void Go() {
		Debug.Log("Go");
		momentMap.moment = "moment02";
	}
}