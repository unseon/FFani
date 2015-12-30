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

	public bool isConnected() {
		return emit != null;
	}
}

public class FFaniTransition {
	public string name;
	public Action connectSignal;
	public Action disconnectSignal;

	public FFaniState source;
	public FFaniState target;

	public bool isExternal = true;

	FFaniSignal signal;

	public void trigger() {
		// exit from bottom
		FFaniState anc = findCommonAncestor();

		// calculate exiting path
		List <FFaniState> exitingPath = new List<FFaniState>();

		// starting point is not source state
		FFaniState state = source.stateMachine.activeState;
		while (state != anc) {
			exitingPath.Add(state);
			state = state.parent;
		}

		// calculate entering Path
		List <FFaniState> enteringPath = new List<FFaniState>();
		state = target;
		while (state != anc) {
			enteringPath.Add(state);
			state = state.parent;
		}
		enteringPath.Reverse();

		// if isExternal is true, common ancestor should re-enter
		if (isExternal) {
			exitingPath.Add(anc);
			enteringPath.Insert(0, anc);
		}

		// go along the path
		foreach (FFaniState iter in exitingPath) {
			iter.exit();
		}

		foreach (FFaniState iter in enteringPath) {
			iter.enterCurrent();
		}

		// finallly go into child init state;
		target.enterInitState();
	}

	public FFaniTransition() {
	}

	public FFaniTransition(FFaniState from, FFaniState to, FFaniSignal sig) {
		from.addTransition(this);
		target = to;
		setSignal(sig);
	}

	public FFaniState findCommonAncestor() {
		int toStateDepth = target.depth();
		int fromStateDepth = source.depth();

		FFaniState state;

		if (toStateDepth > fromStateDepth) {
			state = target;
			for (int i = 0; i < toStateDepth - fromStateDepth; i++) {
				state = state.parent;
			}

			if (state != source) {
				state = state.parent;

				if (state != source.parent) {
					// ASSERT
				}
			}

		} else {
			state = source;

			for (int i = 0; i < fromStateDepth - toStateDepth; i++) {
				state = state.parent;
			}

			if (state != target) {
				state = state.parent;

				if (state != target.parent) {
					// ASSERT
				}
			}
		}

		return state;
	}

	public void setSignal(FFaniSignal signal) {

		Action prevAction = null;

		connectSignal = () => {
			//Debug.Log("connect signal");


			// stacking emit
			if (signal.isConnected()) {
				prevAction = signal.emit;
				signal.emit = null;
			}

			signal.connect(trigger);
		};

		disconnectSignal = () => {
			signal.disconnect(trigger);

			// pop emit
			if (prevAction != null) {
				signal.emit = prevAction;
			}
		};
	}
}


public class FFaniState {
	public bool isActive;
	public FFaniState parent;

	private FFaniStateMachine mStateMachine;
	public FFaniStateMachine stateMachine {
		get {
			return mStateMachine;
		}
		set {
			mStateMachine = value;

			foreach(FFaniState child in children) {
				child.stateMachine = value;
			}
		}
	}

	public string name;

	public bool isFinal = false;

	public Action onEntered	= () => {};
	public Action onExited = () => {};

	public int depth() {
		int depthCount = 0;
		FFaniState state = this;

		while (state.parent != null) {
			depthCount++;
			state = state.parent;
		}

		return depthCount;
	}

	public void enter() {
		enterCurrent();
		enterInitState();
	}

	public void setInitState(FFaniState state) {
		if (children.IndexOf(state) < 0) {
			children.Remove(state);
			children.Insert(0, state);
		}
	}

	public void enterInitState() {
		FFaniState state = this;
		// propagate enter method to first child
		if (state.children.Count > 0) {
			state = state.children[0];
			state.enter();
		} else {
			stateMachine.activeState = this;
		}
	}

	public void enterCurrent() {
		Debug.Log("state - " + name + " entered");
		connectTransitions();
		isActive = true;
		onEntered();
	}

	public void exit() {
		Debug.Log("state - " + name + " exited");
		disconnectTransitions();
		isActive = false;
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

	public List<FFaniTransition> transitions = new List<FFaniTransition>();
	public List<FFaniState> children = new List<FFaniState>();

	public void addTransition(FFaniTransition transition) {
		transition.source = this;
		transitions.Add(transition);
	}

	public void addChild(FFaniState state) {
		state.parent = this;
		state.stateMachine = stateMachine;
		children.Add(state);
	}
}

public class FFaniStateMachine : FFaniState {
	public FFaniState activeState;

	public FFaniStateMachine() {
		stateMachine = this;
	}
}
