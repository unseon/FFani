using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniPropertyChange {
	public FFaniProperty property;
	public object value;

	public FFaniPropertyChange(Component target, string propertyName, object value) {
		this.property = FFani.getTargetMember(target, propertyName);
		this.value = value;
	}

	public void Activate() {
		property.setValue (value);
	}
}

public class FFaniMoment {
	public string name = "";
	public List<FFaniPropertyChange> propertyChanges = new List<FFaniPropertyChange>();

	public FFaniMoment(string name) {
		this.name = name;
	}

	public void Activate() {
		for (int i = 0; i < propertyChanges.Count; i++) {
			propertyChanges[i].Activate ();
		}
	}

	public void Add(FFaniPropertyChange mv) {
		propertyChanges.Add (mv);
	}

	public void SetPropertyChange(Component target, string propertyName, object value) {
		FFaniPropertyChange pair = new FFaniPropertyChange(target, propertyName, value);
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
				FFaniPropertyChange memberValue = moment.propertyChanges.Find (item => item.property.isEqual(memberAnim.member));
				memberAnim.to = memberValue.value;
			}
		}
	}
}

public class FFaniMomentMap {
	Dictionary<string, FFaniMoment> moments = new Dictionary<string, FFaniMoment>();

	List<FFaniMomentMation> momentMations = new List<FFaniMomentMation>();

	public FFaniMoment currentMoment;
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


// inner codes are almost same as MomentMap
public class MomentBehaviour : MonoBehaviour {
	public Dictionary<string, FFaniMoment> moments = new Dictionary<string, FFaniMoment>();

	public List<FFaniMomentMation> momentMations = new List<FFaniMomentMation>();

	public FFaniMoment currentMoment;
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

	protected virtual void Awake() {
		currentMoment = new FFaniMoment("");
		moments[""] = currentMoment;
	}

	public void AddMoment(FFaniMoment moment) {
		moments[moment.name] = moment;
	}

	public void Moments(params FFaniMoment[] moments) {
		for (int i = 0; i < moments.Length; i++) {
			this.moments[moments[i].name] = moments[i];
		}
	}

	public void AddMomentMation(FFaniMomentMation link) {
		momentMations.Insert(0, link);
	}

	public void MomentMations(params FFaniMomentMation[] links) {
		for (int i = 0; i < links.Length; i++) {
			momentMations.Insert(0, links[i]);
		}
	}

	public FFaniMomentMation FindMomentMation(string from, string to) {
		FFaniMomentMation link = momentMations.Find (item => (item.from == from || item.from == "*" ) && (item.to == to || item.to == "*"));

		return link;
	}
}
