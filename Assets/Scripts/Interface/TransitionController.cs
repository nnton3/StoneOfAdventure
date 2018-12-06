using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

	static GameObject mainBG;
	static GameObject sideBG;

	void Start () {
		mainBG = GameObject.Find ("MainBG");
		backstage = GameObject.Find ("Backstage");
		EnableBackstage (false);
	}

	//Осуществить переход
	static bool mainBGisActive = true;
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

	//Запомнить побочный уровень, на который будет идти переход
	public void SetSideBG (GameObject inputSideBG) {
		sideBG = inputSideBG;
	}

	//Включить затемнение экрана при переходе
	GameObject backstage;
	public void EnableBackstage (bool enable) {
		backstage.SetActive (enable);
		backstage.GetComponent<Backstage> ().StartAnimation ();
	}
}
