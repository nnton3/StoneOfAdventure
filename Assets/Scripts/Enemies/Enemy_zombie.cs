using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie : Unit, IReaction<GameObject> {

	//Атакуемые слои
	public LayerMask attackCollision;
	//Область агра
	DangerArea start;

	//Ссылка на игрока
	GameObject target;

	bool idle = true;

	void Awake() {
		start = GetComponentInParent<DangerArea> ();
		start.AddEnemie (this);
	}

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {

		Vector2 targetVector = new Vector2 (direction, 0f);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);
		Debug.DrawRay (rayOrigin, targetVector * attackRange, Color.red);


		if (!idle) {
			if (alive && Mathf.Abs (target.transform.position.x - transform.position.x) < attackRange && ((target.transform.position.x > transform.position.x && direction > 0f) || (target.transform.position.x < transform.position.x && direction < 0f))) {
				input = 0f;
				GetDamage ();
			} else if (attackCheck && alive) {
				input = (target.transform.position.x > transform.position.x) ? 1 : -1;
			} else if (!alive) {
				input = 0f;
			}
		}

		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);

		anim.SetFloat ("speed", Mathf.Abs (input * moveSpeed));
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
			hit.transform.GetComponent<Unit> ().SetDamage (attack);
		}
	}

	//Получить урон
	public override void SetDamage (float damage) {
		if (health <= damage) {
			anim.SetTrigger ("die");
			Die ();
			return;
		}

		health -= damage;
	}

	//Получить стан
	public override void SetStun () {

	}

	//Умереть
	public override void Die () {
		alive = false;
		gameObject.layer = 2;
	}

	//Начать преследование
	public void Chase (GameObject player) {
		target = player;
		idle = false;
	}

	//Остановить преследование
	public void Idle () {

	}
}
