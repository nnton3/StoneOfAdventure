﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*! 
\brief Класс объекта "лестница"

	   Отслеживает проникновение игрока в триггер "лестница", в котором он может двигаться по вертикали
*/
public class Ladder : MonoBehaviour {

	bool playerEnter = false;   ///< Игрок находится в триггере "лестница" или нет
	Player player;   ///< Ссылка на объект "игрок"

	void Start () {
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	void Update () {
		if (playerEnter && player.inputY != 0f) {
			player.onLadder = true;
			player.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
		}
	}

	void OnTriggerEnter2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			playerEnter = true;
		}
	}

	void OnTriggerExit2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			targetObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
			playerEnter = false;
			player.onLadder = false;
		}
	}
}
