using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest_chillMan : Quest
{
    DialogueWindow dialogueWindow;
    private void Start()
    {
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
    }

    public GameObject indicator;
    public string hello_replic = "Приветствую тебя славный воин, помоги мне вернуть мой меч. Его забрал мерзкий зомби со щитом";
    public string player_hello = "Здравствуй, хорошо я помогу тебе";
    public string not_complete_replic = "И где мой меч парень?";
    public string player_not_complete_answer = "Ща все буит паринь";
    public string quest_complete = "О, спасибо";
    public string player_quest_complete = "Да не за что";
    public string player_have_sword = "Отдать меч";
    public void StartDialogue()
    {
        if (!QuestComplete())
        {
            dialogueWindow.Enable(true);
            Debug.Log(QuestController.FindActiveQuest(ID));
            if (QuestInProgress())
            {
                Debug.Log("Квест в процессе выполнения");
                if (QuestController.CheckProgress(ID))
                {
                    Debug.Log("Квест таки выполнен");
                    TakeSword();
                    QuestController.DeleteActiveQuest(ID);
                }
                else
                {
                    Debug.Log("Квест не выполнен");
                    dialogueWindow.CreateTextMessage(not_complete_replic);
                    dialogueWindow.CreateButton(player_not_complete_answer, new UnityAction[] { delegate { dialogueWindow.Enable(false); } });
                }
            }
            else
            {
                Debug.Log("work");
                dialogueWindow.CreateTextMessage(hello_replic);
                if (QuestController.FindActiveQuest(3) == null)
                {
                    dialogueWindow.CreateButton(player_hello, new UnityAction[] { AddThisQuest, delegate { dialogueWindow.Enable(false); } });
                }
                else
                    dialogueWindow.CreateButton(player_have_sword, new UnityAction[] { TakeSword, delegate { QuestController.DeleteActiveQuest(3); } });
            }
        }
    }

    bool QuestComplete ()
    {
        return QuestList.quest2_chill_man;
    }

    bool QuestInProgress ()
    {
        return QuestController.FindActiveQuest(ID) != null;
    }
   
    public void AddThisQuest()
    {
        QuestController.AddActiveQuest(GetComponent<Quest>());
    }

    void EndQuest()
    {
        Destroy(indicator);
        objectivesComplete = true;
        QuestList.quest4_little_girl = true;
    }

    void TakeSword ()
    {
        dialogueWindow.CreateTextMessage(quest_complete);
        dialogueWindow.CreateButton(player_quest_complete, new UnityAction[] { delegate { dialogueWindow.Enable(false); } });
        QuestList.quest2_chill_man = true;
        QuestList.quest3_chill_man_sword = true;
        Destroy(indicator.gameObject);
    }
}