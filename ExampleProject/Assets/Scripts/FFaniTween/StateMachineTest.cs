using UnityEngine;
using System.Collections;
using System;

public class StateMachineTest : MonoBehaviour {

	FFaniSignal nextAction = new FFaniSignal();
	public FFaniState state1 = new FFaniState();
	public FFaniState state2 = new FFaniState();
	public FFaniState state3 = new FFaniState();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		state1.name = "state1";
		state2.name = "state2";
		state3.name = "state3";
			

		FFaniTransition trans01 = new FFaniTransition();

		state1.addTransition(trans01);
		trans01.setSignal(nextAction);
		trans01.toState = state2;


		FFaniTransition trans02 = new FFaniTransition();

		state2.addTransition(trans02);
		trans02.setSignal(nextAction);
		trans02.toState = state3;

		state1.enter();

		Debug.Log(nextAction);
	}

	public void onButtonClicked() {
		nextAction.emit();
	}
}
