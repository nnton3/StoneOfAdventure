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
			target.GetComponent<Unit> ().health += attack;
		} else if (target.CompareTag ("Player")) {
			target.GetComponent<Unit> ().SetDamage(10f, direction, attackModify);
			anim.SetTrigger ("hit");
		}
	}

	public void SetDirection (float direction) {
		input = direction;
		flipParam = direction;
	}

	public override void GetDamage () {}

	public override void SetDamage (float damage, float impulseDirection, bool[] attackModify) {}

	public override void SetStun (float direction) {}

	public override void Die () {
		Destroy (gameObject);
	}

	IEnumerator Timer() {
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}
}