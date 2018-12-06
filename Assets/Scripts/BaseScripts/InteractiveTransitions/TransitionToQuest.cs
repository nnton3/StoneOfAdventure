using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionToQuest : MonoBehaviour {

	TransitionController transControl;
	public Text textInSign;

	void Start () {
		transControl = GameObject.Find ("TransitionController").GetComponent<TransitionController> ();
	}

	//Включить/выключить кнопку если игрок вошел в триггер
	public GameObject button;
	void OnTriggerEnter2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			button.SetActive (true);
		}
	}
	void OnTriggerExit2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			button.SetActive (false);
		}
	}

	//Передать ссылку на побочный уровень
	[HideInInspector]
	public GameObject sideBG;
	public void GoToSideBG () {
		transControl.SetSideBG (sideBG);
		transControl.EnableBackstage (true);
	}
}
