using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_blacksmith : Quest {

	void Start () {
		SetReplics ();
	}

	public void AddThisQuest () {
		QuestController.AddActiveQuest (this.GetComponent<Quest>());
	}

	public void FinishQuest () {
		QuestController.DeleteActiveQuest (ID);
		QuestList.quest1_blacksmith_finish = true;
		replica3.SetActive (false);
	}



	void OnTriggerEnter2D (Collider2D targetObject) {
		//Если пришел не игрок
		/*if (!targetObject.CompareTag ("Player")) {
			//Не реагировать
			return;
		}*/
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

	void OnTriggerExit2D (Collider2D targetObject) {
		if (targetObject.CompareTag ("Player")) {
			replica1.SetActive (false);
			replica2.SetActive (false);
			replica3.SetActive (false);
		}
	}

	//Получить ссылки на диалоги
	GameObject replica1;
	GameObject replica2;
	GameObject replica3;
	void SetReplics () {
		replica1 = GameObject.Find ("Blacksmith_replica1");
		replica1.SetActive (false);
		replica2 = GameObject.Find ("Blacksmith_replica2");
		replica2.SetActive (false);
		replica3 = GameObject.Find ("Blacksmith_replica3");
		replica3.SetActive (false);
	}
}
