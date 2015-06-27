using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniMemberValue {
	public FFaniMember member;
	public object value;

	public void activate() {
		member.setValue (value);
	}
}

public class FFaniMoment {
	public string name = "";
	public List<FFaniMemberValue> memberValues = new List<FFaniMemberValue>();

	public void activate() {
		for (int i = 0; i < memberValues.Count; i++) {
			memberValues[i].activate ();
		}
	}

	public void add(FFaniMemberValue mv) {
		memberValues.Add (mv);
	}
}

public class FFaniMomentLink {
	public string from;
	public string to;

	public List<FFaniAnimation> animList = new List<FFaniAnimation>();

	public void startTo(FFaniMoment moment) {
		updateTarget (moment);

		for(int i = 0; i < animList.Count; i++) {
			animList[i].start ();
		}
	}

	public void updateTarget(FFaniMoment moment) {
		for (int i = 0; i < animList.Count; i++) {
			updateAnimationTargetValue(moment, animList[i]);
		}
	}
	
	public void updateAnimationTargetValue(FFaniMoment moment, FFaniAnimation anim) {
		if (anim.GetType() == typeof(FFaniGroupAnimation)) {
			FFaniGroupAnimation groupAnim = (FFaniGroupAnimation)anim;
			for (int i = 0; i < groupAnim.animList.Count; i++) {
				updateAnimationTargetValue(moment, groupAnim.animList[i]);
			}
			
			return;
		} else if (anim.GetType() == typeof(FFaniMemberAnimation)){
			FFaniMemberAnimation memberAnim = (FFaniMemberAnimation) anim;
			
			if (memberAnim.to == null) {
				FFaniMemberValue memberValue = moment.memberValues.Find (item => item.member.isEqual(memberAnim.member));
				memberAnim.to = memberValue.value;
			}
		}
	}
}

public class FFaniMomentMap {
	Dictionary<string, FFaniMoment> moments = new Dictionary<string, FFaniMoment>();

	List<FFaniMomentLink> momentLinks = new List<FFaniMomentLink>();

	private FFaniMoment currentMoment;
	public string moment {
		get {
			return currentMoment.name;
		}
		set {
			string prevMoment = currentMoment.name;
			currentMoment = moments[value];

			FFaniMomentLink link = findMomentLink(prevMoment, moment);

			if (link != null) {
				link.startTo(currentMoment);
			} else {
				currentMoment.activate();
			}
		}
	}

	public FFaniMomentMap() {
		currentMoment = new FFaniMoment();
		moments[""] = currentMoment;
	}

	public void add(FFaniMoment moment) {
		moments[moment.name] = moment;
	}

	public void addLink(FFaniMomentLink link) {
		momentLinks.Insert(0, link);
	}

	public FFaniMomentLink findMomentLink(string from, string to) {
		FFaniMomentLink link = momentLinks.Find (item => (item.from == from || item.from == "*" ) && (item.to == to || item.to == "*"));

		return link;
	}
}
