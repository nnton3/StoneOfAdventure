using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject_chillMan_sword : QuestObject {

    public override void AddProgress()
    {
        QuestController.AddQuestProgress(2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddProgress();
            Destroy(gameObject);
        }
    }
}
