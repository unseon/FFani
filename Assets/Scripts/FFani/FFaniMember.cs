using System;
using System.Reflection;

public class FFaniMember {
	public MemberInfo memberInfo;
	public object reflectedObject;
	
	public MemberInfo reflectedObjectInfo;
	public object rereflectedObject;
	
	public FFaniMember(MemberInfo memberInfo, object reflectedObject, MemberInfo reflectedObjectInfo = null, object rereflectedObject = null) {
		this.memberInfo = memberInfo;
		this.reflectedObject = reflectedObject;
		
		this.reflectedObjectInfo = reflectedObjectInfo;
		this.rereflectedObject = rereflectedObject;
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
	
	public bool setValue(object value) {
		if (memberInfo.MemberType == MemberTypes.Property) {
			PropertyInfo pi = (PropertyInfo)memberInfo;
			pi.SetValue(reflectedObject, value, null);
			
			return true;
		} else if (memberInfo.MemberType == MemberTypes.Field){
			FieldInfo fi = (FieldInfo)memberInfo;
			fi.SetValue(reflectedObject, value);
			
			return true;
		}
		
		return false;
	}
	
	public object getValue() {
		if (memberInfo.MemberType == MemberTypes.Property) {
			PropertyInfo pi = (PropertyInfo)memberInfo;

			return pi.GetValue(reflectedObject, null);
		} else if (memberInfo.MemberType == MemberTypes.Field){
			FieldInfo fi = (FieldInfo)memberInfo;
						
			return fi.GetValue(reflectedObject);;
		}
		
		return null;
	}
}

