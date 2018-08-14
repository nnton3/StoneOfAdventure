using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_invoker_conditions : Conditions {

	//ОГЛУШЕНИЕ
	public override void DisableStun ()
	{
		base.DisableStun ();
	}

	//АТАКА
	public GameObject zombie;

	public override void Default_Attack ()
	{
		GameObject zombyInstance = Instantiate (zombie, new Vector3 (transform.position.x + Random.Range (-3, -15), transform.position.y + 0.125f, transform.position.z), Quaternion.identity);
		Enemy_invokedZomby zombyScript = zombyInstance.GetComponent<Enemy_invokedZomby> ();
		zombyScript.Alert(GetComponent<Enemy>().target);
	}

	//СМЕРТЬ
	public override void UnitDie ()
	{
		DisableStun ();
		anim.SetTrigger ("die");
		//unit.myStack.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}


}