﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_startEvent : MonoBehaviour
{
    DialogueWindow dialogueWindow;
    private void Start()
    {
        dialogueWindow = (DialogueWindow)FindObjectOfType(typeof(DialogueWindow));
    }

    [SerializeField]
    string player_hello_replic = "Ого, кажется игра все таки релизнулась";
    public void PlayerTextPrint()
    {
        dialogueWindow.CreateTextMessage(player_hello_replic);
    }

    [SerializeField]
    string littleGirl_hello_replic = "Даже не верится";
    public void LittleGirlTextPrint()
    {
        dialogueWindow.CreateTextMessage(littleGirl_hello_replic);
    }

    [SerializeField]
    string zombie_hello_replic = "Очередные, мать их, зомби";
    public void ZombieTextPrint()
    {
        dialogueWindow.CreateTextMessage(zombie_hello_replic);
    }

    public void EnableDialogueWindow()
    {
        dialogueWindow.Enable(true);
    }

    public void DisableDialogueWindow()
    {
        dialogueWindow.Enable(false);
    }

    [SerializeField]
    DangerArea startDangerArea;
    public void StartZombieAttack()
    {
        startDangerArea.GoToAttack();
    }

    public void DisableStartEvent ()
    {
        this.gameObject.SetActive(false);
    }
}
