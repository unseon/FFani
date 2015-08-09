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


		anim = 
			FFani.Step (
				FFani.Tween (
					target: transform,
					propertyName: "lry",
					to: 360.0f,
					duration: 2.0f
				),
				FFani.Serial (
					FFani.Tween(
						target: transform,
						propertyName: "py",
						to: 5.0f
					)
				),
				FFani.Tween (
					target: transform,
					propertyName: "px",
					to: 5.0f,
					duration: 2.0f
				)
			).SetInterval(2.0f)
			.SetSkipTrigger(ref OnClick);
		anim.Start ();
//		testTrigger.OnClick += anim.Next;

		GameObject.Find ("Panel").GetComponent<RectTransform>().anchoredPosition = new Vector2(50, 50);

	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click.");
			OnClick();
		}
	}
}
