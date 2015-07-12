using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniGroupAnimation : FFaniMation {
	public FFaniGroupAnimation() {
		duration = -1.0f;
	}

	public List<FFaniMation> animList = new List<FFaniMation>();
	
	public void Add(FFaniMation anim) {
		animList.Add(anim);
	}
}

public class FFaniParallelAnimation : FFaniGroupAnimation {

	List<FFaniMation> runningAnimList;

	override public void Init () {
		base.Init();

		runningAnimList = new List<FFaniMation>(animList);

		for (int i = 0; i < runningAnimList.Count; i++) {
			runningAnimList[i].Reset();
		}
	}

	override protected void OnUpdatePlay(float delta) {
		for (int i = 0; i < runningAnimList.Count; i++) {
			runningAnimList[i].Update(delta);
			if (runningAnimList[i].state == "completed") {
				runningAnimList.RemoveAt(i);
				i--;
			}
		}

		if (runningAnimList.Count == 0) {
			Complete ();
		}
	}
}

public class FFaniSerialAnimation : FFaniGroupAnimation {

	int activeAnimNumber = -1;

	override public void Init () {
		base.Init();

		if (animList.Count > 0) {
			animList[0].Reset();
			activeAnimNumber = 0;
		}
	}

	override protected void OnUpdatePlay(float delta) {
		//Debug.Log ("seq currentTime: " + currentTime);

		FFaniMation currentAnim = animList[activeAnimNumber];

		float prevTime = currentAnim.currentTime;
		currentAnim.Update(delta);

		while (currentAnim.state == "completed") {
			float newDelta = prevTime + delta - currentAnim.duration;
			activeAnimNumber++;

			if (activeAnimNumber < animList.Count) {
				currentAnim = animList[activeAnimNumber];
				currentAnim.Reset ();
				currentAnim.Update(newDelta);
			} else {
				state = "completed";
				break;
			}
		}
	}

	override public void Complete() {
		//Debug.Log ("completed");
		
		if (state == "completed") {
			return;
		}
		
		state = "completed";
		currentTime = duration;

		for (int i = activeAnimNumber; i < animList.Count; i++) {
			FFaniMation anim = animList[i];

			if (anim.state == "ready" || anim.state == "delaying") {
				anim.Init();
				
				if (onStarted != null) {
					anim.onStarted();
				}
			}

			if (anim.state == "playing") {
				anim.Complete();
			}
		}

		if (onCompleted != null) {
			onCompleted();
		}
	}
}
