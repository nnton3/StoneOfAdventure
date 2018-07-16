using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieZheron_conditions : Conditions {

	public override void DisableStun ()
	{
		base.DisableStun ();
		DisableRage ();
	}

	//Удар сбивающий с ног
	public override void KnockDown (Collider2D target) {
		target.GetComponent<Damage> ().KnockDown (unit.attackPoints, unit.direction);
	}

	bool rage = false;
	public void EnableRage() {
		rage = true;
		unit.input = unit.direction;
		SetMovespeed (15f);
	}

	void DisableRage () {
		rage = false;
	}

	void OnTriggerEnter2D (Collider2D target) {
		if (target.CompareTag ("Enemy") && rage) {
			Debug.Log ("enter");
			KnockDown (target);
		}
	}
}
