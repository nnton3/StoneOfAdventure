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
	public static void DeleteActiveQuest (Quest quest) {
		int count = 0;
		foreach (Quest currentQuest in quests) {
			if (currentQuest == quest) {
			}
		}
	}

	public static Quest FindActiveQuest (int ID) {
		for (int i = 0; i < quests.Length; i++) {
			if (quests [i] != null) {
				Debug.Log ("квест не равен нулю");
				if (quests [i].ID == ID) {
					Debug.Log ("найден квест с таким же ID");
					return quests [i];
				}
			}
		}
		Debug.Log ("квест не найден нулю");
		return nullQuest;
	}

}
