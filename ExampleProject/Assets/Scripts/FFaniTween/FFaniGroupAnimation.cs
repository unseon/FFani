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

		if (animList.Count == 0) {
			Complete();
			return;
		}

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

	protected int activeAnimNumber = 0;

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
			//float newDelta = prevTime + delta - currentAnim.duration;
			activeAnimNumber++;

			if (activeAnimNumber < animList.Count) {
				currentAnim = animList[activeAnimNumber];
				currentAnim.Reset ();
//				prevTime = newDelta;
//				currentAnim.Update(newDelta);
				prevTime = 0.0f;
				currentAnim.Update(0.0f);

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

public class FFaniStepAnimation : FFaniSerialAnimation {
	public float interval {
		get {
			return intervalRest.duration;
		}

		set {
			intervalRest.duration = value;
		}
	}

	FFaniMation activeAnim;
	private FFaniMation intervalRest = new FFaniMation();

	public FFaniStepAnimation SetInterval(float interval = 0.0f) {
		this.interval = interval;

		return this;
	}

	override public void Init () {
		base.Init();

		if (animList.Count > 0) {
			activeAnim = animList[0];
		} else {
			Complete();
			return;
		}
	}

	public void Next() {
		if (activeAnim != null) {
			activeAnim.Complete();
		} else {
			state = "completed";
		}
	}

	public FFaniStepAnimation SetSkipTrigger(ref FFani.Callback trigger) {
		trigger += Next;

		return this;
	}

	override protected void OnUpdatePlay(float delta) {
		//Debug.Log ("seq currentTime: " + currentTime);

		float prevTime = activeAnim.currentTime;

		if (activeAnim.state != "completed") {
			activeAnim.Update(delta);
		}
		
		while (activeAnim.state == "completed") {
			//float newDelta = Mathf.Max(0.0f, prevTime + delta - activeAnim.duration);

			if (activeAnimNumber < animList.Count - 1) {
				// if current anim is not last one

				if (activeAnim == intervalRest) {
					activeAnimNumber++;
					activeAnim = animList[activeAnimNumber];
					Debug.Log (activeAnimNumber + " reset ");
				} else {
					activeAnim = intervalRest;
				}

				activeAnim.Reset ();
//				prevTime = newDelta;
//				activeAnim.Update(newDelta);
				prevTime = 0.0f;
				activeAnim.Update(0.0f);
			} else {
				activeAnim = null;
				state = "completed";
				break;
			}
		}
	}
}