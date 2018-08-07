using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBarElements : MonoBehaviour {

	Text questDiscription;
	public int ID = 0;

	void Update () {

	}

	public void EditQuestBarElement (string questInfo, string objectives, int currentProgress, int targetProgress) {
		questDiscription = GetComponent<Text> ();
		questDiscription.text = questInfo + "\n" + objectives + " " + currentProgress + " of " + targetProgress;
	}

	public void DeleteQuestDiscription () {
		Destroy (gameObject);
	}

	public void SaveQuestID (int id) {
		ID = id;
	}
}
