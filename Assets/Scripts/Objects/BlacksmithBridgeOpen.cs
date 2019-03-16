using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithBridgeOpen : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    [SerializeField]
    GameObject barrier;
    [SerializeField]
    GameObject indicate;
    public void OpenBridge ()
    {
        if (!GameManager.GetBattleMode())
        {
            Destroy(barrier);
            Destroy(indicate);
            anim.SetTrigger("open");
        }
    }
}
