using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueWindow : MonoBehaviour {

    public GameObject dialogueText;
    public GameObject dealogueButton;
    GameObject dialogueWindow;

    private void Start()
    {
        dialogueWindow = GameObject.Find("DialogueWindow");
        playerControl = GameObject.Find("PlayerControlButtons");
        dialogueWindow.SetActive(false);
    }

    GameObject playerControl;
    public void Enable(bool enable)
    {
        if (!enable)
        {
            ClearWindow();
        }
        foreach (GameObject obj in UI_list)
        {
            Debug.Log(obj.name);
        }
        dialogueWindow.SetActive(enable);
        playerControl.SetActive(!enable);
    }

    List<GameObject> UI_list = new List<GameObject>();
    void ClearWindow()
    {
        foreach (GameObject ui_obj in UI_list)
        {
            Destroy(ui_obj);
        }
        UI_list = new List<GameObject>();
    }

    public void CreateTextMessage (string text)
    {
        GameObject messageObj = Instantiate(dialogueText, dialogueWindow.transform);
        UI_list.Add(messageObj);
        Text message = messageObj.GetComponent<Text>();
        message.text = text;
    }

    public void CreateButton(string buttonText, UnityAction[] action)
    {
        GameObject buttonObj = Instantiate(dealogueButton, dialogueWindow.transform);
        UI_list.Add(buttonObj);
        buttonObj.GetComponent<Text>().text = buttonText;

        Button buttonComponent = buttonObj.GetComponent<Button>();
        for (int i = 0; i < action.Length; i++)
        {
            buttonComponent.onClick.AddListener(action[i]);
        }
    }
}
