using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest_littleGirl : Quest {

    DialogueWindow dialogueWindow;

    private void Start()
    {
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
    }

    public GameObject indicator;
    public string thanks_replic = "Спасибо, что разобрался с этми монстрами внизу";
    public string player_answer = "Тебе бы лучше вернуться домой, кажется тут не безопасно";
    public void StartDialogue()
    {
        dialogueWindow.Enable(true);
        dialogueWindow.CreateTextMessage(thanks_replic);
        dialogueWindow.CreateButton(player_answer, new UnityAction[] { delegate { dialogueWindow.Enable(false); }, EndQuest });
    }

    void EndQuest ()
    {
        Destroy(indicator);
        objectivesComplete = true;
        QuestList.quest4_little_girl = true;
    }

    public List<Conditions> zombieList;
	bool CheckProgress () {
		progress = 0;
		foreach (Conditions zombie in zombieList) {
			if (!zombie.alive) {
				progress++;
			}
		}
        return target == progress;
	}
}
