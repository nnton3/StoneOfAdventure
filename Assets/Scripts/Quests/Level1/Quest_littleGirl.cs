using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_littleGirl : Quest {

	void Start () {
		SetReplics ();
	}

	void OnTriggerEnter2D (Collider2D targetObject) {
		//Если пришел не игрок
		if (!targetObject.CompareTag ("Player")) {
			//Не реагировать
			return;
		}

		if (!objectivesComplete && CheckProgress ()) {
			objectivesComplete = true;
			QuestList.quest4_little_girl = true;
			replica2.SetActive (true);
		}

		if (!QuestList.quest4_little_girl) {
			replica1.SetActive (true);
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
		replica1 = GameObject.Find ("LittleGirl_replica1");
		replica1.SetActive (false);
		replica2 = GameObject.Find ("LittleGirl_replica2");
		replica2.SetActive (false);
		replica3 = GameObject.Find ("LittleGirl_replica3");
		replica3.SetActive (false);
	}

	public List<Conditions> zombieList;
	bool CheckProgress () {
		progress = 0;
		foreach (Conditions zombie in zombieList) {
			if (!zombie.alive) {
				progress++;
			}
		}
		if (target == progress) {
			return true;
		} else
			return false;
	}
}
