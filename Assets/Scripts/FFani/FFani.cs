using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class FFani {

	public bool Fire() {
	
		return true;
	}
	
	public static FFaniMember createMember(object target, string memberName) {
		MemberInfo mi = target.GetType ().GetMember(memberName)[0];
		
		if (mi.MemberType == MemberTypes.Property) {
			return new FFaniMemberFromProperty(target, mi);
		} else if (mi.MemberType == MemberTypes.Field) {
			return new FFaniMemberFromField(target, mi);
		} else {
			return null;
		}
	}
	
	public static FFaniMember createMember(FFaniMember member, string submemberName) {
		object obj = member.getValue();
		
		return createMember (obj, submemberName);
	}
	
	public static FFaniMember createMember(FFaniMember member, List<string> names) {
		if (member.getType() == typeof(Vector3)) {
			if (names.Count != 1) {
				return null;
			}
			FFaniMember subMember = createMember(member, names[0]);
						
			return new FFaniComplexProperty(member, subMember);
		} else {
		
			if (names.Count == 1) {
				return createMember (member, names[0]);
			} else {
				names.RemoveAt(0);
				
				FFaniMember subMember = createMember(member, names[0]);
				
				return createMember (subMember, names);
			}
		}
	}
	
	public static FFaniMember getTargetMember(object target, List<string> names) {
		MemberInfo mi = target.GetType ().GetMember(names[0])[0];
		
		string memberName = names[0];
		FFaniMember member = createMember(target, memberName);
		
		if (names.Count == 1) {
			return member;
		}
		
		names.RemoveAt(0);

		return createMember (member, names);
//						
//		if (member.getType() == typeof(Vector3)) {
//			return FFani.createMember (member, names[0]);
//		}
//		
//		if (mi.MemberType == MemberTypes.Property) {
//			PropertyInfo pi = (PropertyInfo)mi;
//			if (pi.PropertyType == typeof(Vector3)) {
//				MemberInfo mmi = pi.PropertyType.GetMember(names[0])[0];
//				return new FFaniProperty(mmi, target, mi);
//			} else {
//				object t = pi.GetValue(target, null);
//				return getTargetMember (t, names, target);
//			}
//		} else if (mi.MemberType == MemberTypes.Field) {
//			FieldInfo fi = (FieldInfo)mi;
//			object t = fi.GetValue(target);
//			return getTargetMember (t, names, target);
//		} else {
//			return null;
//		}
	}
	
	public static FFaniMember getTargetMember(object target, string memberName) {
		return getTargetMember (target, new List<string>(memberName.Split('.')));
	}
}
