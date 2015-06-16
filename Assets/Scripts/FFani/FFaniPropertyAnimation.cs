using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class FFaniPropertyAnimation : FFaniAnimation {

	public Component targetComponent;
	public string propertyName;
	public object valueFrom = null;
	public object valueTo = null;
	public float duration = 1.0f;
	
	//private FFaniProperty member;
	private FFaniMember member;	
	
	public delegate void Blender(float t);
	public Blender blendValue;
	
	override protected void onStart () {
		if (targetComponent == null) {
			return;
		}
		
		member = FFani.getTargetMember(targetComponent, propertyName);
		
		if (member.getType() == typeof(Vector3)) {
			blendValue = blendVector3;
		} else if (member.getType() == typeof(float)) {
			blendValue = blendNumber;
		} else {
			blendValue = defaultSetter;
		}
	}
	
	override protected void onUpdate(float delta) {
		// t shuoud be in 0.0 ~ 1.0
		float t = Mathf.Clamp(currentTime / duration, 0.0f, 1.0f);
		blendValue(t);
	}
	
	void defaultSetter(float t) {
		if (valueTo != null) {
			member.setValue (valueTo);
		}
	}
	
	void blendNumber(float t) {
		float value = Convert.ToSingle(member.getValue());
	
		float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value;
		float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value;
		
		float newValue =  vTo * t + vFrom * (1.0f - t);
	
		member.setValue(newValue);
	}
	
	void blendVector3(float t) {
		Vector3 value = (Vector3)member.getValue();
		
		Vector3 vTo = valueTo != null ? (Vector3)valueTo : value;
		Vector3 vFrom = valueFrom != null ? (Vector3)valueFrom : value;
		
		Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
		member.setValue(newValue);
	}	
}