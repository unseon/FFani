using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

// abstract PropertyInfo or FieldInfo
public abstract class FFaniProperty {
	public object obj;

	public abstract Type getType();
	public abstract object getValue();
	public abstract void setValue(object value);
	public abstract string getName();
//	public abstract object blend(object valueFrom, object valueTo, float t);

	public bool isEqual(FFaniProperty that) {
		if (this.obj == that.obj && this.getName () == that.getName ()) {
			return true;
		} else {
			return false;
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

	public static Dictionary<string, string> transformShortName = new Dictionary<string, string> {
		{"lpos", "localPosition"},
		{"lpx", "localPosition.x"},
		{"lpy", "localPosition.y"},
		{"lpz", "localPosition.z"},
		{"lrot", "localRotation.eulerAngles"},
		{"lrx", "localRotation.eulerAngles.x"},
		{"lry", "localRotation.eulerAngles.y"},
		{"lrz", "localRotation.eulerAngles.z"},
		{"lscl", "localScale"},
		{"lsx", "localScale.x"},
		{"lsy", "localScale.y"},
		{"lsz", "localScale.z"},
		{"wpos", "position"},
		{"wpx", "position.x"},
		{"wpy", "position.y"},
		{"wpz", "position.z"},
		{"wrot", "rotation.eulerAngles"},
		{"wrx", "rotation.eulerAngles.x"},
		{"wry", "rotation.eulerAngles.y"},
		{"wrz", "rotation.eulerAngles.z"},
		{"pos", "position"},
		{"px", "position.x"},
		{"py", "position.y"},
		{"pz", "position.z"},
		{"rot", "rotation.eulerAngles"},
		{"rx", "rotation.eulerAngles.x"},
		{"ry", "rotation.eulerAngles.y"},
		{"rz", "rotation.eulerAngles.z"},
	};
}

public class FFaniPropertyFromPropertyInfo : FFaniProperty {
	PropertyInfo info;
	//object obj;

	public FFaniPropertyFromPropertyInfo(object obj, MemberInfo memberInfo) {
		info = (PropertyInfo)memberInfo;
		this.obj = obj;
	}
	
	public override Type getType() {
		return info.PropertyType;
	}
	
	public override object getValue() {
		return info.GetValue(obj, null);
	}
	
	public override void setValue(object value) {
		try {
			info.SetValue(obj, value, null);
			//Debug.Log ("value: " + value);
		} catch(Exception e) {
			Debug.Log ("type:" + info.PropertyType + "<-" + value.GetType());
			Debug.Log (e);
		}
	}
	
	public override string getName() {
		return info.Name;
	}
}

public class FFaniPropertyFromFieldInfo : FFaniProperty {
	FieldInfo info;
	//object obj;
	
	public FFaniPropertyFromFieldInfo(object obj, MemberInfo memberInfo) {
		info = (FieldInfo)memberInfo;
		this.obj = obj;
	}
	
	public override Type getType() {
		return info.FieldType;
	}
	
	public override object getValue() {
		return info.GetValue(obj);
	}
	
	public override void setValue(object value) {
		try {
			info.SetValue(obj, value);
			//Debug.Log (info.Name + ":" + value);
		} catch(Exception e) {
			Debug.Log (e);
		}
	}
	
	public override string getName() {
		return info.Name;
	}
}

	
	
