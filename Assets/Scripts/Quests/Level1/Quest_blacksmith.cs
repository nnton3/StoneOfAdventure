using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Quest_blacksmith : Quest {

    DialogueWindow dialogueWindow;
    private void Start()
    {
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
    }

    [SerializeField]
    GameObject indicator;
    [SerializeField]
    string hello_friend = "Опять ты? Удалось найти руду?";
    [SerializeField]
    string not_complete_replic = "Того, что ты принес недостаточно, нужно больше руды";
    [SerializeField]
    string player_not_complete_answer = "Хорошо постараюсь найти еще";
    [SerializeField]
    string hello_replic = "Здравствуй воин. Нужна работа? Мне не хватает руды для работы. Если будет время спустись в шахту.";
    [SerializeField]
    string player_hello = "Здравствуй, хорошо я помогу тебе";
    [SerializeField]
    string player_have_ore = "Отдать руду";

    public void StartDialogue()
    {
        if (!QuestComplete())
        {
            dialogueWindow.Enable(true);
            indicator.SetActive(false);
           
            if (QuestInProgress())
            {
                dialogueWindow.CreateTextMessage(hello_friend);
                if (QuestController.CheckProgress(ID))
                {
                    dialogueWindow.CreateButton(player_have_ore, new UnityAction[] { EndQuest, delegate { dialogueWindow.Enable(false); } });
                    QuestController.DeleteActiveQuest(ID);
                }
                else
                {
                    dialogueWindow.CreateButton(player_have_ore, new UnityAction[] { QuestNotComplete });
                }
            }
            else
            {
                dialogueWindow.CreateTextMessage(hello_replic);
                if (QuestController.FindActiveQuest(5) == null)
                {
                    dialogueWindow.CreateButton(player_hello, new UnityAction[] { AddThisQuest, SwapIndicator, EnableIndicator, delegate { dialogueWindow.Enable(false); } });
                }
                else
                {
                    if (QuestController.CheckProgress(5))
                    {
                        dialogueWindow.CreateButton(player_have_ore, new UnityAction[] { EndQuest, EnableIndicator, delegate { QuestController.DeleteActiveQuest(5); } });
                    }
                    else
                    {
                        dialogueWindow.CreateButton(player_have_ore, new UnityAction[] { QuestNotComplete });
                    }
                }
           }
        }
    }

    bool QuestComplete()
    {
        return QuestList.quest1_blacksmith;
    }

    void QuestNotComplete()
    {
        dialogueWindow.CreateTextMessage(not_complete_replic);
        dialogueWindow.CreateButton(player_not_complete_answer, new UnityAction[] { EnableIndicator, delegate { dialogueWindow.Enable(false); } });
    }

    bool QuestInProgress()
    {
        return QuestController.FindActiveQuest(ID) != null;
    }

    public void AddThisQuest()
    {
        QuestController.AddActiveQuest(GetComponent<Quest>());
    }

    void EndQuest()
    {
        TakeOre();
        Destroy(indicator);
        objectivesComplete = true;
        QuestList.quest1_blacksmith = true;
        QuestList.quest5_blacksmith_ore = true;
    }


    [SerializeField]
    string quest_complete = "Спасибо, парень, вот твоя награда";
    [SerializeField]
    string player_quest_complete = "Всегда пожалуйста";


    void TakeOre()
    {
        dialogueWindow.CreateTextMessage(quest_complete);
        dialogueWindow.CreateButton(player_quest_complete, new UnityAction[] { delegate { dialogueWindow.Enable(false); } });
    }

    void EnableIndicator()
    {
        indicator.SetActive(true);
    }

    public Sprite questionMark;
    void SwapIndicator()
    {
        indicator.GetComponent<Image>().sprite = questionMark;
    }
}
