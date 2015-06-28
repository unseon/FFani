using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class FFaniMemberAnimation : FFaniAnimation {

	public Component targetComponent;
	public string propertyName;
	public object from = null;
	public object to = null;

	//private FFaniProperty member;
	private FFaniMember _member;
	public FFaniMember member {
		get {
			if (_member == null) {
				_member = FFani.getTargetMember(targetComponent, propertyName);
			}

			return _member;
		}
		set {
			_member = value;
		}
	}
	
	public delegate void Blender(float t);
	public Blender blendValue;
	
	override protected void onStart () {
		base.onStart();

		if (targetComponent == null) {
			return;
		}
		
		member = FFani.getTargetMember(targetComponent, propertyName);
		object value = member.getValue();
		
		if (to == null) {
			to = value;
		}
		
		if (from == null) {
			from = value;
		}
		
		
		if (member.getType() == typeof(Vector3)) {
			blendValue = blendVector3;
		} else if (member.getType() == typeof(Quaternion)) {
			blendValue = blendQuaternion;
		} else if (member.getType() == typeof(float)) {
			blendValue = blendNumber;
		} else {
			blendValue = defaultSetter;
		}
	}
	
	override protected void onUpdate(float delta) {
		//Debug.Log (currentTime);

		// t shuoud be in 0.0 ~ 1.0
		float t = Mathf.Clamp(currentTime / duration, 0.0f, 1.0f);
		float easingTime = easingFunction(t);

		blendValue(easingTime);
		
		if (currentTime >= duration) {
			onFinish();
		}
	}
	
	void defaultSetter(float t) {
		if (to != null) {
			member.setValue (to);
		}
	}
	
	void blendNumber(float t) {
		float vTo = Convert.ToSingle (to);
		float vFrom = Convert.ToSingle (from);
		
		float newValue = vTo * t + vFrom * (1.0f - t);
		member.setValue(newValue);
	}
	
	void blendVector3(float t) {
		Vector3 vTo = (Vector3)to;
		Vector3 vFrom = (Vector3)from;
		
		Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
		member.setValue(newValue);
	}
	
	void blendQuaternion(float t) {
		Quaternion vTo = (Quaternion)to;
		Quaternion vFrom = (Quaternion)from;
		
		Quaternion newValue = Quaternion.Slerp(vFrom, vTo, t);
		member.setValue(newValue);
	}
}