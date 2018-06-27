using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubick_patron : Unit {

	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine ("Timer");
	}

	void Update () {
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	void OnTriggerEnter2D (Collider2D target) {
		if (target.CompareTag ("Enemy")) {
			anim.SetTrigger ("hit");
			target.GetComponent<Damage> ().Healing(attackPoints);
		} else if (target.CompareTag ("Player")) {
			target.GetComponent<Damage> ().DefaultDamage(attackPoints, direction);
			anim.SetTrigger ("hit");
		}
	}

	public void SetDirection (float direction) {
		input = direction;
		flipParam = direction;
	}

	IEnumerator Timer() {
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}
}