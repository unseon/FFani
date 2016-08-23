using UnityEngine;
using System.Collections;

public class ActionTestMain : MonoBehaviour {

	MecanimAction maction;

	void Awake () {
		maction = GameObject.Find("unitychan").GetComponent<MecanimAction>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnJump() {
	}
}
