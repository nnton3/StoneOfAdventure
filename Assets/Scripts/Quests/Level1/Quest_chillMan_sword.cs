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
			if (QuestController.FindActiveQuest (ID) == QuestController.nullQuest) {
				replica1.SetActive (true);
			} else {
				AddItemToChest ();
			}
		}
	}

	void OnTriggerExit2D (Collider2D player) {
		if (player.CompareTag ("Player")) {
				replica1.SetActive (false);
		}
	}
		
	public void AddThisQuest () {
		QuestController.AddActiveQuest (this.GetComponent<Quest>());
		AddItemToChest ();
	}

	void AddItemToChest () {
		QuestController.PickUpQuestItem (this.gameObject);
		GameObject itemChest = GameObject.Find ("ItemChest");
		this.transform.SetParent (itemChest.transform);
		transform.position = itemChest.transform.position;
	}
}
