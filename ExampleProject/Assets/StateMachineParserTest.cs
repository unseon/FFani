using UnityEngine;
using System.Collections;

public class StateMachineParserTest : MonoBehaviour {

	FFaniStateMachine stateMachine;
	// Use this for initialization
	void Start () {
		stateMachine = GetComponent<StateMachineController>().stateMachine;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onButtonPressed() {
		stateMachine.signals["machineTest"].emit();
	}

	public void onStateEntered() {
		Debug.Log("onStateEntered() called");
	}

	public void onState1Entered() {
		Debug.Log("onState1Entered() called");
	}
}
