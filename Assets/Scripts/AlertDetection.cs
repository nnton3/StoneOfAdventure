using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertDetection : MonoBehaviour {

	[HideInInspector]
	public Collider2D AlertTrigger;

	void Start () {
		AlertTrigger = null;
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("AlertTrigger")) {
			AlertTrigger = other;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("AlertTrigger")) {
			AlertTrigger = null;
		}
	}
}
