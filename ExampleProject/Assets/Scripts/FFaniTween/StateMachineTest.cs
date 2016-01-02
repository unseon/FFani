using UnityEngine;
using System.Collections;
using System;

public class StateMachineTest : MonoBehaviour {

	FFaniSignal nextAction = new FFaniSignal();

	public FFaniState state1 = new FFaniState();
	public FFaniState state1_1 = new FFaniState();
	public FFaniState state1_2 = new FFaniState();
	public FFaniState state2 = new FFaniState();
	public FFaniState state3 = new FFaniState();
	public FFaniStateMachine stateMachine = new FFaniStateMachine();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		Test3();
	}

	void Test1() {
		stateMachine.name = "stateMachine";
		state1.name = "state1";
		state1_1.name = "state1_1";
		state1_2.name = "state1_2";
		state2.name = "state2";
		state3.name = "state3";

		state1.addChild(state1_1);
		state1.addChild(state1_2);

		stateMachine.addChild(state1);
		stateMachine.addChild(state2);
		stateMachine.addChild(state3);


		FFaniTransition trans01 = new FFaniTransition(state1_1, state1_2, nextAction);
		FFaniTransition trans02 = new FFaniTransition(state2, state3, nextAction);
		FFaniTransition trans03 = new FFaniTransition(state1_2, state2, nextAction);

		stateMachine.enter();

		Debug.Log("depth: " + state1_1.depth());


		FFaniState stateAnc = trans01.findCommonAncestor();

		Debug.Log("common Anc = " + stateAnc.name);

		Debug.Log(nextAction);

	}

	void Test2() {
		stateMachine.name = "stateMachine";
		state1.name = "state1";
		state1_1.name = "state1_1";
		state1_2.name = "state1_2";
		state2.name = "state2";
		state3.name = "state3";

		state1.addChild(state1_1);
		state1.addChild(state1_2);

		stateMachine.addChild(state1);
		stateMachine.addChild(state2);
		stateMachine.addChild(state3);


		//FFaniTransition trans01 = new FFaniTransition(state1_1, state1_2, nextAction);
		FFaniTransition trans03 = new FFaniTransition(state1, state2, nextAction);
		FFaniTransition trans02 = new FFaniTransition(state2, state1_2, nextAction);

		stateMachine.enter();

	}

	void Test3() {
		stateMachine.name = "stateMachine";
		state1.name = "state1";
		state1_1.name = "state1_1";
		state1_2.name = "state1_2";
		state2.name = "state2";
		state3.name = "state3";

		state1.addChild(state1_1);
		state1.addChild(state1_2);

		stateMachine.addChild(state1);
		stateMachine.addChild(state2);
		stateMachine.addChild(state3);


		FFaniTransition trans03 = new FFaniTransition(state1, state2, nextAction);
		FFaniTransition trans01 = new FFaniTransition(state1_1, state1_2, nextAction);
		FFaniTransition trans02 = new FFaniTransition(state2, state1_2, nextAction);

		stateMachine.enter();

	}

	public void onButtonClicked() {
		nextAction.emit();
	}
}
