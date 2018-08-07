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

	public GameObject mainBG;
	public GameObject sideBG;
	bool mainBGisActive = true;

	public void Transition () {
		if (mainBGisActive) {
			sideBG.SetActive (true);
			mainBG.SetActive (false);
			mainBGisActive = false;
		} else {
			mainBG.SetActive (true);
			sideBG.SetActive (false);
			mainBGisActive = true;
		}
	}
}
