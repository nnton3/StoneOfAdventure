using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour {

	public static bool questBarIsFull = false;
	public int maxActiveQuests = 3;
	Quest[] quests;

	//Добавить квест в массив активных квестов
	public void AddActiveQuest (Quest quest) {
		for (int i = 0; i >= (maxActiveQuests - 1); i++) {
			if (quests [i] != null) {
				quests [i] = quest;
				CheckQuestBarLength (i);
			} else
				Debug.Log ("position of " + i + " place occupied");
		}
	}

	public void DeleteActiveQuest (Quest quest) {
		int count = 0;
		foreach (Quest currentQuest in quests) {
			if (currentQuest == quest) {
				currentQuest = null;
			}
		}
	}

	//Проверка свободных мест в массиве активных квестов
	void CheckQuestBarLength (int currentLenghtOfActiveQuests) {
		if (currentLenghtOfActiveQuests >= (maxActiveQuests - 1)) {
			questBarIsFull = true;
		} else
			questBarIsFull = false;
	}
}
