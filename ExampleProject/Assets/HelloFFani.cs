using UnityEngine;
using System.Collections;

public class HelloFFani : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		FFani.Serial (
//			FFani.Mation (
//				target: transform,
//				propertyName: "lry",
//				to: 360.0f,
//				duration: 2.0f,
//			),
//			FFani.Prompt(
//				target: transform,
//				propertyName: "py",
//				to: 5.0f
//			),
//			FFani.Mation (
//				target: transform,
//				propertyName: "px",
//				to: 5.0f,
//				duration: 2.0f
//			)
//		).Start ();

		FFani.Tween (
			target: transform,
			propertyName: "px",
			from: -5.0f,
			to: 5.0f,
			delay: 2.0f,
			duration: 2.0f,
			easingCurve: FFaniEasing.OutCubic
		).Start ();

		FFani.Tween (
			target: GameObject.Find ("Cube01").GetComponent<Transform>(),
			propertyName: "px",
			from: -5.0f,
			to: 5.0f,
			delay: 2.0f,
			duration: 2.0f
		).Start ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
