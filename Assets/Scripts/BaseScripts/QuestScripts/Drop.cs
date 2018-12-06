using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

	Conditions conditionsScript;

	void Start () {
		conditionsScript = GetComponent<Conditions> ();
	}
	
	void Update () {
		if (!objectsDropped && !conditionsScript.alive) {
			DroppedObjects ();
		}
	}

	bool objectsDropped = false;

	public GameObject[] avaliablaObjects;
	void DroppedObjects () {
		objectsDropped = true;
		for (int i = 0; i < avaliablaObjects.Length; i++) {
			Instantiate (avaliablaObjects [i], transform);
		}
	}
}
