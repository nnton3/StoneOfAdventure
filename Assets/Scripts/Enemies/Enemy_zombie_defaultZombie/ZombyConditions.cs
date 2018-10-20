using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombyConditions : Conditions {

	public override IEnumerator FinishAttack ()
	{
		yield return new WaitForSeconds (unit.attackSpeed);
		attack = false;
	}

	//СМЕРТЬ
	public override void UnitDie ()
	{
		//Destroy (hpBar);
		anim.SetTrigger ("die");
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
		unit.enemieTriggerScript.AddCorpse ();
	}
}
