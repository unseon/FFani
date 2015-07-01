﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Easing {
	public static float Linear(float t) {
		return t;
	}

	public static float InQuad(float t) {
		return t * t;
	}

	public static float OutQuad(float t) {
		t = 1.0f - t;
		return 1.0f - t * t;
	}

	public static float InOutQuad(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InQuad(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutQuad(t);
		}
	}

	public static float OutInQuad(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutQuad(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InQuad(t);
		}
	}

	public static float InCubic(float t) {
		return Mathf.Pow(t, 3);
	}

	public static float OutCubic(float t) {
		t = 1.0f - t;
		return 1.0f - Mathf.Pow(t, 3);
	}

	public static float InOutCubic(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InCubic(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutCubic(t);
		}
	}

	public static float OutInCubic(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutCubic(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InCubic(t);
		}
	}

	public static float InQuart(float t) {
		return Mathf.Pow(t, 4);
	}

	public static float OutQuart(float t) {
		t = 1.0f - t;
		return 1.0f - Mathf.Pow(t, 4);
	}

	public static float InOutQuart(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InQuart(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutQuart(t);
		}
	}
	
	public static float OutInQuart(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutQuart(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InQuart(t);
		}
	}

	public static float InQuint(float t) {
		return Mathf.Pow(t, 5);
	}
	
	public static float OutQuint(float t) {
		t = 1.0f - t;
		return 1.0f - Mathf.Pow(t, 5);
	}
	
	public static float InOutQuint(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InQuint(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutQuint(t);
		}
	}
	
	public static float OutInQuint(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutQuint(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InQuint(t);
		}
	}

	public static float InSine(float t) {
		return Mathf.Sin(t);
	}
	
	public static float OutSine(float t) {
		t = 1.0f - t;
		return 1.0f - Mathf.Sin(t);
	}
	
	public static float InOutSine(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InSine(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutSine(t);
		}
	}
	
	public static float OutInSine(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutSine(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InSine(t);
		}
	}

	public static float InExpo(float t) {
		return Mathf.Pow(2.0f, t);
	}
	
	public static float OutExpo(float t) {
		t = 1.0f - t;
		return 1.0f - Mathf.Pow(2.0f, t);
	}
	
	public static float InOutExpo(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InExpo(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutExpo(t);
		}
	}
	
	public static float OutInExpo(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutExpo(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InExpo(t);
		}
	}

	public static float InCirc(float t) {
		t = 1.0f - t;
		return 1.0f - OutCirc(t);
	}
	
	public static float OutCirc(float t) {
		t = t - 1.0f;
		return Mathf.Sqrt(1.0f - t * t);
	}
	
	public static float InOutCirc(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InCirc(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutCirc(t);
		}
	}
	
	public static float OutInCirc(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutCirc(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InCirc(t);
		}
	}

	public static float InElastic(float t) {
		float a = 5.0f;
		float p = 3.0f;
		
		float s;
		if(a < 1.0f) {
			a = 1.0f;
			s = p / 4.0f;
		} else {
			s = p / (2.0f * Mathf.PI) * Mathf.Asin(1.0f / a);
		}
		
		t -= 1.0f;
		return -(a * Mathf.Pow(2.0f, 10.0f * t) * Mathf.Sin((t - s) * (2.0f * Mathf.PI) / p ));
	}
	
	public static float OutElastic(float t) {
		t = t - 1.0f;
		return 1.0f - InElastic(t);
	}
	
	public static float InOutElastic(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InElastic(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutElastic(t);
		}
	}
	
	public static float OutInElastic(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutElastic(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InElastic(t);
		}
	}

	public static float InBack(float t) {
		float s = 5.0f;
		return (s + 1) * Mathf.Pow(t, 3) - s * t * t; 
	}

	public static float OutBack(float t) {
		float s = 5.0f;
		t = 1.0f - t;
		return 1.0f - ((s + 1) * Mathf.Pow(t, 3) - s * t * t); 
	}

	public static float InOutBack(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InBack(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutBack(t);
		}
	}
	
	public static float OutInBack(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutBack(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InBack(t);
		}
	}

	public static float InBounce(float t) {
		t = 1.0f - t;
		return 1.0f - OutBounce(t);
	}

	public static float OutBounce(float t) {
		float a = 1.0f;
		float b = 7.5625f;

		if (t < (4.0f / 11.0f)) {
			return (b * t * t);
		} else if (t < (8.0f / 11.0f)) {
			t -= (6.0f / 11.0f);
			return 1.0f - a * (1.0f - (b * t * t  + 0.75f));
		} else if (t < (10.0f / 11.0f)) {
			t -= (9.0f / 11.0f);
			return 1.0f - a * (1.0f - (b * t * t + 0.9375f));
		} else {
			t -= (21.0f / 22.0f);
			return 1.0f - a * (1.0f - (b * t * t + 0.984375f));
		}
	}

	public static float InOutBounce(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * InBounce(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * OutBounce(t);
		}
	}
	
	public static float OutInBounce(float t) {
		if (t < 0.5) {
			t = t * 2.0f;
			return 0.5f * OutBounce(t);
		} else {
			t = (t - 0.5f) * 2.0f;
			return 0.5f + 0.5f * InBounce(t);
		}
	}

}

