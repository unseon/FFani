using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class PropertyAnimation : MonoBehaviour {

	public GameObject target;
	public string componentName;
	public string propertyName;
	public string valueFrom;
	public string valueTo;
	public float duration = 1.0f;
	
	private bool started;

	// Use this for initialization
	void Start () {
		if (target != null) {
			Type type = target.GetType();
			Component component = target.GetComponent(componentName);
			
			Debug.Log (component.GetType ());
		
			//FieldInfo fieldInfo = component.GetType ().GetField (propertyName);
			//Debug.Log (fieldInfo);
			
			string[] propertyNames = propertyName.Split('.');
			
			PropertyInfo pInfo = component.GetType ().GetProperty(propertyNames[0]);
			
			Debug.Log (pInfo.GetValue (component, null));
			
			if (pInfo.PropertyType == typeof(Vector3)) {
				//Debug.Log (fieldInfo.GetValue(component));
				Vector3 value = (Vector3)pInfo.GetValue (component, null);
				if (propertyNames.Length == 2) {
					string member = propertyNames[1];
					Vector3 newValue = new Vector3();
					if (member == "x") {
						newValue = new Vector3(float.Parse(valueTo), value.y, value.z);
					} else if (member == "y") {
						newValue = new Vector3(value.x, float.Parse(valueTo), value.z);
					} else if (member == "z") {
						newValue = new Vector3(value.x, value.y, float.Parse (valueTo));
					}
					
					//pInfo.SetValue(component, newValue, null);			
				}
			}
		}
		
		startAnim();
	}
	
	public void startAnim() {
		StartCoroutine("run");
	}
	
	public IEnumerator run() {
		float playTime = 0.0f;
		float startTime = Time.time;
		
		Component component = target.GetComponent(componentName);
		string[] propertyNames = propertyName.Split('.');
		PropertyInfo pInfo = component.GetType ().GetProperty(propertyNames[0]);
		
		while(Time.time - startTime < duration) {
			float t = ( Time.time - startTime ) / duration;
			
			if (pInfo.PropertyType == typeof(Vector3)) {
				Vector3 value = (Vector3)pInfo.GetValue (component, null);
				if (propertyNames.Length == 2) {
					string member = propertyNames[1];
					Vector3 newValue = new Vector3();
					
					if (member == "x") {
						float vTo = 0.0f;
						try {
							vTo = float.Parse(valueTo);
						} catch {
							vTo = value.x;
						}
						
						float vFrom = 0.0f;
						try {
							vFrom = float.Parse (valueFrom);
						} catch {
							vFrom = value.x;
						}
						
						float newX =  vTo * t + vFrom * (1.0f - t);
						newValue = new Vector3(newX, value.y, value.z);
					} else if (member == "y") {
						float vTo = 0.0f;
						try {
							vTo = float.Parse(valueTo);
						} catch {
							vTo = value.y;
						}
						
						float vFrom = 0.0f;
						try {
							vFrom = float.Parse (valueFrom);
						} catch {
							vFrom = value.y;
						}
						
						float newY =  vTo * t + vFrom * (1.0f - t);
						newValue = new Vector3(value.x, newY, value.z);
					} else if (member == "z") {
						float vTo = 0.0f;
						try {
							vTo = float.Parse(valueTo);
						} catch {
							vTo = value.z;
						}
						
						float vFrom = 0.0f;
						try {
							vFrom = float.Parse (valueFrom);
						} catch {
							vFrom = value.z;
						}
						
						float newZ =  vTo * t + vFrom * (1.0f - t);
						newValue = new Vector3(value.x, value.y, newZ);
					}
					
					pInfo.SetValue(component, newValue, null);			
				}
			}
			
			yield return null;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
