using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {

	public int ID;
	Quest quest;

	void Start () {
		quest = GetComponent<Quest> ();
		quest.enabled = false;
	}

	void OnTriggerEnter2D (Collider2D player) {
		if (player.CompareTag ("Player")) {
			if (QuestController.FindActiveQuest (ID) != QuestController.nullQuest) {
				QuestController.AddQuestProgress (ID);
			} else {
				quest.enabled = true;
			}
		}
	}
}
