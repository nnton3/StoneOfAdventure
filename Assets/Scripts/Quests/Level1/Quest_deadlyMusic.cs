using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Quest_deadlyMusic : Quest
{

    DialogueWindow dialogueWindow;
    private void Start()
    {
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
    }

    [SerializeField]
    GameObject indicator;
    [SerializeField]
    string hello_replic = "Ааарргх... Лекарство... Мне нужно лекарство";
    [SerializeField]
    string player_hello = "...";
    [SerializeField]
    string not_complete_replic = "Лекарство...Ааагх";
    [SerializeField]
    string player_not_complete_answer = "...";
    [SerializeField]
    string quest_complete = "Спасибо, ты спас меня от неминуемого превращения в одного из этих ужасных монстров, что продят в округе";
    [SerializeField]
    string player_quest_complete = "Рад был помочь";
    [SerializeField]
    string player_have_medicine = "Отдать лекарство";

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
                if (QuestController.FindActiveQuest(7) == null)
                {
                    dialogueWindow.CreateButton(player_hello, new UnityAction[] { AddThisQuest, SwapIndicator, EnableIndicator, delegate { dialogueWindow.Enable(false); } });
                }
                else
                    dialogueWindow.CreateButton(player_have_medicine, new UnityAction[] { EndQuest, EnableIndicator, delegate { QuestController.DeleteActiveQuest(7); } });
            }
        }
    }

    bool QuestComplete()
    {
        return QuestList.quest2_chill_man;
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
        TakeSword();
        Destroy(indicator);
        objectivesComplete = true;
        QuestList.quest2_chill_man = true;
        QuestList.quest3_chill_man_sword = true;
    }

    void TakeSword()
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
