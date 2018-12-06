using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieZheron_conditions : Conditions {

	public override void DisableStun ()
	{
		base.DisableStun ();
		DisableRage ();
	}

	//Включить ярость
	bool rage = false;
	public void EnableRage() {
		rage = true;
		unit.inputX = unit.direction;
		SetMovespeed (15f);
	}

	//Отключить ярость
	void DisableRage () {
		rage = false;
	}

	//Фиксация столкновений с игроком во время ярости
	void OnCollisionStay2D (Collision2D target) {
		if (target.gameObject.CompareTag ("Player") && rage) {
			KnockDown (target.collider);
			DisableStun ();
		}
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
