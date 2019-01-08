using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Astofena_conditions : Conditions {

    public override void UnitDie()
    {
        anim.SetTrigger("die");
        //unit.myStack.AddCorpse ();
        alive = false;
        gameObject.layer = 2;
        gameObject.tag = "Puddle";
    }
}
