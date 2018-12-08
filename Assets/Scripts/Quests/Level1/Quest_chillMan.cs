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
    public void StartDialogue()
    {
        if (!QuestComplete())
        {
            dialogueWindow.Enable(true);
            if (QuestInProgress())
            {
                if (CheckProgress())
                {
                    dialogueWindow.CreateTextMessage(not_complete_replic);
                    dialogueWindow.CreateButton(player_not_complete_answer, new UnityAction[] { delegate { dialogueWindow.Enable(false); } });
                }
            }
        }
        else
        {
            dialogueWindow.CreateTextMessage(hello_replic);
            dialogueWindow.CreateButton(player_hello, new UnityAction[] { AddThisQuest, delegate { dialogueWindow.Enable(false); } });
        }
    }

    bool QuestComplete ()
    {
        return QuestList.quest2_chill_man;
    }

    bool QuestInProgress ()
    {
        return QuestController.FindActiveQuest(ID) == QuestController.nullQuest;
    }
    /*void OnTriggerEnter2D (Collider2D targetObject) {
        //Если пришел не игрок
        if (!targetObject.CompareTag ("Player")) {
            //Не реагировать
            return;
        }
        //Если у него нет этого квеста
        if (QuestController.FindActiveQuest (ID) == QuestController.nullQuest) {
            //Если этот квест еще не выполнялся
            if (!QuestList.quest2_chill_man) {
                replica1.SetActive (true);
            }
            //Если у него есть этот квест
        } else {
            //Если цели выполнены
            if (QuestController.FindActiveQuest (ID).objectivesComplete || QuestController.FindQuestItem (ID, target)) {
                replica4.SetActive (true);
            }
        }
    }

    void OnTriggerExit2D (Collider2D targetObject) {
        if (targetObject.CompareTag ("Player")) {
            replica1.SetActive (false);
            replica2.SetActive (false);
            replica3.SetActive (false);
        }
    }

    //Получить ссылки на диалоги
    GameObject replica1;
    GameObject replica2;
    GameObject replica3;
    GameObject replica4;
    void SetReplics () {
        replica1 = GameObject.Find ("ChillMan_replica1");
        replica1.SetActive (false);
        replica2 = GameObject.Find ("ChillMan_replica2");
        replica2.SetActive (false);
        replica3 = GameObject.Find ("ChillMan_replica3");
        replica3.SetActive (false);
        replica4 = GameObject.Find ("ChillMan_replica4");
        replica4.SetActive (false);
    }*/

    bool CheckProgress()
    {
        if (target == progress)
        {
            return true;
        }
        return false;
    }

    public void AddThisQuest()
    {
        QuestController.AddActiveQuest(GetComponent<Quest>());
    }
}