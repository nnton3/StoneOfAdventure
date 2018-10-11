using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_chillMan : Quest {

	void Start () {
		SetReplics ();
	}

	void Update () {
		CheckProgress ();
	}

	void OnTriggerEnter2D (Collider2D targetObject) {
		//Если пришел не игрок
		if (!targetObject.CompareTag ("Player")) {
			//Не реагировать
			return;
		}
		//Если у него нет этого квеста
		if (QuestController.FindActiveQuest (ID) == QuestController.nullQuest) {
			//Если этот квест еще не выполнялся
			if (!QuestList.quest2_chill_man) {
				replica1.SetActive (true);
			}
			//Если у него есть этот квест
		} else {
			//Если цели выполнены
			if (QuestController.FindActiveQuest (ID).objectivesComplete) {
				replica4.SetActive (true);
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
	GameObject replica4;
	void SetReplics () {
		replica1 = GameObject.Find ("ChillMan_replica1");
		replica1.SetActive (false);
		replica2 = GameObject.Find ("ChillMan_replica2");
		replica2.SetActive (false);
		replica3 = GameObject.Find ("ChillMan_replica3");
		replica3.SetActive (false);
		replica4 = GameObject.Find ("ChillMan_replica4");
		replica4.SetActive (false);
	}

	public List<Conditions> zombieList;
	bool CheckProgress () {
		int currentProgress = 0;
		foreach (Conditions zombie in zombieList) {
			if (!zombie.alive) {
				currentProgress++;
			}
		}
		progress = currentProgress;
		if (target == progress) {
			QuestController.AddQuestProgress (ID);
			return true;
		} else
			return false;
	}

	public void AddThisQuest () {
		QuestController.AddActiveQuest (this.GetComponent<Quest>());
	}
}