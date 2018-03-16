using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndWindow : MonoBehaviour {

	public GameObject endwindow;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator EnableEndWindow() {
		Time.timeScale = 0.001f;
		endwindow.SetActive (true);
		yield return new WaitForSeconds (Time.timeScale * 3f);
		endwindow.SetActive (false);
		Time.timeScale = 1f;
	}

	public void GameEnd() {
		StartCoroutine ("EnableEndWindow");
	}
}
