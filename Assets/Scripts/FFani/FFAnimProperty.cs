using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

namespace FFanim {

public class Property : Base {
	
	public GameObject target;
	public string componentName;
	public string propertyName;
	public object valueFrom = null;
	public object valueTo = null;
	public float duration = 1.0f;
	
	private bool started;
	
	private Component targetComponent;
	private string[] propertyNames;
	private PropertyInfo pInfo;
	
	override protected void onStart () {
		if (target != null) {
			// initialize basic members about the target property			
			targetComponent = target.GetComponent(componentName);
			propertyNames = propertyName.Split('.');
			pInfo = targetComponent.GetType ().GetProperty(propertyNames[0]);
			
			//Debug.Log (targetComponent.GetType ());
			//Debug.Log (pInfo.GetValue (targetComponent, null));
		}
	}
	
	override protected void onUpdate(float delta) {
		// t shuoud be in 0.0 ~ 1.0
		float t = Mathf.Clamp(currentTime / duration, 0.0f, 1.0f);
		
		if (pInfo.PropertyType == typeof(Vector3)) {
			//Vector3 type
			Vector3 value = (Vector3)pInfo.GetValue (targetComponent, null);
			if (propertyNames.Length == 2) {
				// float member in Vector3 (ie. position.x)
				string member = propertyNames[1];
				Vector3 newValue = new Vector3();
				
				// change the member value but keep others
				if (member == "x") {
					float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.x;
					float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.x;
					
					float newX =  vTo * t + vFrom * (1.0f - t);
					newValue = new Vector3(newX, value.y, value.z);
				} else if (member == "y") {
					float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.y;
					float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.y;
					
					float newY =  vTo * t + vFrom * (1.0f - t);
					newValue = new Vector3(value.x, newY, value.z);
				} else if (member == "z") {
					float vTo = valueTo != null ? Convert.ToSingle (valueTo) : value.z;
					float vFrom = valueFrom != null ? Convert.ToSingle (valueFrom) : value.z;
					
					float newZ =  vTo * t + vFrom * (1.0f - t);
					newValue = new Vector3(value.x, value.y, newZ);
				}
				
				pInfo.SetValue(targetComponent, newValue, null);			
			} else {
				// Vector3 type
				Vector3 vTo = valueTo != null ? (Vector3)valueTo : value;
				Vector3 vFrom = valueFrom != null ? (Vector3)valueFrom : value;
				Vector3 newValue = Vector3.Lerp (vFrom, vTo, t);
				pInfo.SetValue(targetComponent, newValue, null);			
			}
		}
	}
}

}