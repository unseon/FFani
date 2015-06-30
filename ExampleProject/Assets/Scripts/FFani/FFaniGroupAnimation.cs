using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniGroupAnimation : FFaniMation {
	public List<FFaniMation> animList = new List<FFaniMation>();
	
	public void Add(FFaniMation anim) {
		animList.Add(anim);
	}
}

public class FFaniParallelAnimation : FFaniGroupAnimation {

	List<FFaniMation> runningAnimList;

	override protected void Init () {
		base.Init();

		runningAnimList = new List<FFaniMation>(animList);

		for (int i = 0; i < runningAnimList.Count; i++) {
			runningAnimList[i].Reset();
		}
	}

	override protected void OnUpdate(float delta) {
		for (int i = 0; i < runningAnimList.Count; i++) {
			runningAnimList[i].UpdateDelta(delta);
			if (runningAnimList[i].state == "finished") {
				runningAnimList.RemoveAt(i);
				i--;
			}
		}

		if (runningAnimList.Count == 0) {
			Finish ();
		}
	}
}

public class FFaniSerialAnimation : FFaniGroupAnimation {

	int activeAnimNumber = -1;

	override protected void Init () {
		base.Init();

		if (animList.Count > 0) {
			animList[0].Reset();
			activeAnimNumber = 0;
		}
	}

	override protected void OnUpdate(float delta) {
		//Debug.Log ("seq currentTime: " + currentTime);

		FFaniMation currentAnim = animList[activeAnimNumber];

		float prevTime = currentAnim.currentTime;
		currentAnim.UpdateDelta(delta);

		if (currentAnim.state == "finished") {
			float newDelta = prevTime + delta - currentAnim.duration;
			activeAnimNumber++;

			if (activeAnimNumber < animList.Count) {
				currentAnim = animList[activeAnimNumber];
				currentAnim.Reset ();
				currentAnim.UpdateDelta(newDelta);
			} else {
				Finish();
			}
		}
	}
}
