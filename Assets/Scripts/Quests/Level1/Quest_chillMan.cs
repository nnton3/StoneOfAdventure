using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Quest_chillMan : Quest
{
    DialogueWindow dialogueWindow;
    private void Start()
    {
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
    }

    [SerializeField]
    GameObject indicator;
    [SerializeField]
    string hello_replic = "Приветствую тебя славный воин, помоги мне вернуть мой меч. Его забрал мерзкий зомби со щитом";
    [SerializeField]
    string player_hello = "Здравствуй, хорошо я помогу тебе";
    [SerializeField]
    string not_complete_replic = "И где мой меч парень?";
    [SerializeField]
    string player_not_complete_answer = "Ща все буит паринь";
    [SerializeField]
    string quest_complete = "О, спасибо";
    [SerializeField]
    string player_quest_complete = "Да не за что";
    [SerializeField]
    string player_have_sword = "Отдать меч";

    public void StartDialogue()
    {
        if (!QuestComplete())
        {
            dialogueWindow.Enable(true);
            indicator.SetActive(false);
            if (QuestInProgress())
            {
                dialogueWindow.CreateTextMessage(not_complete_replic);
                if (QuestController.CheckProgress(ID))
                {
                    dialogueWindow.CreateButton(player_have_sword, new UnityAction[] { EndQuest, delegate { dialogueWindow.Enable(false); } });
                    QuestController.DeleteActiveQuest(ID);
                }
                else
                {
                    dialogueWindow.CreateButton(player_not_complete_answer, new UnityAction[] { EnableIndicator, delegate { dialogueWindow.Enable(false); } });
                }
            }
            else
            {
                dialogueWindow.CreateTextMessage(hello_replic);
                if (QuestController.FindActiveQuest(3) == null)
                {
                    dialogueWindow.CreateButton(player_hello, new UnityAction[] { AddThisQuest, SwapIndicator, EnableIndicator, delegate { dialogueWindow.Enable(false); } });
                }
                else
                    dialogueWindow.CreateButton(player_have_sword, new UnityAction[] { EndQuest, EnableIndicator, delegate { QuestController.DeleteActiveQuest(3); } });
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
        TakeSword();
        Destroy(indicator);
        objectivesComplete = true;
        QuestList.quest2_chill_man = true;
        QuestList.quest3_chill_man_sword = true;
    }

    void TakeSword ()
    {
        dialogueWindow.CreateTextMessage(quest_complete);
        dialogueWindow.CreateButton(player_quest_complete, new UnityAction[] { delegate { dialogueWindow.Enable(false); } });
    }

    void EnableIndicator()
    {
        indicator.SetActive(true);
    }

    public Sprite questionMark;
    void SwapIndicator ()
    {
        indicator.GetComponent<Image>().sprite = questionMark;
    }
}