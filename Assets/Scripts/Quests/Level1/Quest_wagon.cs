using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_wagon : Quest, I_QuestObject
{
    public void AddProgress()
    {
        QuestController.AddQuestProgress(ID);
    }

    public GameObject indicator;

    DialogueWindow dialogueWindow;
    public string object_discription = "Среди прочего вы находите в вагонетке кусок редкой руды";
    public string player_do = "Взять";

    void Start ()
    {
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
    }

    public void StartDialogue()
    {
        dialogueWindow.Enable(true);
        dialogueWindow.CreateTextMessage(object_discription);
        dialogueWindow.CreateButton(player_do, new UnityEngine.Events.UnityAction[] { AddThisQuest, delegate { dialogueWindow.Enable(false); } });
    }

    public void AddThisQuest()
    {
        if (QuestController.FindActiveQuest(1) == null)
        {
            if (QuestController.FindActiveQuest(ID) == null)
            {
                QuestController.AddActiveQuest(GetComponent<Quest>());
            }
            Destroy(indicator);
            QuestController.AddQuestProgress(ID);
        }
        else
        {
            QuestController.AddQuestProgress(1);
            Destroy(indicator);
        }
    }
}
