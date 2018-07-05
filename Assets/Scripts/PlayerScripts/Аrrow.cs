using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Аrrow : Unit {

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine ("Timer");
	}

	void Update () {
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	void OnTriggerEnter2D (Collider2D target) {
		if (target.CompareTag ("Enemy")) {
			target.GetComponent<Damage> ().DefaultDamage(attackPoints, direction);
			Destroy (gameObject);
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
