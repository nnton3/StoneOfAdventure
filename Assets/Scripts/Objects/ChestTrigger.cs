using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour {

	public GameObject replica;
	public Chest chestScript;
	Player playerScript;

	void Start () {
		replica = GameObject.Find ("ChestOpen_replica");
		replica.SetActive (false);
		playerScript = GameObject.Find ("Player").GetComponent<Player> ();
	}
		
	public bool playerNear = false;
	void Update () {
		if (playerNear && !chestScript.chestOpened) {
			GoOpen ();
		}
	}

	void OnTriggerEnter2D (Collider2D player) {
		if (player.CompareTag ("Player")) {
			if (!chestScript.chestOpened) {
				replica.SetActive (true);
			}
		}
	}

	void OnTriggerExit2D (Collider2D player) {
		if (player.CompareTag ("Player")) {
			replica.SetActive (false);
		}
	}

	public void EnablePlayerNear () {
		replica.SetActive (false);
		playerNear = true;
		playerScript.conditions.stun = true;
	}

	void GoOpen () {
		if (Mathf.Abs(transform.position.x - playerScript.transform.position.x) > 0.1f) {
			playerScript.inputX = (transform.position.x > playerScript.transform.position.x) ? 1 : -1;
		} else {
			playerScript.inputX = 0;
			chestScript.OpenChest ();
			playerScript.anim.SetTrigger ("open_chest");
		}
	}
}
