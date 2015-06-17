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

	public override string getName () {
		return member.getName() + "." + subMember.getName();
	}
	
	public override void setValue(object value) {
	}
}

public class FFaniValueTypeMember : FFaniComplexProperty {

	public FFaniValueTypeMember(FFaniMember member, FFaniMember subMember)
		: base(member, subMember)
	{
	}
	
	public override void setValue(object value) {
		// '=' means copy of value type object;
		object newValue = member.getValue();
		
		FFaniMember newMember = FFani.createMember(newValue, subMember.getName());
		newMember.setValue(value);
		
		member.setValue (newValue);
	}	
}