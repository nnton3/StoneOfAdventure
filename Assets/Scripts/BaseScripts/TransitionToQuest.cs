﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToQuest : MonoBehaviour {

	public GameObject button;
	public GameObject sideBG;

	void OnTriggerEnter2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			button.SetActive (true);
		}
	}

	void OnTriggerExit2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			button.SetActive (false);
		}
	}

	public void GetSideBG () {
		TransitionController.SetSideBG (sideBG);
	}

}
