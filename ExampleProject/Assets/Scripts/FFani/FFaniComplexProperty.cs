using UnityEngine;
using System;
using System.Reflection;

public class FFaniComplexProperty : FFaniMember{
	public FFaniMember member;
	public FFaniMember subMember;
	
	public string __name;
	
	public FFaniComplexProperty(FFaniMember member, FFaniMember subMember) {
		this.member = member;
		this.subMember = subMember;
		
		__name = getName();
		
		//Debug.Log ("Property" + __name);
	}
	
	public override Type getType () {
		return subMember.getType();
	}
	
	public override object getValue () {
		return subMember.getValue();
	}

	public override string getName () {
		return member.getName() + "." + subMember.getName();
		//return member.getName();
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
		
		if (subMember.GetType() == typeof(FFaniValueTypeMember)) {
			FFaniValueTypeMember valMember = (FFaniValueTypeMember)subMember;
			valMember.setValue(value);
									
			member.setValue (valMember.member.obj);
		} else {
			subMember.setValue(value);
			
			member.setValue (subMember.obj);
		}
	}	
}