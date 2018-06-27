using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_invokedZombie_conditions : Conditions {

	//ОГЛУШЕНИЕ
	public override void EnableStun (float stunDirection)
	{
		base.EnableStun (stunDirection);
	}

	public override void DisableStun ()
	{
		base.DisableStun ();
	}

	//АТАКА
	public override void Default_Attack ()
	{
		base.Default_Attack ();
	}

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
		unit.myStack.AddCorpse ();
	}
}
