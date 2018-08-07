using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_wagon : MonoBehaviour {

	public GameObject replica1;
	int ID = 1;

	void OnTriggerEnter2D (Collider2D targetObject) {
		//Если пришел игрок
		if (targetObject.CompareTag("Player")) {
			//Если у него есть квест от кузнеца
			if (QuestController.FindActiveQuest(ID) != QuestController.nullQuest) {
				if (QuestController.FindActiveQuest (ID).progress != QuestController.FindActiveQuest (ID).target) {
					replica1.SetActive (true);
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D targetObject) { 
		if (targetObject.CompareTag ("Player")) {
			replica1.SetActive (false);
		}
	}
}
