using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backstage : MonoBehaviour {

	Animator anim;

	void Awake () {
		anim = GetComponent<Animator> ();
	}

	public void StartAnimation () {
		anim.SetTrigger ("startTransition");
	}

	public void StartTransition () {
		TransitionController.Transition ();
	}
}
