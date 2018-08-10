using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

	static GameObject mainBG;
	static GameObject sideBG;
	static bool mainBGisActive = true;
	GameObject backstage;

	void Start () {
		mainBG = GameObject.Find ("MainBackground");
		backstage = GameObject.Find ("Backstage");
		EnableBackstage (false);
	}

	public static void Transition () {
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

	public static void SetSideBG (GameObject inputSideBG) {
		sideBG = inputSideBG;
	}

	public void EnableBackstage (bool enable) {
		backstage.SetActive (enable);
	}
}
