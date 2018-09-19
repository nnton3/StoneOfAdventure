using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Kenny_conditions : Conditions {
	
	public override void Hit_Through_The_Block ()
	{
		base.Hit_Through_The_Block ();
	}

	public override IEnumerator FinishAttack ()
	{
		return base.FinishAttack ();
		unit.inputX = 0;
	}

	public override void UnitDie (){
		anim.SetTrigger ("die");
		//unit.myStack.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}
}
