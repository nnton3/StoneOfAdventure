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

	public float runDistance = 7f;
	public float runSpeed = 5f;
	float zombySpeed;
	public float bornDelay = 0f;

	//Сила толчка во время получения урона
	public float impulsePower = 1;

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

		if (!idle) {
			
			if (Mathf.Abs (target.transform.position.x - transform.position.x) < runDistance) {
				zombySpeed = runSpeed;
			} else
				zombySpeed = moveSpeed;
			
			if (alive && Mathf.Abs (target.transform.position.x - transform.position.x) < attackRange && ((target.transform.position.x > transform.position.x && direction > 0f) || (target.transform.position.x < transform.position.x && direction < 0f))) {
				input = 0f;
				GetDamage ();
			} else if (attackCheck && alive) {
				input = (target.transform.position.x > transform.position.x) ? 1 : -1;
			} else if (!alive) {
				input = 0f;
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
			hit.transform.GetComponent<Unit> ().SetDamage (attack, direction);
		}
	}

	//Получить урон
	public override void SetDamage (float damage, float impulseDirection) {
		if (health <= damage) {
			Die ();
			return;
		}

		Impulse (impulseDirection);
		health -= damage;
	}

	//Получить стан
	public override void SetStun () {

	}

	//Умереть
	public override void Die () {
		anim.SetTrigger ("die");
		start.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}

	//Начать преследование
	public void Chase (GameObject player) {
		target = player;
		StartCoroutine ("TimeToBorn");
	}

	public void StartChase() {
		idle = false;
	}

	//Остановить преследование
	public void Idle () {

	}

	void Impulse(float inputDirection) {
		rb.AddForce (new Vector2 (inputDirection * impulsePower, impulsePower/3.5f), ForceMode2D.Impulse);
	}

	//Задержка перед воскрешением
	IEnumerator TimeToBorn() {
		yield return new WaitForSeconds (bornDelay);
		anim.SetTrigger ("born");
	}
}
