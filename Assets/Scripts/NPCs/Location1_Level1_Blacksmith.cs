using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location1_Level1_Blacksmith : MonoBehaviour {

	public GameObject replica1;
	public GameObject replica2;
	public GameObject replica3;

	void OnTriggerEnter2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			replica1.SetActive (true);
		}
	}

	void OnTriggerExit2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			replica1.SetActive (false);
			replica2.SetActive (false);
			replica3.SetActive (false);
		}
	}
}
