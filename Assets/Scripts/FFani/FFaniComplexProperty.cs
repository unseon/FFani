using UnityEngine;
using System;
using System.Reflection;

public class FFaniComplexProperty : FFaniMember{
	public FFaniMember member;
	public FFaniMember subMember;
	
	public FFaniComplexProperty(FFaniMember member, FFaniMember subMember) {
		this.member = member;
		this.subMember = subMember;
	}
	
	public override Type getType () {
		return subMember.getType();
	}
	
	public override object getValue () {
		return subMember.getValue();
	}

	public override void setValue(object value) {
		if (member.getType() == typeof(Vector3)) {
			Vector3 memberValue = (Vector3)member.getValue();
			float subMemberValue = Convert.ToSingle(value);
			if (subMember.getName() == "x") {
				member.setValue(new Vector3(subMemberValue, memberValue.y, memberValue.z));
			} else if (subMember.getName() == "y") {
				member.setValue(new Vector3(memberValue.x, subMemberValue, memberValue.z));
			} else if (subMember.getName() == "z") {
				member.setValue(new Vector3(memberValue.x, memberValue.y, subMemberValue));
			}
		}
	}
	
	public override string getName () {
		return member.getName() + "." + subMember.getName();
	}
}
