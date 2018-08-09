using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController: MonoBehaviour {

	static int maxActiveQuests = 3;
	static Quest[] quests = new Quest[3];
	public static int questEnumerator = 1;
	public static QuestBar questBarScript;
	public static Quest nullQuest;

	void Start () {
		FindQuestBar ();
	}

	//Получить ссылку на квест бар
	public static void FindQuestBar () {
		questBarScript = GameObject.Find ("QuestBar").GetComponent<QuestBar> ();
	}

	//Добавить квест в массив активных квестов
	public static void AddActiveQuest (Quest quest) {
		if (questEnumerator >= quests.Length) {
			questEnumerator = 1;
		}
		quests [questEnumerator] = quest;
		questBarScript.CreateQuestBarElement (quests [questEnumerator].descriptionText, quests [questEnumerator].objectiveText, quests [questEnumerator].progress, quests [questEnumerator].target, quests [questEnumerator].ID);
		questEnumerator++;
	}

	//Удалить квест из массива квестов
	public static void DeleteActiveQuest (int ID) {
		questBarScript.DeleteQuestBarElements (ID);
		for (int i = 0; i < quests.Length; i++) {
			try {
				if (quests [i].ID == ID) {
					quests [i] = null;
					return;
				}
			} catch {
				continue;
			}
		}
	}

	public static Quest FindActiveQuest (int ID) {
		foreach (Quest quest in quests) {
			try {
				if (quest.ID == ID) {
					return quest;
				}
			} catch {
				Debug.Log ("null");
				continue;
			}
		}
		return nullQuest;
	}

	public static void AddQuestProgress (int ID) {
		Quest targetQuest = FindActiveQuest (ID);
		targetQuest.progress++;
		questBarScript.EditQuestBarElement (targetQuest.descriptionText, targetQuest.objectiveText, targetQuest.progress, targetQuest.target, ID);
		if (targetQuest.progress == targetQuest.target) {
			targetQuest.objectivesComplete = true;
		}
	}
}
