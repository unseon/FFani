using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniGroupAnimation : FFaniAnimation {
	public List<FFaniAnimation> animList = new List<FFaniAnimation>();
	
	public void add(FFaniAnimation anim) {
		animList.Add(anim);
	}
}

public class FFaniParallelAnimation : FFaniGroupAnimation {

	List<FFaniAnimation> runningAnimList;

	override protected void onStart () {
		base.onStart();

		runningAnimList = new List<FFaniAnimation>(animList);

		for (int i = 0; i < runningAnimList.Count; i++) {
			runningAnimList[i].reset();
		}
	}

	override protected void onUpdate(float delta) {
		for (int i = 0; i < runningAnimList.Count; i++) {
			runningAnimList[i].updateDelta(delta);
			if (runningAnimList[i].state == "finished") {
				runningAnimList.RemoveAt(i);
				i--;
			}
		}

		if (runningAnimList.Count == 0) {
			onFinish ();
		}
	}
}

public class FFaniSequentialAnimation : FFaniGroupAnimation {

	int activeAnimNumber = -1;

	override protected void onStart () {
		base.onStart();

		if (animList.Count > 0) {
			animList[0].reset();
			activeAnimNumber = 0;
		}
	}

	override protected void onUpdate(float delta) {
		//Debug.Log ("seq currentTime: " + currentTime);

		FFaniAnimation currentAnim = animList[activeAnimNumber];

		float prevTime = currentAnim.currentTime;
		currentAnim.updateDelta(delta);

		if (currentAnim.state == "finished") {
			float newDelta = prevTime + delta - currentAnim.duration;
			activeAnimNumber++;

			if (activeAnimNumber < animList.Count) {
				currentAnim = animList[activeAnimNumber];
				currentAnim.reset ();
				currentAnim.updateDelta(newDelta);
			} else {
				onFinish();
			}
		}
	}
}
