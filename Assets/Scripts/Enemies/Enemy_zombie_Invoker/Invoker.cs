using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : Zombie {

	void Update () {
		//Если зомби ожил
		if (!idle) {
			//Если он может двигаться и атаковать
			if (CanAttack() && CanMove()) {
				//Атаковать
				Attack ();
			} 
		}
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	public override IEnumerator TimeToBorn () {
		input = 1f;
		flipParam = input;
		anim.SetTrigger ("born");
		yield return new WaitForSeconds (bornDelay);
		input = 0f;
		rb.gravityScale = 1f;
	}
}
