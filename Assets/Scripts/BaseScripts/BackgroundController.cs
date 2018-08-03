using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	GameObject mainBG;

	void Start () {
		mainBG = GameObject.Find ("MainBackground");
	}
	
	void Update () {
		
	}

	void EnableQuestBG (GameObject questBG) {
		mainBG.SetActive (false);
		questBG.SetActive (true);
	}
}
