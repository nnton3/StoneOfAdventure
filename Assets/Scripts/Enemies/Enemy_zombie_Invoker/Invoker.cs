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
		rb.velocity = new Vector2 (inputX * moveSpeed, rb.velocity.y);
	}

	public override IEnumerator TimeToBorn () {
		inputX = 1f;
		flipParam = inputX;
		anim.SetTrigger ("born");
		yield return new WaitForSeconds (bornDelay);
		flipParam = -1;
		inputX = 0f;
		rb.gravityScale = 1f;
	}
}
