using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System;

public class FFani {

	public delegate void Callback();

	public static FFaniMation Sleep(float duration) {
		FFaniMation anim = new FFaniMation();
		anim.duration = duration;

		return anim;
	}

	public static FFaniMation Mation(Component target
	                        , string propertyName
	                        , object to = null
	                        , object from = null
	                        , float duration = 0.5f
	                        , float delayTime = 0.0f)
	{
		FFaniPropertyAnimation anim = new FFaniPropertyAnimation();
		anim.targetComponent = target;
		anim.propertyName = propertyName;
		anim.to = to;
		anim.duration = duration;
		anim.delayTime = delayTime;

		return anim;
	}

	public static FFaniSerialAnimation Serial (params FFaniMation[] anims) {
		FFaniSerialAnimation serial = new FFaniSerialAnimation();

		for (int i = 0; i < anims.Length; i++) {
			serial.Add (anims[i]);
		}

		return serial;
	}

	public static FFaniParallelAnimation Parallel (params FFaniMation[] anims) {
		FFaniParallelAnimation parAnim = new FFaniParallelAnimation();
		
		for (int i = 0; i < anims.Length; i++) {
			parAnim.Add (anims[i]);
		}
		
		return parAnim;
	}
	
	public static FFaniProperty CreateMember(object target, string propertyName) {
	
		try {
			MemberInfo mi = target.GetType().GetMember(propertyName)[0];
			
			if (mi.MemberType == MemberTypes.Property) {
				return new FFaniPropertyFromPropertyInfo(target, mi);
			} else if (mi.MemberType == MemberTypes.Field) {
				return new FFaniPropertyFromFieldInfo(target, mi);
			} else {
				return null;
			}
		
		} catch(Exception e) {
			Debug.Log (e);
			Debug.Log ("propertyName: " + propertyName);
			
			return null;
        }
			           
	}
	
	public static FFaniProperty createValueTypeMember(FFaniProperty member, List<string> names) {
		object obj = member.getValue();
		
		FFaniProperty submember = CreateMember(obj, names[0]);
		
		if (names.Count == 1) {
			return new FFaniFeedbackProperty(member, submember);
		}
		names.RemoveAt(0);
		FFaniProperty valueSubmember = createValueTypeMember (submember, names);
		
		return new FFaniFeedbackProperty(member, valueSubmember);
		//return new FFaniComplexProperty(member, submember);
	}
	
	public static FFaniProperty getTargetMember(FFaniProperty member, List<string> names) {
		if (member.getType().IsValueType) {
			return createValueTypeMember(member, names);
		} else {
		
			if (names.Count == 1) {
				// if this name is the last name, create FFaniMember object. 
				return CreateMember (member.getValue(), names[0]);
			} else {
				// recursive call to get FFaniMember for the last member name.	
				FFaniProperty subMember = CreateMember(member.getValue(), names[0]);
			
				names.RemoveAt(0);
				
				return getTargetMember (subMember, names);
			}
		}
	}
	
	public static FFaniProperty getTargetMember(object target, string propertyName) {
		if (target == null || propertyName == null) {
			return null;
		}

		// replace short name of Transform with real name;
		if (target.GetType() == typeof(Transform) &&
		    transformShortName.ContainsKey(propertyName))
		{
			propertyName = transformShortName[propertyName];
		}
		
		List<string> names = new List<string>(propertyName.Split('.'));
		string firstName = names[0];
		
		FFaniProperty member = CreateMember(target, firstName);
		
		if (names.Count == 1) {
			return member;
		}
		
		names.RemoveAt(0);
		
		return getTargetMember (member, names);
	}

	public static Dictionary<string, string> transformShortName = new Dictionary<string, string> {
		{"pos", "localPosition"},
		{"px", "localPosition.x"},
		{"py", "localPosition.y"},
		{"pz", "localPosition.z"},
		{"rot", "localRotation.eulerAngles"},
		{"rx", "localRotation.eulerAngles.x"},
		{"ry", "localRotation.eulerAngles.y"},
		{"rz", "localRotation.eulerAngles.z"},
		{"scl", "localScale"},
		{"sx", "localScale.x"},
		{"sy", "localScale.y"},
		{"sz", "localScale.z"},
		{"wpos", "position"},
		{"wpx", "position.x"},
		{"wpy", "position.y"},
		{"wpz", "position.z"},
	};
}
