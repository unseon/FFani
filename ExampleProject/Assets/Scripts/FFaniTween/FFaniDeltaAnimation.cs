using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class FFaniDeltaAnimation : FFaniPropertyAnimation {
	public delegate void DeltaBlender(float t, float dt);
	public DeltaBlender blendDeltaValue;

	override public void Init () {
		state = "playing";

		if (targetComponent == null) {
			return;
		}
		
		member = FFani.getTargetMember(targetComponent, propertyName);
		object value = member.getValue();
		
		if (member.getType() == typeof(Vector3)) {
			blendDeltaValue = blendDeltaVector3;

			if (to == null) {
				to = new Vector3();
			} else {
				to = (Vector3)to + (Vector3)value;
			}
			
			if (from == null) {
				from = new Vector3();
			} else {
				from = (Vector3)from + (Vector3)value;
			}

			member.setValue((Vector3)member.getValue() + (Vector3)from);

		} else if (member.getType() == typeof(Color)) {
			blendDeltaValue = blendDeltaColor;

			if (to == null) {
				to = new Color();
			} else {
				to = (Color)to + (Color)value;
			}
			
			if (from == null) {
				from = new Color();
			} else {
				from = (Color)from + (Color)value;
			}
			
			member.setValue((Color)member.getValue() + (Color)from);

		} else if (member.getType() == typeof(Quaternion)) {
			blendDeltaValue = blendDeltaQuaternion;

			if (to == null) {
				to = new Quaternion();
			} else {
				to = (Quaternion)to * (Quaternion)value;
			}
			
			if (from == null) {
				from = new Quaternion();
			} else {
				from = (Quaternion)from * (Quaternion)value;
			}
			
			member.setValue((Quaternion)member.getValue() * (Quaternion)from);

		} else if (member.getType() == typeof(float)) {
			blendDeltaValue = blendDeltaNumber;

			if (to == null) {
				to = 0.0f;
			}
			
			if (from == null) {
				from = 0.0f;
			}

			member.setValue(Convert.ToSingle(member.getValue()) + Convert.ToSingle(from));
		} else {
		}
	}
	
	override protected void OnUpdatePlay(float delta) {
		//Debug.Log (currentTime);

		// t shuoud be in 0.0 ~ 1.0
		float t = Mathf.Clamp(currentTime / duration, 0.0f, 1.0f);
		float easingTime = easingCurve(t);

		float prevT = Mathf.Clamp((currentTime - delta) / duration, 0.0f, 1.0f);
		float prevEasingTime = easingCurve(prevT);

		float easingDelta = easingTime - prevEasingTime;

		blendDeltaValue(easingTime, easingDelta);
		
//		if (currentTime >= duration) {
//			Finish();
//		}
	}

	void blendDeltaNumber(float t, float dt) {
		float vTo = Convert.ToSingle (to);
		float vFrom = Convert.ToSingle (from);
		float newValue = vTo * t + vFrom * (1.0f - t);

		float prevT = t - dt; 
		float prevValue = vTo * prevT + vFrom * (1.0f - prevT);

		float delta = newValue - prevValue;

		Debug.Log (newValue + "," + prevValue + "," + delta + " : " + member.getValue ());

		newValue = Convert.ToSingle(member.getValue()) + delta;
		member.setValue(newValue);
	}
	
	void blendDeltaVector3(float t, float dt) {
		Vector3 vTo = (Vector3)to;
		Vector3 vFrom = (Vector3)from;
		
		Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
		member.setValue(newValue);
	}
	
	void blendDeltaQuaternion(float t, float dt) {
		Quaternion vTo = (Quaternion)to;
		Quaternion vFrom = (Quaternion)from;
		
		Quaternion newValue = Quaternion.Slerp(vFrom, vTo, t);
		member.setValue(newValue);
	}

	void blendDeltaColor(float t, float dt) {
		Color vTo = (Color)to;
		Color vFrom = (Color)from;

		Color newValue = Color32.Lerp(vFrom, vTo, t);
		member.setValue (newValue);
	}
}