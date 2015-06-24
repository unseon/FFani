using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniSequentialAnimation : FFaniAnimation {

	List<FFaniAnimation> animList = new List<FFaniAnimation>();
	int activeAnimNumber = -1;

	override protected void onStart () {
		base.onStart();

		if (animList.Count > 0) {
			animList[0].start();
			activeAnimNumber = 0;
		}
	}

	override protected void onUpdate(float delta) {
		FFaniAnimation currentAnim = animList[activeAnimNumber];

		float prevTime = currentAnim.currentTime;
		currentAnim.updateDelta(delta);

		if (currentAnim.state == "finished") {
			float newDelta = prevTime + delta - currentAnim.duration;
			activeAnimNumber++;

			if (activeAnimNumber < animList.Count) {
				currentAnim = animList[activeAnimNumber];
				currentAnim.start ();
				currentAnim.updateDelta(newDelta);
			} else {
				onFinish();
			}
		}
	}

	public void add(FFaniAnimation anim) {
		animList.Add(anim);
	}
}
