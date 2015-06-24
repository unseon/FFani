using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniMomentValue {
	public FFaniMember member;
	public object value;
}

public class FFaniMoment {
	public string name = "";
	public List<FFaniMomentValue> momentValues = new List<FFaniMomentValue>();
}

public class FFaniMomentFlow {
	Dictionary<string, FFaniMoment> moments = new Dictionary<string, FFaniMoment>();

	private FFaniMoment currentMoment;
	private string _moment = "";
	public string moment {
		get {
			return currentMoment.name;
		}
		set {
			currentMoment = moments[value];
		}
	}

	public FFaniMomentFlow() {
		currentMoment = new FFaniMoment();
		moments[""] = currentMoment;
	}
}
