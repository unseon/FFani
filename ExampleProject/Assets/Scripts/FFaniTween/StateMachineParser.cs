using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class StateMachineParser {

	public FFaniStateMachine stateMachine;
	public TextAsset jsonFile;

	private Dictionary<JSONNode, FFaniSignalTransition> transitionDic = new Dictionary<JSONNode, FFaniSignalTransition>();
	private Dictionary<string, FFaniState> stateDic = new Dictionary<string, FFaniState>();

	public FFaniStateMachine Parse (string text) {
		var root = JSON.Parse(text);
		if (root["type"].Value == "StateMachine") {
			stateMachine = (FFaniStateMachine) ParseState(root);
			PostParseState(root);

			return stateMachine;
		}

		return null;
	}

	FFaniState ParseState (JSONNode node) {
		FFaniState state = null;

		if (node["type"].Value == "StateMachine") {
			state = new FFaniStateMachine();
		} else if (node["type"].Value == "State") {
			state = new FFaniState();
		}

		stateDic.Add(node["id"].Value, state);
		state.name = node["objectName"].Value;

		if (node["children"] != null) {
			var children = node["children"].AsArray;

			for (int i = 0; i < children.Count; i++) {
				var child = children[i].AsObject;
				FFaniState childState = ParseState(child);

				state.addChild(childState);
			}
		}

		if (node["signals"] != null) {
			var signals = node["signals"].AsArray;
			for (int i = 0; i < signals.Count; i++) {
				var signalName = signals[i].Value;
				state.addSignal(signalName);
			}
		}

		if (node["transitions"] != null) {
			var transitions = node["transitions"].AsArray;
			for (int i = 0; i < transitions.Count; i++) {
				var transitionNode = transitions[i];
				FFaniSignalTransition transition = new FFaniSignalTransition();//ParseTransition(transitionNode);

				state.addTransition(transition);

				transitionDic.Add(transitionNode, transition);
			}
		}

		return state;
	}

	FFaniSignalTransition ParseTransition (JSONNode node) {
		FFaniSignalTransition transition = new FFaniSignalTransition();


		return transition;
	}

	void PostParseState (JSONNode node) {
		if (node["children"] != null) {
			var children = node["children"].AsArray;

			for (int i = 0; i < children.Count; i++) {
				var child = children[i].AsObject;
				PostParseState(child);
			}
		}

		if (node["transitions"] != null) {
			var transitions = node["transitions"].AsArray;
			for (int i = 0; i < transitions.Count; i++) {
				var transitionNode = transitions[i];
				PostParseTransition(transitionNode);
			}
		}
	}

	void PostParseTransition (JSONNode node) {
		var transition = transitionDic[node];

		var signal = stateMachine.signals[node["signalName"].Value];
		transition.setSignal(signal);
		transition.target = stateDic[node["targetState"].Value];
	}
}
