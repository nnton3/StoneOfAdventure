﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_invokedZomby : Unit, IReaction<GameObject> {

	//Атакуемые слои
	public LayerMask attackCollision;
	Flip flip;

	//Ссылка на игрока
	[HideInInspector]
	public GameObject target;

	public float runDistance = 7f;
	public float runSpeed = 5f;
	float zombySpeed;

	//Сила толчка во время получения урона
	public float impulsePower = 3;

	bool idle = true;

	void Start () {
		flip = GetComponent<Flip> ();
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		Chase (target);
	}

	void Update () {

		if (!idle) {

			if (Mathf.Abs (target.transform.position.x - transform.position.x) < runDistance) {
				zombySpeed = runSpeed;
			} else
				zombySpeed = moveSpeed;

			if (alive && Mathf.Abs (target.transform.position.x - transform.position.x) < (attackRange - 0.3f) && ((target.transform.position.x > transform.position.x && direction > 0f) || (target.transform.position.x < transform.position.x && direction < 0f))) {
				input = 0f;
				GetDamage ();
			} else if (attackCheck && alive) {
				input = (target.transform.position.x > transform.position.x) ? 1 : -1;
			} else if (!alive) {
				float step = 1f * Time.time;
				moveSpeed = Mathf.MoveTowards (impulsePower, 0f, step);
			}
		}

		rb.velocity = new Vector2 (input * zombySpeed, rb.velocity.y);

		anim.SetFloat ("speed", Mathf.Abs (input * zombySpeed));
	}

	//Нанести урон
	public override void GetDamage () {
		if (attackCheck) {
			attackCheck = false;
			anim.SetTrigger ("attack");
		}
	}

	//Сбросить чек атаки
	public IEnumerator ResetAttackCheck () {
		yield return new WaitForSeconds (attackSpeed);
		attackCheck = true;
	}

	//Построить луч атаки
	public void CreateAttackVector() {
		Vector2 targetVector = new Vector2 (direction, 0);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);

		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, targetVector, attackRange, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Unit> ().SetDamage (attack, direction,false);
		}
	}

	//Получить урон
	public override void SetDamage (float damage, float impulseDirection, bool piercing_attack) {
		if (health <= damage) {
			flip.enabled = false;
			input = impulseDirection;
			Die ();
			return;
		}

		health -= damage;
	}

	//Получить стан
	public override void SetStun () {

	}

	//Сбросить чек стана
	public void ResetStunCheck () {
		input = 0f;
	}

	//Умереть
	public override void Die () {
		anim.SetTrigger ("die");
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}

	//Начать преследование
	public void Chase (GameObject player) {
		Debug.Log ("work");
		target = player;
		anim.SetTrigger ("born");
	}

	public void StartChase() {
		gameObject.layer = 9;
		idle = false;
	}

	//Остановить преследование
	public void Idle () {
	}
}
