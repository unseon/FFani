using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class StateMachineController : MonoBehaviour {

	public StateMachineParser parser = new StateMachineParser();
	public FFaniStateMachine stateMachine;
	public TextAsset jsonFile;
	public Component handlerObject;
    public Dictionary<string, FFaniSignal> signals;


	virtual public void Init()
	{

	}

	void Awake () {
		stateMachine = parser.Parse(jsonFile.text);
        signals = stateMachine.signals;
	}
	// Use this for initialization
	void Start () {
		if (handlerObject == null) {
			handlerObject = this;
		}

		if (stateMachine != null && handlerObject != null) {
			stateMachine.onStateEntered = onStateEntered;
			stateMachine.onStateExited = onStateExited;
		}

		if (stateMachine != null) {
			stateMachine.enter();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onStateEntered(string stateName) {
		Debug.Log("onStateEntered " + stateName);

		stateName = char.ToUpper(stateName[0]) + stateName.Substring(1);
		Type thisType = handlerObject.GetType();
		MethodInfo theMethod = thisType.GetMethod("On" + stateName + "Entered");
		if (theMethod != null) {
			theMethod.Invoke(handlerObject, null);
		}
	}

	void onStateExited(string stateName) {
		Type thisType = handlerObject.GetType();

		stateName = char.ToUpper(stateName[0]) + stateName.Substring(1);
		MethodInfo theMethod = thisType.GetMethod("On" + stateName + "Exited");
		if (theMethod != null) {
			theMethod.Invoke(handlerObject, null);
		}
	}
}