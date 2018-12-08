using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBar: MonoBehaviour {

	QuestBarElements[] questBarElements = new QuestBarElements[3];

	//Создать описание квеста в QuestBar
	public GameObject questBarElement;

	public void CreateQuestBarElement (string questInfo, string objectives, int currentProgress, int targetProgress, int ID) {
		GameObject questBarElementInstance = Instantiate (questBarElement, new Vector3 (0f, 0f, 0f), Quaternion.identity);
		QuestBarElements questBarElementScript = questBarElementInstance.GetComponent<QuestBarElements> ();
		questBarElementInstance.transform.SetParent (this.transform);
		questBarElements [QuestController.questEnumerator] = questBarElementScript;
		questBarElementScript.EditQuestDescription (questInfo, objectives, currentProgress, targetProgress);
		questBarElementScript.SaveQuestID (ID);
	}

	public void EditQuestBarElement (string questInfo, string objectives, int currentProgress, int targetProgress, int ID) {
		FindQuestBarElement (ID).EditQuestDescription (questInfo, objectives, currentProgress, targetProgress);
	}

	//Получить ссылку на нужный квест
	public QuestBarElements FindQuestBarElement (int ID) {
		QuestBarElements searchQuestBarElements;
		for (int i = 0; i < questBarElements.Length; i++) {
			if (questBarElements [i] != null) {
				if (questBarElements [i].ID == ID) {
					searchQuestBarElements = questBarElements [i];
					return searchQuestBarElements;
				}
			}
		}
        return null;
	}

	//Удалить квест из квест-бара
	public void DeleteQuestBarElements (int ID) {
		Destroy (FindQuestBarElement (ID).gameObject);
	}
}
