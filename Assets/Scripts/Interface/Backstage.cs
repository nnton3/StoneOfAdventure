using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backstage : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void StartAnimation () {
		anim.SetTrigger ("startTransition");
	}
}
