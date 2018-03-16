using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit {

	public LayerMask attackCollision;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {

		//Управление
		if (Input.GetKeyDown (KeyCode.F)) {
			GetDamage ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Roll ();
		}

		if (!invulnerability && !stunned) {
			input = Input.GetAxisRaw ("Horizontal");
		} else if (stunned) {
			input = 0f;
		} else {
			float step = 0.01f * Time.time;
			moveSpeed = Mathf.MoveTowards (7f, 0f, step);
		}
			
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);

		anim.SetBool ("run", Mathf.Abs (input) > 0.1f);
	}

	//Атака
	public override void GetDamage () {
		if (attackCheck && !stunned) {
			anim.SetTrigger ("attack");
			attackCheck = false;
		}
	}

	public void ResetAttackCheck () {
		attackCheck = true;
	}

	public void CreateAttackVector() {
		Vector2 targetVector = new Vector2 (direction, 0);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);

		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, targetVector, attackRange, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Unit> ().SetDamage (attack);
		}
	}

	public override void SetDamage (float damage) {
		anim.SetTrigger ("attackableWithSword");
		SetStun ();
	}

	public override void SetStun () {
		stunned = true;
	}

	public void ResetStunCheck () {
		stunned = false;
	}

	public override void Die () {

	}

	public void Roll() {
		if (!invulnerability) {
			invulnerability = true;
			Physics2D.IgnoreLayerCollision (9, 8, true);
			anim.SetTrigger ("roll");
			input = Mathf.Sign (direction);
		}
	}

	public void StopRoll() {
		moveSpeed = 5f;
		Physics2D.IgnoreLayerCollision (9, 8, false);
		invulnerability = false;
	}
}
