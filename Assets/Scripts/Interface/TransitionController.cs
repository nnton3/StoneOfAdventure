using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

	public GameObject mainBG;
	public GameObject sideBG;
	static bool mainBGisActive = true;

	public static void Transition (GameObject mainBG, GameObject sideBG) {
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
}
