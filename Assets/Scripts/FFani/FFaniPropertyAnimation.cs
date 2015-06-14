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
	
	private string[] propertyNames;
	private PropertyInfo pInfo;
	private PropertyInfo ppInfo;
	
	private MemberInfo mInfo;

	private FFaniMember member;	
	
	delegate void ValueSetter(object value);
	
	void Vector3XSetter(object value) {
	}
	
	void Vector3YSetter(object value) {
	}
	
	void Vector3ZSetter(object value) {
	}
	
	void Vector3Setter(object value) {
	}
	
	override protected void onStart () {
		if (targetComponent != null) {
			// initialize basic members about the target property			
			propertyNames = propertyName.Split('.');
			
			// get list property name's info
			pInfo = targetComponent.GetType ().GetProperty(propertyNames[0]);

			member = getTargetMember(targetComponent, propertyName);
			
//			if (mInfo.ReflectedType == typeof(Vector3) ||
//			    mInfo.ReflectedType == typeof(Quaternion) ||
//			    mInfo.ReflectedType == typeof(Color))
//		    {
//		    	
//			} 			
			
//			if (propertyNames.Length >= 2) {
//				ppInfo = targetComponent.GetType ().GetProperty(propertyNames[propertyNames.Length - 1]);				
//			}
			
			//Debug.Log (targetComponent.GetType ());
			//Debug.Log (pInfo.GetValue (targetComponent, null));
		}
	}
	
	public static FFaniMember getTargetMember(object target, string memberName) {
		return getTargetMember (target, new List<string>(memberName.Split('.')));
	}
	
	static FFaniMember getTargetMember(object target, List<string> names, object reflectedObject = null) {
		MemberInfo mi = target.GetType ().GetMember(names[0])[0];
		
		if (names.Count == 1) {
			return new FFaniMember(mi, target, null);
		}
		
		names.RemoveAt(0);
		
		if (mi.MemberType == MemberTypes.Property) {
			PropertyInfo pi = (PropertyInfo)mi;
			object t = pi.GetValue(target, null);
			return getTargetMember (t, names, target);
		} else if (mi.MemberType == MemberTypes.Field) {
			FieldInfo fi = (FieldInfo)mi;
			object t = fi.GetValue(target);
			return getTargetMember (t, names, target);
		} else {
			return null;
		}
	}
	
	
	override protected void onUpdate(float delta) {
		// t shuoud be in 0.0 ~ 1.0
		float t = Mathf.Clamp(currentTime / duration, 0.0f, 1.0f);
		
//		if (pInfo.PropertyType == typeof(Vector3)) {
//			//Vector3 type
//			Vector3 value = (Vector3)pInfo.GetValue (targetComponent, null);
//			if (propertyNames.Length == 2) {
//				// float member in Vector3 (ie. position.x)
//				string member = propertyNames[1];
//				Vector3 newValue = new Vector3();
//				
//				// change the member value but keep others
//				if (member == "x") {
//					float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.x;
//					float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.x;
//					
//					float newX =  vTo * t + vFrom * (1.0f - t);
//					newValue = new Vector3(newX, value.y, value.z);
//				} else if (member == "y") {
//					float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.y;
//					float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.y;
//					
//					float newY =  vTo * t + vFrom * (1.0f - t);
//					newValue = new Vector3(value.x, newY, value.z);
//				} else if (member == "z") {
//					float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.z;
//					float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.z;
//					
//					float newZ =  vTo * t + vFrom * (1.0f - t);
//					newValue = new Vector3(value.x, value.y, newZ);
//				}
//				
//				pInfo.SetValue(targetComponent, newValue, null);		
//
//			} else {
//				// Vector3 type
//				Vector3 vTo = valueTo != null ? (Vector3)valueTo : value;
//				Vector3 vFrom = valueFrom != null ? (Vector3)valueFrom : value;
//				Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
//				pInfo.SetValue(targetComponent, newValue, null);			
//			}
//		}
		if (member.getType() == typeof(Vector3)) {
			Vector3 value = (Vector3)member.getValue();
			
			Vector3 vTo = valueTo != null ? (Vector3)valueTo : value;
			Vector3 vFrom = valueFrom != null ? (Vector3)valueFrom : value;
			
			Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
			member.setValue(newValue);
		} else if (member.reflectedObject.GetType () == typeof(Vector3)) {
			// change the member value but keep others
			Vector3 newValue = new Vector3();
			Vector3 value = (Vector3)member.reflectedObject;
			
			if (member.memberInfo.Name == "x") {
				float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.x;
				float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.x;
				
				float newX =  vTo * t + vFrom * (1.0f - t);
				newValue = new Vector3(newX, value.y, value.z);
			} else if (member.memberInfo.Name == "y") {
				float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.y;
				float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.y;
				
				float newY =  vTo * t + vFrom * (1.0f - t);
				newValue = new Vector3(value.x, newY, value.z);
			} else if (member.memberInfo.Name == "z") {
				float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.z;
				float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.z;
				
				float newZ =  vTo * t + vFrom * (1.0f - t);
				newValue = new Vector3(value.x, value.y, newZ);
			}
			
			
			value.Set(newValue.x, newValue.y, newValue.z);			
		}
	}
}