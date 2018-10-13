using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_chillMan_sword : Quest {

	void Start () {
		if (this.transform.lossyScale.x < 0) {
			Vector3 currentScale;
			currentScale = this.transform.localScale;
			currentScale.x *= -1f;
			this.transform.localScale = currentScale;
		}
	}

	public GameObject replica1;
	void OnTriggerEnter2D (Collider2D player) {
		if (player.CompareTag ("Player")) {
			replica1.SetActive (true);
		}
	}

	void OnTriggerExit2D (Collider2D player) {
		if (player.CompareTag ("Player")) {
			replica1.SetActive (false);
		}
	}
		
	public void AddThisQuest () {
		QuestController.AddActiveQuest (this.GetComponent<Quest>());
		QuestController.AddQuestProgress (ID);
		Destroy (this.gameObject);
	}
}
