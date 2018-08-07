using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_blacksmith : Quest {

	public void AddThisQuest () {
		QuestController.AddActiveQuest (this.GetComponent<Quest>());
	}

}
