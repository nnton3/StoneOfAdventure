using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubick_patron : Patron {

	public override void TrueHit (Collider2D target)
	{
		if (target.CompareTag ("Enemy")) {
			anim.SetTrigger ("hit");
			target.GetComponent<Damage> ().Healing(attackPoints);
		} else if (target.CompareTag ("Player")) {
			target.GetComponent<Damage> ().DefaultDamage(attackPoints, input);
			anim.SetTrigger ("hit");
		}
	}
}