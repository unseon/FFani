using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class FFani {

	public bool Fire() {
	
		return true;
	}
	
	public static FFaniMember createMember(object target, string memberName) {
		MemberInfo mi = target.GetType().GetMember(memberName)[0];
		
		if (mi.MemberType == MemberTypes.Property) {
			return new FFaniMemberFromProperty(target, mi);
		} else if (mi.MemberType == MemberTypes.Field) {
			return new FFaniMemberFromField(target, mi);
		} else {
			return null;
		}
	}
	
	public static FFaniComplexProperty createValueTypeMember(FFaniMember member, string submemberName) {
		object obj = member.getValue();
		FFaniMember submember = createMember (obj, submemberName);
		
		
		if (member.getType() == typeof(Vector3)) {
			return new FFaniValueTypeMember(member, submember);
		}
		
		return null;
		
		//return new FFaniComplexProperty(member, submember);
	}
	
	public static FFaniMember getTargetMember(FFaniMember member, List<string> names) {
		if (member.getType().IsValueType) {
			// if member is value type, create ValueTypeMember
			return createValueTypeMember (member, names[0]);
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
}
