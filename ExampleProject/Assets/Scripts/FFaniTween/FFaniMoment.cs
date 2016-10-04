using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniPropertyChange {
	public FFaniProperty property;
	public object value;

	virtual public void Activate() {
//		Debug.Log("FFaniPropertyChange Activate " + property.obj);

		property.setValue (value);
	}
}

public class FFaniActivationChange : FFaniPropertyChange {
	private GameObject _target;
	private bool _active;

	public GameObject target {
		get {
			return _target;
		}
		set {
			_target = value;
			property = FFaniProperty.CreateMember(_target, "active");
		}
	}

	public bool active {
		get {
			return _active;
		}
		set {
			_active = value;
			this.value = _active;
		}
	}

	override public void Activate() {
		target.SetActive(active);
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
}

public class FFaniMomentMation {
	public string from;
	public string to;

	public FFaniMation blendAnim;

	public FFani.Callback stopped;

	public void StartTo(FFaniMoment moment) {
		UpdateTarget (moment);

		if (blendAnim != null) {
			blendAnim.Start(OnAnimStopped);
		}
	}

	public void OnAnimStopped() {
		if (stopped != null) {
			stopped();
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
		if (anim is FFaniGroupAnimation) {
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
		if (anim is FFaniGroupAnimation) {
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
	public FFaniMomentMation currentMomentMation;
	public string moment {
		get {
			return currentMoment.name;
		}
		set {
			string prevMoment = currentMoment.name;
			currentMoment = moments[value];

			if (currentMomentMation != null) {
				currentMomentMation.blendAnim.Stop();
			}

			currentMomentMation = FindMomentMation(prevMoment, moment);

			if (currentMomentMation != null) {
				currentMomentMation.StartTo(currentMoment);
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
	public FFaniMomentMation currentMomentMation;

	public string moment {
		get {
			return currentMoment.name;
		}
		set {
			if (currentMoment.name == value) {
				return;
			}
			
			string prevMoment = currentMoment.name;
			currentMoment = moments[value];

			if (currentMomentMation != null) {
				currentMomentMation.blendAnim.Stop();
			}

			currentMomentMation = FindMomentMation(prevMoment, moment);

			if (currentMomentMation != null) {
				currentMomentMation.StartTo(currentMoment);
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
			links[i].stopped += OnMomentMationStopped;
		}
	}

	public void OnMomentMationStopped() {
		//Debug.Log("OnMomentMationStopped");
	}

	public FFaniMomentMation FindMomentMation(string from, string to) {
		FFaniMomentMation link = momentMations.Find (item => (item.from == from || item.from == "*" ) && (item.to == to || item.to == "*"));

		return link;
	}
}

public class FFaniChangeMoment: FFaniMation {
	public MomentBehaviour target;
	public string momentName;

	override public void Init () {
		base.Init();

		target.moment = momentName;
		duration = -1;
	}

	override protected void OnUpdatePlay(float delta) {
		if (target.currentMomentMation == null) {
			state = "completed";
		} else {
			state = target.currentMomentMation.blendAnim.state;
		}
	}
}
