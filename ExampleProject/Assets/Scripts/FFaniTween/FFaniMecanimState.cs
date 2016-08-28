using UnityEngine;
using System.Collections;

public class FFaniMecanimState : FFaniMation {
	public Animator target;
	public string stateName;
	public float fadeTime = 0.0f;

	public FFaniMecanimState () {
	}

	override public void Init () {
		base.Init();

		if (target == null) {
			return;
		}

		if (fadeTime > 0.0f) {
			target.CrossFadeInFixedTime(stateName, fadeTime);
		} else {
			target.PlayInFixedTime(stateName);
		}

		if (target.GetCurrentAnimatorStateInfo(0).loop) {
			duration = -1.0f;
		} else {
			duration = target.GetCurrentAnimatorStateInfo(0).length;
		}
	}

	protected override void OnUpdatePlay (float delta)
	{
		//Debug.Log("duration: " + target.GetCurrentAnimatorStateInfo(0).length);
		if (target.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
			!target.GetCurrentAnimatorStateInfo(0).loop)
		{
			state = "completed";
		}
	}
}
