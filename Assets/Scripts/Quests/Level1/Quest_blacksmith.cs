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
    string not_complete_replic = "Того, что ты принес недостаточно, нужно больше руды";
    [SerializeField]
    string player_not_complete_answer = "Хорошо постараюсь найти еще";
    [SerializeField]
    string hello_replic = "Здравствуй воин. Нужна работа? Мне не хватает руды для работы. Если будет спустись в шахту.";
    [SerializeField]
    string player_hello = "Здравствуй, хорошо я помогу тебе";
    [SerializeField]
    string player_have_sword = "Отдать руду";

    public void StartDialogue()
    {
        if (!QuestComplete())
        {
            dialogueWindow.Enable(true);
            indicator.SetActive(false);
           
            if (QuestInProgress())
            {
                if (QuestController.CheckProgress(ID))
                {
                    EndQuest();
                    QuestController.DeleteActiveQuest(ID);
                }
                else
                {
                    dialogueWindow.CreateTextMessage(not_complete_replic);
                    dialogueWindow.CreateButton(player_not_complete_answer, new UnityAction[] { EnableIndicator, delegate { dialogueWindow.Enable(false); } });
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
                    dialogueWindow.CreateButton(player_have_sword, new UnityAction[] { EndQuest, EnableIndicator, delegate { QuestController.DeleteActiveQuest(3); } });
            }
        }
    }

    bool QuestComplete()
    {
        return QuestList.quest1_blacksmith;
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
