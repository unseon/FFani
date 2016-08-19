using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniPair {
	public FFaniProperty property;
	public object value;

	public void Activate() {
		property.setValue (value);
	}
}

public class FFaniMoment {
	public string name = "";
	public List<FFaniPair> momentValues = new List<FFaniPair>();

	public FFaniMoment(string name) {
		this.name = name;
	}

	public void Activate() {
		for (int i = 0; i < momentValues.Count; i++) {
			momentValues[i].Activate ();
		}
	}

	public void Add(FFaniPair mv) {
		momentValues.Add (mv);
	}

	public void PropertyChange(Component target, string propertyName, object value) {
		FFaniPair pair = new FFaniPair();
		pair.property = FFani.getTargetMember(target, propertyName);
		pair.value = value;

		momentValues.Add(pair);
	}
}

public class FFaniMomentMation {
	public string from;
	public string to;

	public List<FFaniMation> animList = new List<FFaniMation>();

	public void StartTo(FFaniMoment moment) {
		UpdateTarget (moment);

		for(int i = 0; i < animList.Count; i++) {
			animList[i].Start ();
		}
	}

	public void UpdateTarget(FFaniMoment moment) {
		for (int i = 0; i < animList.Count; i++) {
			UpdateAnimationTargetValue(moment, animList[i]);
		}
	}
	
	public void UpdateAnimationTargetValue(FFaniMoment moment, FFaniMation anim) {
		if (anim.GetType() == typeof(FFaniGroupAnimation)) {
			FFaniGroupAnimation groupAnim = (FFaniGroupAnimation)anim;
			for (int i = 0; i < groupAnim.animList.Count; i++) {
				UpdateAnimationTargetValue(moment, groupAnim.animList[i]);
			}
			
			return;
		} else if (anim.GetType() == typeof(FFaniPropertyAnimation)){
			FFaniPropertyAnimation memberAnim = (FFaniPropertyAnimation) anim;
			
			if (memberAnim.to == null) {
				FFaniPair memberValue = moment.momentValues.Find (item => item.property.isEqual(memberAnim.member));
				memberAnim.to = memberValue.value;
			}
		}
	}
}

public class FFaniMomentMap {
	Dictionary<string, FFaniMoment> moments = new Dictionary<string, FFaniMoment>();

	List<FFaniMomentMation> momentMations = new List<FFaniMomentMation>();

	private FFaniMoment currentMoment;
	public string moment {
		get {
			return currentMoment.name;
		}
		set {
			string prevMoment = currentMoment.name;
			currentMoment = moments[value];

			FFaniMomentMation link = FindMomentMation(prevMoment, moment);

			if (link != null) {
				link.StartTo(currentMoment);
			} else {
				currentMoment.Activate();
			}
		}
	}

	public FFaniMomentMap() {
		currentMoment = new FFaniMoment("");
		moments[""] = currentMoment;
	}

	public void AddMoment(FFaniMoment moment) {
		moments[moment.name] = moment;
	}

	public void AddMomentMation(FFaniMomentMation link) {
		momentMations.Insert(0, link);
	}

	public FFaniMomentMation FindMomentMation(string from, string to) {
		FFaniMomentMation link = momentMations.Find (item => (item.from == from || item.from == "*" ) && (item.to == to || item.to == "*"));

		return link;
	}
}
