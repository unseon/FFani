using UnityEngine;
using System.Collections;

public class HelloFFani : MonoBehaviour {

	public class TestTrigger {
		public FFani.Callback OnClick;

		public void go() {
			OnClick();
		}
	}

	TestTrigger testTrigger = new TestTrigger();


	public FFani.Callback OnClick;

	FFaniStepAnimation anim;
	// Use this for initialization
	void Start () {


//		anim = 
//			FFani.Step (
//				FFani.Tween (
//					target: transform,
//					propertyName: "lry",
//					to: 360.0f,
//					duration: 2.0f
//				),
//				FFani.Tween(
//					target: transform,
//					propertyName: "py",
//					to: 5.0f
//				),
//				FFani.Tween (
//					target: transform,
//					propertyName: "px",
//					to: 5.0f,
//					duration: 2.0f
//				)
//			).SetInterval(2.0f)
//			.SetSkipTrigger(ref OnClick);
//		anim.Start ();
		//testTrigger.OnClick += anim.Next;

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
		FFani.Tween (
			target: GameObject.Find ("Panel").GetComponent<RectTransform>(),
			propertyName: "anchoredPosition.x",
			from: -50.0f,
			to: 50.0f,
			delay: 2.0f,
			duration: 2.0f
		).Start ();

		GameObject.Find ("Panel").GetComponent<RectTransform>().anchoredPosition = new Vector2(50, 50);

	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click.");
			OnClick();
		}
	}
}
