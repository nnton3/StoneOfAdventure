using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	GameObject canvas;
	Animator anim;

	void Start () {
		canvas = transform.Find ("Canvas").gameObject;
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D unit) {
		if (unit.CompareTag ("Player")) {
			canvas.SetActive (true);
		}
	}

	void OnTriggerExit2D (Collider2D unit) {
		if (unit.CompareTag ("Player")) {
			canvas.SetActive (false);
		}
	}

	//Агимация рычага
	public void UseLever () {
		anim.SetTrigger ("use");
	}
}