public class FFaniMation {

//	public enum Curve {Linear, InQuad, OutQuad, InOutQuad, OutInQuad,
//		InCubic, OutCubic, InOutCubic, OutInCubic, InQuart,
//		OutQuart, InOutQuart, OutInQuart, InQuint, OutQuint,
//		InOutQuint, OutInQuint, InSine, OutSine, InOutSine,
//		OutInSine, InExpo, OutExpo, InOutExpo, OutInExpo,
//		InCirc, OutCirc, InOutCirc, OutInCirc, InElastic,
//		OutElastic, InOutElastic, OutInElastic, InBack, OutBack,
//		IntOutBack, OutInBack, InBounce, OutBounce, InOutBounce,
//		OutInBounce};

	public delegate float EasingCurve(float t);

	public EasingCurve easingFunction = Easing.Linear;

	public FFani.Callback onStarted = null;
	public FFani.Callback onFinished = null;
	public FFani.Callback onStopped = null;

	public float currentTime = 0.0f;
	public float currentEasingTime = 0.0f;
	public float delayTime = 0.0f;
	public float duration = 0.0f;

	public string state = "ready";


	// Use this for initialization
	public void Start () {
		if (delayTime > 0.0f) {
			currentTime = - delayTime;
			FFaniManager.Instance().Play(this);
		} else {
			currentTime = 0.0f;
			Init();
			FFaniManager.Instance().Play(this);

			if (onStarted != null) {
				onStarted();
			}
		}
	}

	public void Reset() {
		if (delayTime > 0.0f) {
			currentTime = - delayTime;
		} else {
			currentTime = 0.0f;
			Init();
			
			if (onStarted != null) {
				onStarted();
			}
		}
	}

	public void Stop() {
		//Debug.Log ("Stopped");
		FFaniManager.Instance().Stop(this);
		state = "stop";
		if (onStopped != null) {
			onStopped();
		}
	}

	protected void Finish() {
		//Debug.Log ("Finished");
		FFaniManager.Instance().Stop(this);
		state = "finished";
		if (onFinished != null) {
			onFinished();
		}
	}

	public FFaniMation Remind(FFani.Callback onFinished, 
	                          FFani.Callback onStarted = null,
	                          FFani.Callback onStopped = null) {
		this.onFinished += onFinished;
		this.onStarted += onStarted;
		this.onStopped += onStopped;

		return this;
	}

	virtual protected void Init() {
		state = "playing";
		//Debug.Log ("onStart");
	}
	
	// Update is called once per frame from FFaniManager
	public void UpdateDelta (float dt) {
		currentTime += dt;

		if (state == "ready" && currentTime > 0) {
			Init();

			if (onStarted != null) {
				onStarted();
			}
		}

		if (state == "playing") {

			OnUpdate(dt);
		}
	}
	
	public void UpdateTo(float time) {
		currentTime = time;
		OnUpdate (0.0f);
	}
	
	virtual protected void OnUpdate(float delta) {
		Debug.Log ("onUpdate");
	}
}