using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class FFaniPropertyAnimation : FFaniMation {

	public object targetComponent;
	public string propertyName;
	public object from = null;
	public object to = null;

	private object _from = null;
	private object _to = null;

	//private FFaniProperty member;
	private FFaniProperty _member;
	public FFaniProperty member {
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
	
	override public void Init () {
		base.Init();

		if (targetComponent == null) {
			return;
		}
		
		member = FFani.getTargetMember(targetComponent, propertyName);
		object value = member.getValue();
		
		if (to == null) {
			_to = value;
		} else {
			_to = to;
		}
		
		if (from == null) {
			_from = value;
		} else {
			_from = from;
		}

		if (member.getType() == typeof(float)) {
			blendValue = blendNumber;
		} else if (member.getType() == typeof(Vector3)) {
			blendValue = blendVector3;
		} else if (member.getType() == typeof(Vector2)) {
			blendValue = blendVector2;
		} else if (member.getType() == typeof(Color)) {
			blendValue = blendColor;
		} else if (member.getType() == typeof(Quaternion)) {
			blendValue = blendQuaternion;
		} else {
			blendValue = defaultSetter;
		}
	}
	
	override protected void OnUpdatePlay(float delta) {
		//Debug.Log (currentTime);

		// t shuoud be in 0.0 ~ 1.0
		float t;
        if (duration == 0) {
            t = 1.0f;
        } else {
            t = Mathf.Clamp(currentTime / duration, 0.0f, 1.0f);
        }

		float easingTime = easingCurve(t);


		if (blendValue == null) {
			Debug.Log ("blendValue is null");
		}

		blendValue(easingTime);

//		Debug.Log("Current Time: " + currentTime + ", BlendValue: " + member.getValue());
		
//		if (currentTime >= duration) {
//			Finish();
//		}
	}
	
	void defaultSetter(float t) {
		if (_to != null) {
			member.setValue (_to);
		}
	}
	
	void blendNumber(float t) {
		float vTo = Convert.ToSingle (_to);
		float vFrom = Convert.ToSingle (_from);
		
		float newValue = vTo * t + vFrom * (1.0f - t);
		member.setValue(newValue);
	}

	void blendVector2(float t) {
		Vector2 vTo = (Vector2)_to;
		Vector2 vFrom = (Vector2)_from;
		
		Vector2 newValue = Vector2.Lerp (vFrom, vTo, t);
		member.setValue(newValue);
	}
	
	void blendVector3(float t) {
		Vector3 vTo = (Vector3)_to;
		Vector3 vFrom = (Vector3)_from;
		
		Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
		member.setValue(newValue);
	}
	
	void blendQuaternion(float t) {
		Quaternion vTo = (Quaternion)_to;
		Quaternion vFrom = (Quaternion)_from;
		
		Quaternion newValue = Quaternion.Slerp(vFrom, vTo, t);
		member.setValue(newValue);
	}

	void blendColor(float t) {
		Color vTo = (Color)_to;
		Color vFrom = (Color)_from;

		Color newValue = Color32.Lerp(vFrom, vTo, t);
		member.setValue (newValue);
	}

	override public FFaniMation Cloned() {
		FFaniPropertyAnimation anim = new FFaniPropertyAnimation();
		CopyTo(anim);

		return anim;
	}

	override protected void CopyTo(FFaniMation target) {
		FFaniPropertyAnimation anim = target as FFaniPropertyAnimation;
		base.CopyTo(anim);

		anim.targetComponent = targetComponent;
		anim.propertyName = propertyName;
		anim.from = from;
		anim.to = to;
	}
}