using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class TestMoment : MomentBehaviour {

	// Use this for initialization
	protected override void Awake () {
		base.Awake();

		Transform cube0 = GameObject.Find("Cube").transform;
		Transform cube1 = GameObject.Find("Cube01").transform;

		Moments(
			FFani.Moment(
				name = "moment01",
				FFani.PropertyChange(cube0, "position.x", 10.0f),
				FFani.PropertyChange(cube1, "position.x", 10.0f)
			),
			FFani.Moment(
				name = "moment02",
				FFani.PropertyChange(cube0, "position.x", 0.0f),
				FFani.PropertyChange(cube1, "position.x", 0.0f)
			)
		);

		MomentMations(
			FFani.MomentMation(
				from: "moment01",
				to: "moment02",
				anim:
					FFani.Tween(
						target: cube0,
						propertyName: "position.x",
						duration: 0.5f
					)
			),
			FFani.MomentMation(
				from: "moment02",
				to: "moment01",
				anim:
					FFani.Tween(
						target: cube0,
						propertyName: "position.x",
						duration: 0.5f
					)
			)
		);

//		FFaniMomentMation link = new FFaniMomentMation();
//
//		link.from = "moment01";
//		link.to = "moment02";
//
//		link.blendAnim = 
//			FFani.Tween(
//				target: cube0,
//				propertyName: "position.x",
//				duration: 0.5f
//			);
//
//		AddMomentMation(link);

		moment = "moment02";
	}

	public void Go() {
		Debug.Log("Go");

		if ( moment == "moment01") {
			moment = "moment02";
		} else {
			moment = "moment01";
		}
	}
}