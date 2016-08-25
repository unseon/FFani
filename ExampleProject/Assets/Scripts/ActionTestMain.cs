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

		FFani.MecanimState(
			target: GameObject.Find("unitychan").GetComponent<Animator>(),
			stateName: "JUMP01"
		).Remind(
			()=>{Debug.Log("Hey Hey");}
		).Start();
	
//		Animator animator = GameObject.Find("unitychan").GetComponent<Animator>();
//		animator.Play("JUMP00");
	}

	public void OnWait() {
		FFani.MecanimState(
			target: GameObject.Find("unitychan").GetComponent<Animator>(),
			stateName: "WAIT01"
		).Remind(
			()=>{Debug.Log("Hey Hey");}
		).Start();
	}
}
