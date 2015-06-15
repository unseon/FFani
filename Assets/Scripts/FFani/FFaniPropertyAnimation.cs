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

	//private FFaniProperty member;
	private FFaniMember member;	
	
	override protected void onStart () {
		if (targetComponent != null) {
			// initialize basic members about the target property			
			propertyNames = propertyName.Split('.');
			
			// get list property name's info
			pInfo = targetComponent.GetType ().GetProperty(propertyNames[0]);

			member = FFani.getTargetMember(targetComponent, propertyName);
			
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
	
	override protected void onUpdate(float delta) {
		// t shuoud be in 0.0 ~ 1.0
		float t = Mathf.Clamp(currentTime / duration, 0.0f, 1.0f);

		Type tt = member.getType();
		Type ttt = typeof(float);

		if (member.getType() == typeof(Vector3)) {
			Vector3 value = (Vector3)member.getValue();
			
			Vector3 vTo = valueTo != null ? (Vector3)valueTo : value;
			Vector3 vFrom = valueFrom != null ? (Vector3)valueFrom : value;
			
			Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
			member.setValue(newValue);
		} else if (member.getType() == typeof(float)) {
			float value = Convert.ToSingle(member.getValue());
		
			float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value;
			float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value;
			
			float newValue =  vTo * t + vFrom * (1.0f - t);
		
			member.setValue(newValue);
		}
	}
}