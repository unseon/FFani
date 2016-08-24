using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class FFaniEasing {
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
		return Mathf.Pow(2.0f, 10.0f * (t - 1.0f)) - 0.001f;
	}
	
	public static float OutExpo(float t) {
		return - Mathf.Pow(2.0f, -10.0f * t) + 1;
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

	public EasingCurve easingCurve = FFaniEasing.Linear;

	public FFani.Callback onStarted = null;
	public FFani.Callback onCompleted = null;
	public FFani.Callback onStopped = null;
	public FFani.Callback instantCallback = null;

	public float currentTime = 0.0f;
	public float currentEasingTime = 0.0f;
	public float delay = 0.0f;
	public float duration = 0.0f;

	public string state = "ready";

	public bool isDebug = false;


	// Use this for initialization
	public void Start () {
		if (delay > 0.0f) {
			state = "delaying";
			currentTime = - delay;
			FFaniManager.Instance().Play(this);
		} else {
			currentTime = 0.0f;
			Init();
			Update(0.0f);

			FFaniManager.Instance().Play(this);

			if (onStarted != null) {
				onStarted();
			}
		}
	}

	public void Start (FFani.Callback stopped) {
		instantCallback += stopped;
		Start();
	}

	public void Reset() {
		if (delay > 0.0f) {
			state = "delaying";
			currentTime = - delay;
		} else {
			currentTime = 0.0f;
			Init();

			if (onStarted != null) {
				onStarted();
			}

			Update(0.0f);
		}
	}

	public void Stop() {
		//Debug.Log ("Stopped");
		//FFaniManager.Instance().Stop(this);
		state = "stopped";

		if (instantCallback != null) {
			instantCallback();
			instantCallback = null;
		}

		if (onStopped != null) {
			onStopped();
		}
	}

	virtual public void Complete() {
		Debug.Log ("completed");

		if (state == "completed") {
			return;
		}
        
        if (state == "ready" || state == "delaying") {
            Init();
            
            if (onStarted != null) {
                //anim.state == "playing"
                onStarted();
            }
        }

		state = "completed";

		currentTime = duration;
		OnUpdatePlay (0.0f);

		if (instantCallback != null) {
			instantCallback();
			instantCallback = null;
		}

		if (onCompleted != null) {
			onCompleted();
		}
	}

	public FFaniMation Remind(FFani.Callback onCompleted, 
	                          FFani.Callback onStarted = null,
	                          FFani.Callback onStopped = null) {
		this.onCompleted += onCompleted;
		this.onStarted += onStarted;
		this.onStopped += onStopped;

		return this;
	}

	virtual public void Init() {
		state = "playing";
		//Debug.Log ("onStart");
	}
	
	// Update is called once per frame from FFaniManager or FFaniGroupAnimation
	public void Update (float dt) {
		currentTime += dt;


		if (isDebug) {
			if (Mathf.Abs(currentTime - dt) < 0.0001 && dt != 0) {

				Debug.Log (this.GetType() + " currentTime: " + currentTime + ", dt: " + dt + " duration: " + duration);
			}
		}

		if (state == "delaying" && currentTime > 0) {
			Init();

			if (onStarted != null) {
				onStarted();
			}
		}

		if (state == "playing") {
			OnUpdatePlay(dt);
			if (duration != -1 && currentTime >= duration) {
				state = "completed";
			}

			if (state == "completed") {
				if (instantCallback != null) {
					instantCallback();
					instantCallback = null;
				}

				if (onCompleted != null) {
					onCompleted();
				}
			}
		}
	}
	
	public void UpdateTo(float time) {
		currentTime = time;
		OnUpdatePlay (0.0f);
	}
	
	virtual protected void OnUpdatePlay(float delta) {
		//Debug.Log ("onUpdate");
	}

	virtual public FFaniMation Cloned() {
		FFaniMation anim = new FFaniMation();
		CopyTo(anim);
		return anim;
	}

	virtual protected void CopyTo(FFaniMation target) {
		target.easingCurve = easingCurve;
		target.onStarted += onStarted;
		target.onCompleted += onCompleted;
		target.onStopped += onStopped;

		target.delay = delay;
		target.duration = duration;
	}
}