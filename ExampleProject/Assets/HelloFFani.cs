using UnityEngine;
using System.Collections;

public class HelloFFani : MonoBehaviour {

	FFaniMation anim;
	// Use this for initialization
	void Start () {

		anim = 
			FFani.Step (
				FFani.Tween (
					target: transform,
					propertyName: "lry",
					to: 360.0f,
					duration: 2.0f
				),
				FFani.Tween(
					target: transform,
					propertyName: "py",
					to: 5.0f
				),
				FFani.Tween (
					target: transform,
					propertyName: "px",
					to: 5.0f,
					duration: 2.0f
				)
			).SetInterval(0.0f);
		anim.Start ();

//		FFani.Tween (
//			target: transform,
//			propertyName: "px",
//			from: -5.0f,
//			to: 5.0f,
//			delay: 2.0f,
//			duration: 2.0f,
//			easingCurve: FFaniEasing.OutCubic
//		).Start ();
//
//		FFani.Tween (
//			target: GameObject.Find ("Cube01").GetComponent<Transform>(),
//			propertyName: "px",
//			from: -5.0f,
//			to: 5.0f,
//			delay: 2.0f,
//			duration: 2.0f
//		).Start ();

	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click.");
			anim.Complete();
		}
	}
}
