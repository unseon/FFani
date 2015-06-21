using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFaniManager : MonoBehaviour {

	static private FFaniManager _instance = null;
	static private GameObject _gameObject = null;

	// get the singleton instance
	static public FFaniManager instance() {
		if (_instance == null) {
			_gameObject = new GameObject("FFaniManager");
			_instance = _gameObject.AddComponent<FFaniManager>();
		}
		
		return _instance;
	}
	
	private List<FFaniAnimation> animList = new List<FFaniAnimation>();
	
	public bool isPlaying = false;
	
	public void play(FFaniAnimation anim) {
		animList.Add(anim);
		
		start ();
	}

	public void delayedPlay(FFaniAnimation anim, float delayTime) {
	}
	
	public void stop(FFaniAnimation anim) {
		animList.Remove(anim);
	}
	
	public void start() {
		StartCoroutine("activate");
	}
	
	private IEnumerator activate() {
		isPlaying = true;
		while(true) {
			update();
			yield return null;
		}
		
		isPlaying = false;
	}
	
	private void update() {
		float dt = Time.deltaTime;
		for(int i = 0; i < animList.Count; i++) {
			animList[i].updateDelta(dt);
		}
	}
}