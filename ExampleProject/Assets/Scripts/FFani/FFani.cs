using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System;

public class FFani {

	public delegate void Callback();

	public static bool Fire(Component target
	                        , string memberName
	                        , object to = null
	                        , object from = null
	                        , float duration = 0.5f
	                        , float delayTime = 0.0f
	                        , Callback callback = null) {
	
		FFaniMemberAnimation anim01 = new FFaniMemberAnimation();
		anim01.targetComponent = target;
		anim01.propertyName = memberName;
		anim01.to = to;
		anim01.duration = duration;
		anim01.delayTime = delayTime;
		anim01.onFinishCallback = callback;

		anim01.start ();

		return true;
	}
	
	public static FFaniMember createMember(object target, string memberName) {
	
		try {
			MemberInfo mi = target.GetType().GetMember(memberName)[0];
			
			if (mi.MemberType == MemberTypes.Property) {
				return new FFaniMemberFromProperty(target, mi);
			} else if (mi.MemberType == MemberTypes.Field) {
				return new FFaniMemberFromField(target, mi);
			} else {
				return null;
			}
		
		} catch(Exception e) {
			Debug.Log ("memberName: " + memberName);
			
			return null;
        }
			           
	}
	
	public static FFaniMember createValueTypeMember(FFaniMember member, List<string> names) {
		object obj = member.getValue();
		
		FFaniMember submember = createMember(obj, names[0]);
		
		if (names.Count == 1) {
			return new FFaniValueTypeMember(member, submember);
		}
		names.RemoveAt(0);
		FFaniMember valueSubmember = createValueTypeMember (submember, names);
		
		return new FFaniValueTypeMember(member, valueSubmember);
		//return new FFaniComplexProperty(member, submember);
	}
	
	public static FFaniMember getTargetMember(FFaniMember member, List<string> names) {
		if (member.getType().IsValueType) {
			return createValueTypeMember(member, names);
		} else {
		
			if (names.Count == 1) {
				// if this name is the last name, create FFaniMember object. 
				return createMember (member, names[0]);
			} else {
				// recursive call to get FFaniMember for the last member name.	
				FFaniMember subMember = createMember(member.getValue(), names[0]);
			
				names.RemoveAt(0);
				
				return getTargetMember (subMember, names);
			}
		}
	}
	
	public static FFaniMember getTargetMember(object target, string memberName) {
		if (target == null || memberName == null) {
			return null;
		}

		// replace short name of Transform with real name;
		if (target.GetType() == typeof(Transform) &&
		    transformShortName.ContainsKey(memberName))
		{
			memberName = transformShortName[memberName];
		}
		
		List<string> names = new List<string>(memberName.Split('.'));
		string firstName = names[0];
		
		// create the top FFaniMember object;	
		
		MemberInfo mi = target.GetType ().GetMember(firstName)[0];
		FFaniMember member = createMember(target, firstName);
		
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
