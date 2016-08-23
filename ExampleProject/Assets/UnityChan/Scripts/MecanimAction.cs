using UnityEngine;
using System.Collections;

public class MecanimAction : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		animator.Play("WAIT00");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("JUMP00"));
		Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

//		Debug.Log("" + animator.GetCurrentAnimatorStateInfo(0).length + ", " + 
//			animator.GetCurrentAnimatorStateInfo(0).normalizedTime);


		Debug.Log("isLoop: " + animator.GetCurrentAnimatorStateInfo(0).loop);

	}
}
