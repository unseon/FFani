using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class MyClass{
	public float myValue;
}

public class TestQAnimation : MonoBehaviour {

	public float myValue = 2.0f;
	
	public Vector3 myVector = new Vector3();
	
	public MyClass myObj = new MyClass();

	// Use this for initialization
	void Awake () {
		Transform tr = GameObject.Find ("Cube").transform;
	
		FFaniProperty anim = new FFaniProperty();
		anim.targetComponent = GameObject.Find ("Cube").transform;
		anim.propertyName = "position";
		anim.valueTo = new Vector3(5, 0, 0);
		anim.duration = 1.0f;
		
		anim.start();
//		
//		object a = 10.0f;
//		
//		Debug.Log (a.ToString());
//		Debug.Log (a.GetType() == typeof(float));
		object b = new Vector3(1.0f, 2.0f, 3.0f);
		Vector3 c = (Vector3)b;
//		Debug.Log (c);
		
		Debug.Log ("parameter test: " + ParameterFunc(t1: 2, t2:3));
		
//		FFaniMember px = FFaniProperty.getTargetMember(tr, "position");
//		px.setValue(new Vector3(10, 20, 30));
//		Debug.Log (tr.position);
		
		Type type = tr.position.GetType();
		
		
		//FieldInfo fi = tr.GetType().GetField("position");
		//fi.SetValue(tr, new Vector3());	
		
		PropertyInfo pi = tr.GetType ().GetProperty("position");
		MemberInfo mi = tr.GetType ().GetMember("position")[0];
		
		MemberTypes t = mi.MemberType;
		
		bool bt = t == MemberTypes.Property;
		
		//bool bt = t.GetType() == typeof(Property);
		
		PropertyInfo ppi = (PropertyInfo)mi;
		if (ppi != null) {
			Debug.Log("PropertyInfo");
			MemberInfo mxi = ppi.PropertyType.GetMember("x")[0];
			
		}

		Debug.Log (mi.GetType());
		
		
		FieldInfo pix = pi.GetValue(tr, null).GetType ().GetField("x");
//		pi.SetValue (tr, new Vector3(1, 2, 3), null);
		
		Debug.Log (tr.position);
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	int ParameterFunc(int t1 = 0, int t2 = 0) {
		return t1 * t2;
	}
}
