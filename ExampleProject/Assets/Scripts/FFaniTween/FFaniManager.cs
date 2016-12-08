using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniManager : MonoBehaviour {

	static private FFaniManager _instance = null;
	static private GameObject _gameObject = null;

	// get the singleton instance
	static public FFaniManager Instance() {
		if (_instance == null) {
			_gameObject = new GameObject("FFaniManager");
			_instance = _gameObject.AddComponent<FFaniManager>();
		}
		
		return _instance;
	}
	
	private List<FFaniMation> animList = new List<FFaniMation>();
	
	public bool isPlaying = false;
	
	public void Play(FFaniMation anim) {
		animList.Add(anim);

		isPlaying = true;

//		if (!isPlaying) {
//			StartCoroutine("Activate");
//		}
	}

	void Update() {
		if (isPlaying == true) {
			Tick ();

			if (animList.Count == 0) {
				isPlaying = false;
			}
		}
	}

	public void Stop(FFaniMation anim) {
		animList.Remove(anim);
	}

	private IEnumerator Activate() {
		isPlaying = true;
		while(animList.Count > 0) {
			Tick();
			yield return null;
			Debug.Log("AnimList Count : " + animList.Count);
		}
		
		isPlaying = false;
//		Debug.Log ("FFaniManager Stopped");
	}
	
	private void Tick() {
		float dt = Time.deltaTime;

		for(int i = 0; i < animList.Count; i++) {
			FFaniMation anim = animList[i];
			anim.Update(dt);

			if (anim.state == "completed" || anim.state == "stopped") {
				Stop (anim);
			}
		}
	}
}