using UnityEngine;
using System;
using System.Reflection;

public class FFaniFeedbackProperty : FFaniProperty {

	public FFaniProperty property;
	public FFaniProperty subproperty;

	// just used for debugging
	public string __name;
	
	public FFaniFeedbackProperty(FFaniProperty property, FFaniProperty subproperty)
	{
		this.property = property;
		this.subproperty = subproperty;
		
		__name = getName();

		this.obj = property.obj;

		//Debug.Log("Feedback Property" + property.obj);
		//Debug.Log("Feedback Property" + __name);
	}

	public override Type getType () {
		return subproperty.getType();
	}
	
	public override object getValue () {
		return subproperty.getValue();
	}
	
	public override string getName () {
		return property.getName() + "." + subproperty.getName();
		//return member.getName();
	}

	public override void setValue(object value) {
		if (subproperty.GetType() == typeof(FFaniFeedbackProperty)) {
			// casting is needed to get submember.member.obj
			subproperty.obj = property.getValue();
			FFaniFeedbackProperty valMember = (FFaniFeedbackProperty)subproperty;
			valMember.setValue(value);

			//Debug.Log (valMember.property.obj);

			// reassign submember.member.obj to member's value
			property.setValue (valMember.property.obj);
		} else {
			//Debug.Log (member.getValue ());
			subproperty.obj = property.getValue();

			subproperty.setValue(value);
			// reassign submember.obj to member's value
			property.setValue (subproperty.obj);
		}
	}	
}