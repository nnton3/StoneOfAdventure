using UnityEngine;

public class Quest_deadlyMusic_medicine : Quest, I_QuestObject
{

    public void AddProgress()
    {
        QuestController.AddQuestProgress(ID);
    }

    public GameObject indicator;
    void Start()
    {
        if (transform.lossyScale.x < 0f)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
        }
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
        indicator.SetActive(true);
    }

    DialogueWindow dialogueWindow;
    public string object_discription = "Паучья железа, может быть получится где-то ее применить";
    public string player_do = "Взять";
    public void StartDialogue()
    {
        dialogueWindow.Enable(true);
        dialogueWindow.CreateTextMessage(object_discription);
        dialogueWindow.CreateButton(player_do, new UnityEngine.Events.UnityAction[] { AddThisQuest, delegate { dialogueWindow.Enable(false); } });
    }

    public void AddThisQuest()
    {
        if (QuestController.FindActiveQuest(6) == null)
        {
            QuestController.AddActiveQuest(GetComponent<Quest>());
        }
        else
        {
            QuestController.AddQuestProgress(6);
        }
        transform.position = new Vector3(-100, 100, 0);
    }
}
