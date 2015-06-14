using UnityEngine;
using System.Collections;

public class FFaniAnimation {

	public float currentTime = 0.0f;
	// Use this for initialization
	public void start () {
		currentTime = 0.0f;
		onStart();
		FFaniManager.instance().play(this);
	}
	
	virtual protected void onStart() {
		Debug.Log ("onStart");
	}
	
	// Update is called once per frame
	public void updateDelta (float dt) {
		currentTime += dt;
		onUpdate(dt);
	}
	
	public void updateTo(float time) {
		currentTime = time;
		onUpdate (0.0f);
	}
	
	virtual protected void onUpdate(float delta) {
		Debug.Log ("onUpdate");
	}
}