using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Brian_conditions : Conditions {
	
	//Завершить атаку
	public override IEnumerator FinishAttack ()
	{
		yield return new WaitForSeconds (unit.attackSpeed);
		attack = false;	
		unit.UseShield ();
	}

	//СМЕРТЬ
	public override void UnitDie (){
		anim.SetTrigger ("die");
		//unit.myStack.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}
}
