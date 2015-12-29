using UnityEngine;
using System.Collections;
using System;

public class StateMachineTest : MonoBehaviour {

	FFaniSignal nextAction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		state1.name = "state1";
		state2.name = "state2";
			

		FFaniTransition trans01 = new FFaniTransition();

		state1.addTransition(trans01);
		trans01.setAction(next);
		trans01.toState = state2;

		state1.enter();

		Debug.Log(next);
	}

	public Action next;

	public FFaniState state1 = new FFaniState();
	public FFaniState state2 = new FFaniState();

	public void onButtonClicked() {
		next();
	}
}
