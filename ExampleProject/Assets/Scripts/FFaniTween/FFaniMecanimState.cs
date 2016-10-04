using UnityEngine;
using System.Collections;

public class FFaniMecanimState : FFaniMation {
	public Animator target;
	public string stateName;
	public float fadeTime = 0.0f;

	public FFaniMecanimState () {
	}

	override public void Init () {
		Debug.Log("MecanimState Play: " + stateName);

		base.Init();

		if (target == null) {
			return;
		}

		if (fadeTime > 0.0f) {
			target.CrossFadeInFixedTime(stateName, fadeTime);
		} else {
			target.PlayInFixedTime(stateName);
		}

		// //need update for updating AnimatorStateInfo
		// it has a bug
		//target.Update(0.0f);

		// if (target.GetCurrentAnimatorStateInfo(0).loop) {
		// 	duration = -1.0f;
		// } else {
		// 	duration = target.GetCurrentAnimatorStateInfo(0).length;
		// 	Debug.Log("stateName: " + stateName + " - " + duration);
		// }

		duration = -1.0f;
	}

	protected override void OnUpdatePlay (float delta)
	{
		// skip first update because current animation state info is not updated yet
		if (currentTime <= fadeTime) {
			return;
		}

		if (target.IsInTransition(0)) {
			return;
			// if (target.GetNextAnimatorStateInfo(0).loop) {
			// 	duration = -1.0f;
			// } else {
			// 	duration = target.GetNextAnimatorStateInfo(0).length;
			// 	//Debug.Log("stateName: " + stateName + " - " + duration);
			// }			
		} else {
			if (target.GetCurrentAnimatorStateInfo(0).loop) {
				state = "completed";
				//duration = -1.0f;
			} else {
				//duration = target.GetCurrentAnimatorStateInfo(0).length;
				//Debug.Log("stateName: " + stateName + " - " + duration);
				//Debug.Log("ClipName: " + target.GetCurrentAnimatorClipInfo(0)[0].clip.name);

				if (target.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
					!target.GetCurrentAnimatorStateInfo(0).loop)
				{
					state = "completed";
					Debug.Log("ClipName: " + target.GetCurrentAnimatorClipInfo(0)[0].clip.name + " Completed");
				}
			}
		}
	}
}
