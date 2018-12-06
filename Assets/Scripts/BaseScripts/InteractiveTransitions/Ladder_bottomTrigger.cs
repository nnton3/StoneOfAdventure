using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_bottomTrigger : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D player) {
		if (player.CompareTag ("Player")) {
			player.GetComponent<Player> ().ladderBottomLine = true;
		}
	}

	void OnTriggerExit2D (Collider2D player) {
		if (player.CompareTag ("Player")) {
			player.GetComponent<Player> ().ladderBottomLine = false;
		}
	}
}
