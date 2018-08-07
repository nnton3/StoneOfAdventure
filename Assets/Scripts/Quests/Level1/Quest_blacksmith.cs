using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_blacksmith : Quest {

	public void AddThisQuest () {
		QuestController.AddActiveQuest (this.GetComponent<Quest>());
	}

	public void FinishQuest () {
		QuestController.DeleteActiveQuest (ID);
		QuestList.quest1_blacksmith_finish = true;
		replica3.SetActive (false);
	}

	public GameObject replica1;
	public GameObject replica2;
	public GameObject replica3;

	void OnTriggerEnter2D (Collider2D targetObject) {
		//Если пришел игрок
		if (targetObject.CompareTag ("Player")) {
			//Если у него нет этого квеста
			if (QuestController.FindActiveQuest (ID) == QuestController.nullQuest) {
				//Если этот квест еще не выполнялся
				if (!QuestList.quest1_blacksmith_finish) {
					replica1.SetActive (true);
				}
			//Если у него есть этот квест
			} else {
				//Если цели выполнены
				if (QuestController.FindActiveQuest (ID).objectivesComplete) {
					replica3.SetActive (true);
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			replica1.SetActive (false);
			replica2.SetActive (false);
			replica3.SetActive (false);
		}
	}
}
