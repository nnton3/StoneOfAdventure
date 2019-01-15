using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_chillMan_sword : Quest, I_QuestObject {

    public void AddProgress()
    {
        QuestController.AddQuestProgress(ID);
    }

    public GameObject indicator;
    void Start() {
        if (transform.lossyScale.x < 0f)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
        }
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
        indicator.SetActive(true);
    }

    DialogueWindow dialogueWindow;
    public string object_discription = "Вы видит меч, сталь переливается в лучах лунного света, выглядит завораживающе";
    public string player_do = "Взять";
    public void StartDialogue()
    {
        dialogueWindow.Enable(true);
        dialogueWindow.CreateTextMessage(object_discription);
        dialogueWindow.CreateButton(player_do, new UnityEngine.Events.UnityAction[] { AddThisQuest, delegate { dialogueWindow.Enable(false); } });
    }

    public void AddThisQuest () {
        if (QuestController.FindActiveQuest(2) == null)
        {
            QuestController.AddActiveQuest(GetComponent<Quest>());
        }
        else
        {
            QuestController.AddQuestProgress(2);
        }
        transform.position = new Vector3(-100, 100, 0);
    }
}
