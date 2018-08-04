using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

	public string descriptionText;
	public string objectiveText;
	public int questID = 0;
	public int progress = 0;
	public int target = 0;
	public bool questComplete = false;

	public void CheckProgress () {

	}

	public void GetQuestID(int ID) {
		questID = ID;
	}
}
