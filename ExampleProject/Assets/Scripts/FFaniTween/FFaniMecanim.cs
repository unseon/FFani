using UnityEngine;
using System.Collections;

public class FFaniMecanim : FFaniMation {
	public Animator target;
	public string stateName;

	public bool loop;

	public FFaniMecanim (Animator target, string stateName) {
		this.target = target;
		this.stateName = stateName;
	}

	override public void Init () {
		base.Init();

		if (target == null) {
			return;
		}

		target.Play(stateName);
		if (target.GetCurrentAnimatorStateInfo(0).loop) {
			duration = -1.0f;
		} else {
			duration = target.GetCurrentAnimatorStateInfo(0).length;
		}
	}
}
