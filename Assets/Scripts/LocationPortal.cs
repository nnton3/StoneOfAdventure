using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPortal : MonoBehaviour {

	void Start () {
		StartCoroutine ("Destroy");
	}
	
	void Update () {
		
	}

	IEnumerator Destroy() {
		yield return new WaitForSeconds (1f);
		Destroy(gameObject);
	}
}
