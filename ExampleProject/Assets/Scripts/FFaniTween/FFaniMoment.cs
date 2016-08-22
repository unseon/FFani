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
	public List<FFaniPair> propertyChanges = new List<FFaniPair>();

	public FFaniMoment(string name) {
		this.name = name;
	}

	public void Activate() {
		for (int i = 0; i < propertyChanges.Count; i++) {
			propertyChanges[i].Activate ();
		}
	}

	public void Add(FFaniPair mv) {
		propertyChanges.Add (mv);
	}

	public void SetPropertyChange(Component target, string propertyName, object value) {
		FFaniPair pair = new FFaniPair();
		pair.property = FFani.getTargetMember(target, propertyName);
		pair.value = value;

		propertyChanges.Add(pair);
	}
}

public class FFaniMomentMation {
	public string from;
	public string to;

	//public List<FFaniMation> animList = new List<FFaniMation>();

	public FFaniMation blendAnim;

	public void StartTo(FFaniMoment moment) {
		UpdateTarget (moment);

		if (blendAnim != null) {
			blendAnim.Start();
		}
	}

	public void UpdateTarget(FFaniMoment moment) {
		if (blendAnim != null) {
			UpdateValueImmediately(moment);
			UpdateAnimationTargetValue(moment, blendAnim);
		}
	}

	public void UpdateValueImmediately(FFaniMoment moment) {
		for (int i = 0; i < moment.propertyChanges.Count; i++) {
			if(FindPropertyAnimation(moment.propertyChanges[i].property, blendAnim) == null) {
				moment.propertyChanges[i].Activate();
			}
		}
	}

	public FFaniMation FindPropertyAnimation(FFaniProperty property, FFaniMation anim) {
		if (anim.GetType() == typeof(FFaniGroupAnimation)) {
			FFaniGroupAnimation groupAnim = (FFaniGroupAnimation)anim;
			for (int i = 0; i < groupAnim.animList.Count; i++) {
				FFaniMation result = FindPropertyAnimation(property, groupAnim.animList[i]);
				if (result != null) {
					return result;
				}
			}
			return null;
		} else if (anim.GetType() == typeof(FFaniPropertyAnimation)){
			FFaniPropertyAnimation memberAnim = (FFaniPropertyAnimation) anim;
			if (memberAnim.member.isEqual(property)) {
				return memberAnim;
			} else {
				return null;
			}
		} else {
			return null;
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
				FFaniPair memberValue = moment.propertyChanges.Find (item => item.property.isEqual(memberAnim.member));
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
