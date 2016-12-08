using UnityEngine;
using System.Collections;

public class iTweenTest : MonoBehaviour {
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

		Hashtable ht = new Hashtable();
		ht.Add ("x", 5);
		ht.Add ("time", animationTime);
		ht.Add ("looptype", iTween.LoopType.pingPong);

		for (int i = 0; i < cubeCount; i++) {

			if (i == cubeCount -1) {
				ht.Add ("oncompletetarget", this.gameObject);
				ht.Add ("oncomplete", "OnFinish");

			}

			iTween.MoveTo(objectList[i], ht);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnFinish() {
		Debug.Log ("Finished!!!!");
	}
}
