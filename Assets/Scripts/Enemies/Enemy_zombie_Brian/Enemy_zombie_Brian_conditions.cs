using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Brian_conditions : Conditions {

	//БЛОК
	public override void EnableBlock ()
	{
		base.EnableBlock ();
	}

	public override void DisableBlock ()
	{
		base.DisableBlock ();
	}

	//ОГЛУШЕНИЕ
	public override void EnableStun (float direction)
	{
		base.EnableStun (direction);
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

	//Завершить атаку
	public override IEnumerator FinishAttack ()
	{
		unit.UseShield ();
		yield return new WaitForSeconds (unit.attackSpeed);
		unit.UseShield ();

		attack = false;	
	}

	public override void UnitDie (){
		anim.SetTrigger ("die");
		unit.myStack.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}
}
