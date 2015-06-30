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

	public void Activate() {
		for (int i = 0; i < momentValues.Count; i++) {
			momentValues[i].Activate ();
		}
	}

	public void Add(FFaniPair mv) {
		momentValues.Add (mv);
	}
}

public class FFaniSegue {
	public string from;
	public string to;

	public List<FFaniMation> animList = new List<FFaniMation>();

	public void StartTo(FFaniMoment moment) {
		UpdateTarget (moment);

		for(int i = 0; i < animList.Count; i++) {
			animList[i].Fire ();
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

	List<FFaniSegue> momentLinks = new List<FFaniSegue>();

	private FFaniMoment currentMoment;
	public string moment {
		get {
			return currentMoment.name;
		}
		set {
			string prevMoment = currentMoment.name;
			currentMoment = moments[value];

			FFaniSegue link = FindMomentLink(prevMoment, moment);

			if (link != null) {
				link.StartTo(currentMoment);
			} else {
				currentMoment.Activate();
			}
		}
	}

	public FFaniMomentMap() {
		currentMoment = new FFaniMoment();
		moments[""] = currentMoment;
	}

	public void Add(FFaniMoment moment) {
		moments[moment.name] = moment;
	}

	public void AddLink(FFaniSegue link) {
		momentLinks.Insert(0, link);
	}

	public FFaniSegue FindMomentLink(string from, string to) {
		FFaniSegue link = momentLinks.Find (item => (item.from == from || item.from == "*" ) && (item.to == to || item.to == "*"));

		return link;
	}
}
