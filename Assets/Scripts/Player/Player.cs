﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit {

	public LayerMask attackCollision;
	public GameObject arrow;

	bool inBlock = false;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {

		//Управление
		if (Input.GetKeyDown (KeyCode.F)) {       //Атака мечом
			GetDamage ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {       //Перекат
			Roll ();
		}

		if (Input.GetKeyDown (KeyCode.B)) {       //Блок
			Block ();
		}

		if (Input.GetKeyDown (KeyCode.P)) {       //Атака из лука
			PullBow ();
		}

		if (stunned || !alive) {
			input = 0f;
		} else if (!invulnerability && !stunned || inBlock) {
			input = Input.GetAxisRaw ("Horizontal");
		} else {
				float step = 0.01f * Time.time;
				moveSpeed = Mathf.MoveTowards (7f, 0f, step);
		}
			
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);

		anim.SetBool ("run", Mathf.Abs (input) > 0.1f);
	}

	//Атака
	public override void GetDamage () {

		if (inBlock) {
			StopBlock ();
		}

		if (attackCheck && !stunned) {
			anim.SetTrigger ("attack");
			attackCheck = false;
			//Меняем скорость атаки в зависимости от заданного параметра
			anim.speed = 1 / attackSpeed;
		}
	}

	//Сбросить чек атаки
	public void ResetAttackCheck () {
		attackCheck = true;
		anim.speed = 1;
	}

	//Построить луч атаки
	public void CreateAttackVector() {
		Vector2 targetVector = new Vector2 (direction, 0);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);

		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, targetVector, attackRange, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Unit> ().SetDamage (attack);
		}
	}

	//Получить урон
	public override void SetDamage (float damage) {
		if (!invulnerability) {
			if (health > damage) {
				anim.SetTrigger ("attackable");
				SetStun ();
				health -= damage;
			} else
				Die ();
		} else if (inBlock) {
			anim.SetTrigger ("attackable");
		}
	}

	//Получить стан
	public override void SetStun () {
		stunned = true;
	}

	//Сбросить чек стана
	public void ResetStunCheck () {
		stunned = false;
	}

	//Умереть
	public override void Die () {
		alive = false;
	}

	//Использовать блок
	void Block() {
		if (!invulnerability && !inBlock) {
			invulnerability = true;
			inBlock = true;
			anim.SetTrigger ("block");
		} else
			StopBlock ();
	}

	//Завершить блок
	public void StopBlock () {
		invulnerability = false;
		inBlock = false;
		anim.SetTrigger ("block");
	}

	//Использовать перекат
	void Roll() {

		if (inBlock) {
			StopBlock ();
		}

		if (!invulnerability) {
			invulnerability = true;
			Physics2D.IgnoreLayerCollision (9, 8, true);
			anim.SetTrigger ("roll");
			input = Mathf.Sign (direction);
		}
	}

	//Завершить перекат
	public void StopRoll() {
		moveSpeed = 5f;
		Physics2D.IgnoreLayerCollision (9, 8, false);
		invulnerability = false;
	}

	//Стрельба из лука
	void PullBow () {
		anim.SetTrigger ("pullBow");
	}

	//Выпустить стрелу
	public void CreateArrow() {
		GameObject arrowInstance = Instantiate (arrow, new Vector3 (transform.position.x, transform.position.y + 0.9f, transform.position.z), Quaternion.identity);
		Аrrow arrowScript = arrowInstance.GetComponent<Аrrow> ();
		arrowScript.SetDirection (direction);
	}
}