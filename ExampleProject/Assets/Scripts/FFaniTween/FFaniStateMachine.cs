using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FFaniSignal {
	public Action emit;

	public void connect(Action action) {
		emit += action;
	}

	public void disconnect(Action action) {
		emit -= action;
	}
}

public class FFaniTransition {
	string name;

	public void trigger() {
		fromState.exit();
		toState.enter();
	}

	public Action connectSignal;
	public Action disconnectSignal;

	public FFaniState fromState;
	public FFaniState toState;

	FFaniSignal signal;

	public void setSignal(FFaniSignal signal) {
		connectSignal = () => {
			Debug.Log("connect signal");
			signal.connect(trigger);
		};
		disconnectSignal = () => {
			signal.disconnect(trigger);
		};
	}
}


public class FFaniState {
	public string name;

	public Action onEntered	= () => {};
	public Action onExited = () => {};


	public  void enter() {
		Debug.Log("state - " + name + " entered");
		connectTransitions();
		onEntered();
	}

	public void exit() {
		Debug.Log("state - " + name + " exited");
		disconnectTransitions();
		onExited();
	}

	private void connectTransitions() {
		for (int i = 0; i < transitions.Count; i++) {
			transitions[i].connectSignal();
		}
	}

	private void disconnectTransitions() {
		for (int i = 0; i < transitions.Count; i++) {
			transitions[i].disconnectSignal();
		}
	}

	List<FFaniTransition> transitions = new List<FFaniTransition>();

	public void addTransition(FFaniTransition transition) {
		transition.fromState = this;
		transitions.Add(transition);
	}
		
}

public class FFaniStateMachine : FFaniState{
}
