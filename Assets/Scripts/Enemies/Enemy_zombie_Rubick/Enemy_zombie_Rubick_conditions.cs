using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Rubick_conditions : Conditions {
	
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
	//Выпустить снаряд
	public override void Bow_Attack ()
	{
		GameObject arrowInstance = Instantiate (patron, new Vector3 (transform.position.x + (1.75f * unit.direction), transform.position.y + 1.2f, transform.position.z), Quaternion.identity);
		Patron arrowScript = arrowInstance.GetComponent<Patron> ();
		arrowScript.SetDirection (unit.direction);
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
		unit.myStack.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}
}
