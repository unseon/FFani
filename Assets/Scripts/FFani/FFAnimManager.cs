using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFAnimManager : MonoBehaviour {

	static private FFAnimManager _instance = null;
	static private GameObject _gameObject = null;

	// get the singleton instance
	static public FFAnimManager instance() {
		if (_instance == null) {
			_gameObject = new GameObject("FFAnimManager");
			_instance = _gameObject.AddComponent<FFAnimManager>();
		}
		
		return _instance;
	}
	
	private List<FFBaseAnimation> animList = new List<FFBaseAnimation>();
	
	public bool isPlaying = false;
	
	public void play(FFBaseAnimation anim) {
		animList.Add(anim);
		
		start ();
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
