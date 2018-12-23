using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_chillMan_sword : Quest, I_QuestObject {

    public void AddProgress()
    {
        QuestController.AddQuestProgress(ID);
    }

    public GameObject indicator;
    void Start () {
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
        if (QuestController.FindActiveQuest(ID) == null)
        indicator.SetActive(true);
    }

    DialogueWindow dialogueWindow;
    public string object_discription = "Вы видит меч, сталь переливается в лучах лунного света, выглядит завораживающе";
    public string player_do = "Взять";
    public void StartDialogue()
    {
        if (QuestController.FindActiveQuest(ID) == null)
        {
            dialogueWindow.Enable(true);
            dialogueWindow.CreateTextMessage(object_discription);
            dialogueWindow.CreateButton(player_do, new UnityEngine.Events.UnityAction[] { AddThisQuest, delegate { dialogueWindow.Enable(false); } });
        }
    }

    public void AddThisQuest () {
        Debug.Log("work");
		QuestController.AddActiveQuest (GetComponent<Quest>());
        transform.position = new Vector3(-100, 100, 0);
  	}
}
