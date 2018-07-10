using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Kenny_conditions : Conditions {
	
	public override void HitThroughTheBlock ()
	{
		base.HitThroughTheBlock ();
	}

	public override IEnumerator FinishAttack ()
	{
		return base.FinishAttack ();
		unit.input = 0f;
	}

	public override void UnitDie (){
		anim.SetTrigger ("die");
		unit.myStack.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}
}
