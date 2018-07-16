﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour {

	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public Rigidbody2D rb;

	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine ("Timer");
	}

	[HideInInspector]
	public float input = 0f;
	public float moveSpeed = 0f;

	void Update () {
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	void OnTriggerEnter2D (Collider2D target) {
		TrueHit (target);
	}
		
	public float attackPoints = 10f;
	public virtual void TrueHit (Collider2D target) {
		if (target.CompareTag ("Enemy")) {
			target.GetComponent<Damage> ().DefaultDamage(attackPoints, input);
			Destroy (gameObject);
		}
	}

	public virtual void SetDirection (float direction) {
		input = direction;
		if (direction < 0f) {
			FlipObject();
		}
	}

	public float lifeTime = 3f;
	public virtual IEnumerator Timer() {
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}

	private void FlipObject()
	{
		//получаем размеры персонажа
		Vector3 theScale = transform.localScale;
		//зеркально отражаем персонажа по оси Х
		theScale.x *= -1;
		//задаем новый размер персонажа, равный старому, но зеркально отраженный
		transform.localScale = theScale;
	}
}