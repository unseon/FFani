using UnityEngine;
using System.Collections;

public class BasicTransform : MonoBehaviour {

	public void OnMoveSphere() {
		FFani.Tween(
			target: GameObject.Find("Sphere").GetComponent<Transform>(),
			propertyName: "position.x",
			to: 8.0f,
			duration: 3.0f,
			easingCurve: FFaniEasing.InOutQuad
		).Start();
	}

	public void OnRotateCube() {
		FFani.Tween(
			target: GameObject.Find("Cube").GetComponent<Transform>(),
			propertyName: "localRotation.eulerAngles.y",
			to: 360.0f,
			easingCurve: FFaniEasing.InOutQuad
		).Start();
	}

	public void OnMoveButton() {
		FFani.Tween(
			target: GameObject.Find("Button").GetComponent<RectTransform>(),
			propertyName: "anchoredPosition.x",
			to: 250.0f,
			easingCurve: FFaniEasing.InOutQuad
		).Start();
	}
}
