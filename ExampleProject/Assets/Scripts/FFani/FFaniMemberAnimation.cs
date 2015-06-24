using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class FFaniMemberAnimation : FFaniAnimation {

	public Component targetComponent;
	public string propertyName;
	public object valueFrom = null;
	public object valueTo = null;

	//private FFaniProperty member;
	private FFaniMember member;	
	
	public delegate void Blender(float t);
	public Blender blendValue;
	
	override protected void onStart () {
		base.onStart();

		if (targetComponent == null) {
			return;
		}
		
		member = FFani.getTargetMember(targetComponent, propertyName);
		object value = member.getValue();
		
		if (valueTo == null) {
			valueTo = value;
		}
		
		if (valueFrom == null) {
			valueFrom = value;
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
		// t shuoud be in 0.0 ~ 1.0
		float t = Mathf.Clamp(currentTime / duration, 0.0f, 1.0f);
		
		blendValue(t);
		
		if (currentTime >= duration) {
			onFinish();
		}
	}
	
	void defaultSetter(float t) {
		if (valueTo != null) {
			member.setValue (valueTo);
		}
	}
	
	void blendNumber(float t) {
		float vTo = Convert.ToSingle (valueTo);
		float vFrom = Convert.ToSingle (valueFrom);
		
		float newValue = vTo * t + vFrom * (1.0f - t);
		member.setValue(newValue);
	}
	
	void blendVector3(float t) {
		Vector3 vTo = (Vector3)valueTo;
		Vector3 vFrom = (Vector3)valueFrom;
		
		Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
		member.setValue(newValue);
	}
	
	void blendQuaternion(float t) {
		Quaternion vTo = (Quaternion)valueTo;
		Quaternion vFrom = (Quaternion)valueFrom;
		
		Quaternion newValue = Quaternion.Slerp(vFrom, vTo, t);
		member.setValue(newValue);
	}
}