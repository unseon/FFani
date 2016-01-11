using UnityEngine;
using System.Collections;

public class StateMachineController : MonoBehaviour {

	public StateMachineParser parser = new StateMachineParser();
	public FFaniStateMachine stateMachine;
	public TextAsset jsonFile;

	void Awake () {
		stateMachine = parser.Parse(jsonFile.text);
	}
	// Use this for initialization
	void Start () {
		if (stateMachine != null) {
			stateMachine.enter();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
