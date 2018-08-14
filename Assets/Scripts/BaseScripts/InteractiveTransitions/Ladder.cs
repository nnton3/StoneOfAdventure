using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	bool playerEnter = false;

	void Update () {

		if (playerEnter) {
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				MoveUp ();
				return;
			}

			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				StopMove ();
				return;
			}

			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				MoveDown ();
				return;
			}

			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				StopMove ();
				return;
			}
		}
	}

	void MoveUp () {
		player.inputY = 1;
	}

	void MoveDown () {
		player.inputY = -1;
	}

	void StopMove () {
		player.inputY = 0;
	}

	Unit player;

	void OnTriggerEnter2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			targetObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
			playerEnter = true;
			player = targetObject.GetComponent<Unit> ();
		}
	}

	void OnTriggerExit2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			targetObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
			playerEnter = false;
			player.inputY = 0;
		}
	}
}
