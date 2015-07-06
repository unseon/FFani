using UnityEngine;
using System.Collections;

public class FFaniMationTest : MonoBehaviour {
	public int cubeCount = 10000;
	public float animationTime = 5.0f;

	// Use this for initialization
	private GameObject[] objectList;

	void Start () {


		objectList = new GameObject[cubeCount];

		for (int i = 0; i < cubeCount; i++) {
			objectList[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			objectList[i].transform.position = new Vector3(0, 5, i * 2);
		}

		for (int i = 0; i < cubeCount; i++) {

			if (i == cubeCount -1) {
				FFani.Tween (
					target: objectList[i].GetComponent<Transform>(),
					propertyName: "px",
					to: 50,
					duration: animationTime
				).Remind (
					()=>{OnFinish ();}
				).Start ();
			}

			FFani.Tween (
				target: objectList[i].GetComponent<Transform>(),
				propertyName: "px",
				to: 50,
				duration: animationTime
			).Start ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnFinish() {
		Debug.Log ("Finished!!!!");
	}
}
