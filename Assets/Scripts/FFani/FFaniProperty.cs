using UnityEngine;
using System;
using System.Reflection;

public class FFaniProperty {
	public FFaniMember member;
	public FFaniMember owner;


	public MemberInfo memberInfo;
	public MemberInfo assigneeInfo;
	public object reflectedObject;
	
	public FFaniProperty(MemberInfo memberInfo, object reflectedObject, MemberInfo assigneeInfo = null) {
		this.memberInfo = memberInfo;
		this.reflectedObject = reflectedObject;
		this.assigneeInfo = assigneeInfo;
	}
	
	public Type getType() {
		if (memberInfo.MemberType == MemberTypes.Property) {
			PropertyInfo pi = (PropertyInfo)memberInfo;

			return pi.PropertyType;
		} else if (memberInfo.MemberType == MemberTypes.Field){
			FieldInfo fi = (FieldInfo)memberInfo;
			
			return fi.FieldType;
		}
		
		return null;
	}
	
	Type getAssigneeType() {
		if (assigneeInfo.MemberType == MemberTypes.Property) {
			PropertyInfo pi = (PropertyInfo)assigneeInfo;
			
			return pi.PropertyType;
		} else if (assigneeInfo.MemberType == MemberTypes.Field){
			FieldInfo fi = (FieldInfo)assigneeInfo;
			
			return fi.FieldType;
		}
		
		return null;
	}
	
	object getAssignee() {
		if (assigneeInfo.MemberType == MemberTypes.Property) {
			PropertyInfo pi = (PropertyInfo)assigneeInfo;
			
			return pi.GetValue(reflectedObject, null);
			
		} else if (assigneeInfo.MemberType == MemberTypes.Field){
			FieldInfo fi = (FieldInfo)assigneeInfo;
			return fi.GetValue(reflectedObject);
		}
		
		return null;
	}
	
	public void setValue(object value) {
		if (assigneeInfo == null) {
			if (memberInfo.MemberType == MemberTypes.Property) {
				PropertyInfo pi = (PropertyInfo)memberInfo;
				pi.SetValue(reflectedObject, value, null);
			} else if (memberInfo.MemberType == MemberTypes.Field){
				FieldInfo fi = (FieldInfo)memberInfo;
				fi.SetValue(reflectedObject, value);
			}
		} else {
			object assignee = getAssignee();
	
			if (assignee.GetType() == typeof(Vector3)) {
				Vector3 assigneeValue = (Vector3)assignee;
				float memberValue = Convert.ToSingle(value);
				if (memberInfo.Name == "x") {
					assignee = new Vector3(memberValue, assigneeValue.y, assigneeValue.z);
				} else if (memberInfo.Name == "y") {
					assignee = new Vector3(assigneeValue.x, memberValue, assigneeValue.z);
				} else if (memberInfo.Name == "z") {
					assignee = new Vector3(assigneeValue.x, assigneeValue.y, memberValue);
				}
			}
			
			if (assigneeInfo.MemberType == MemberTypes.Property) {
				PropertyInfo pi = (PropertyInfo)assigneeInfo;
				pi.SetValue(reflectedObject, assignee, null);
			} else if (assigneeInfo.MemberType == MemberTypes.Field){
				FieldInfo fi = (FieldInfo)assigneeInfo;
				fi.SetValue(reflectedObject, assignee);
			}
		}
	}
	
	public object getValue() {
		if (assigneeInfo == null) {
			if (memberInfo.MemberType == MemberTypes.Property) {
				PropertyInfo pi = (PropertyInfo)memberInfo;
	
				return pi.GetValue(reflectedObject, null);
			} else if (memberInfo.MemberType == MemberTypes.Field){
				FieldInfo fi = (FieldInfo)memberInfo;
							
				return fi.GetValue(reflectedObject);;
			}
		} else {
			object assignee = getAssignee();
		
			if (assignee != null) {
			
				if (memberInfo.MemberType == MemberTypes.Property) {
					PropertyInfo pi = (PropertyInfo)memberInfo;
					
					return pi.GetValue(assignee, null);
				} else if (memberInfo.MemberType == MemberTypes.Field){
					FieldInfo fi = (FieldInfo)memberInfo;
					
					return fi.GetValue(assignee);;
				}
			}
		}
		
		return null;
	}
}