using UnityEngine;
using System;
using System.Reflection;

public class FFaniValueTypeMember : FFaniMember {

	public FFaniMember member;
	public FFaniMember submember;

	// just used for debugging
	public string __name;
	
	public FFaniValueTypeMember(FFaniMember member, FFaniMember subMember)
	{
		this.member = member;
		this.submember = subMember;
		
		__name = getName();
		
		//Debug.Log ("Property" + __name);
	}

	public override Type getType () {
		return submember.getType();
	}
	
	public override object getValue () {
		return submember.getValue();
	}
	
	public override string getName () {
		return member.getName() + "." + submember.getName();
		//return member.getName();
	}

	public override void setValue(object value) {
		// '=' means copy of value type object;
		object newValue = member.getValue();
		
		if (submember.GetType() == typeof(FFaniValueTypeMember)) {
			// casting is needed to get submember.member.obj
			FFaniValueTypeMember valMember = (FFaniValueTypeMember)submember;
			valMember.setValue(value);

			// reassign submember.member.obj to member's value
			member.setValue (valMember.member.obj);
		} else {
			submember.setValue(value);

			// reassign submember.obj to member's value
			member.setValue (submember.obj);
		}
	}	
}