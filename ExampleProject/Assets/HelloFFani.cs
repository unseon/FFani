using UnityEngine;
using System.Collections;

public class HelloFFani : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FFani.Mation (
			target: gameObject.transform,
			propertyName: "position.x",
			to: 5.0f,
			duration: 2.0f
		).Start ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
