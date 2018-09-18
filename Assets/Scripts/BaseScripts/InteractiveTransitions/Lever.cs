using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*! 
\brief Класс объекта "рычаг"
*/
public class Lever : MonoBehaviour {

	GameObject canvas;   ///< Ссылка на GUI
	Animator anim;   ///< Ссылка на Animator

	void Start () {
		canvas = transform.Find ("Canvas").gameObject;
		anim = GetComponent<Animator> ();
	}
	///Включает GUI для активации рычага
	void OnTriggerEnter2D (Collider2D unit) {
		if (unit.CompareTag ("Player")) {
			canvas.SetActive (true);
		}
	}
	///Выключает GUI для активации рычага
	void OnTriggerExit2D (Collider2D unit) {
		if (unit.CompareTag ("Player")) {
			canvas.SetActive (false);
		}
	}

	///Что происходит при актиыации рычага
	public void UseLever () {
		anim.SetTrigger ("use");
	}
}
