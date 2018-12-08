using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class testDialogue : MonoBehaviour {

	DialogueWindow _dialogues;

    void Start () {
        _dialogues = GetComponent<DialogueWindow>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Test();
        }
    }

    void Test() {
        _dialogues.Enable(true);
        _dialogues.CreateTextMessage("Привет дружок, позьми с полки пирожок");
        _dialogues.CreateButton("норм получилось", new UnityAction[] { TestMhetod, delegate { _dialogues.Enable(false); } });
    }

    void TestMhetod()
    {
        Debug.Log("work");
    }
}
