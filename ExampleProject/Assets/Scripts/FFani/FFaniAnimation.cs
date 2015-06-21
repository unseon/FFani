using UnityEngine;
using System.Collections;

public class FFaniAnimation {

	public delegate void Callback();

	public Callback onStartCallback = null;
	public Callback onFinishCallback = null;
	public Callback onStopCallback = null;

	public float currentTime = 0.0f;
	public float delayTime = 0.0f;

	public string state = "ready";

	// Use this for initialization
	public void start () {
		if (delayTime > 0.0f) {
			currentTime = - delayTime;
			FFaniManager.instance().play(this);
		} else {
			currentTime = 0.0f;
			onStart();
			FFaniManager.instance().play(this);

			if (onStartCallback != null) {
				onStartCallback();
			}
		}
	}

	public void stop() {
		Debug.Log ("Stopped");
		FFaniManager.instance().stop(this);

		if (onStopCallback != null) {
			onStopCallback();
		}
	}

	public void onFinish() {
		Debug.Log ("Finished");
		FFaniManager.instance().stop(this);
		if (onFinishCallback != null) {
			onFinishCallback();
		}
	}

	virtual protected void onStart() {
		state = "playing";
		Debug.Log ("onStart");
	}
	
	// Update is called once per frame from FFaniManager
	public void updateDelta (float dt) {
		currentTime += dt;

		if (state == "ready" && currentTime > 0) {
			onStart();

			if (onStartCallback != null) {
				onStartCallback();
			}
		}

		if (state == "playing") {
			onUpdate(dt);
		}
	}
	
	public void updateTo(float time) {
		currentTime = time;
		onUpdate (0.0f);
	}
	
	virtual protected void onUpdate(float delta) {
		Debug.Log ("onUpdate");
	}
}