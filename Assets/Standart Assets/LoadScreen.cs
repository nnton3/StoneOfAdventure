using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreen : MonoBehaviour {

	public GameObject LoadScreenOb;

	public void EnableLoadScreenOb() {
		StartCoroutine ("Enable");
	}

	IEnumerator Enable() {
		Time.timeScale = 0.001f;
		LoadScreenOb.SetActive (true);
		yield return new WaitForSeconds (Time.timeScale * 3f);
		Time.timeScale = 1f;
		yield return new WaitForSeconds (1.5f);
		LoadScreenOb.SetActive (false);
	}
}
