using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	bool playerEnter = false;
	Player player;

	void Start () {
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	void Update () {
		if (playerEnter && Input.GetAxisRaw("Vertical") != 0f) {
			player.onLadder = true;
		}
	}

	void OnTriggerEnter2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			targetObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
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
