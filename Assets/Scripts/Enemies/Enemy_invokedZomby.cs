using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_invokedZomby : Unit, IReaction<GameObject> {

	//Атакуемые слои
	public LayerMask attackCollision;

	//Ссылка на игрока
	[HideInInspector]
	public GameObject target;

	public float runDistance = 7f;
	public float runSpeed = 5f;
	float zombySpeed;

	//Сила толчка во время получения урона
	public float impulsePower = 3;

	//Местоположения относительно игрока
	float targetRange = 0f;
	float targetDirection =0f;

	bool idle = true;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		Chase (target);
	}

	void Update () {

		if (!idle) {

			if (alive) {

				//Определение местоположения игрока
				targetRange = Mathf.Abs (transform.position.x - target.transform.position.x);
				targetDirection = Mathf.Sign (transform.position.x - target.transform.position.x);
				flipParam = input;

				if (attackCheck) {
					if (targetRange < (attackRange - 0.3f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f))) {
						input = 0f;
						GetDamage ();
					} else {
						if (targetRange < runDistance) {
							zombySpeed = runSpeed;
						} else
							zombySpeed = moveSpeed;

						input = -targetDirection;
						rb.velocity = new Vector2 (input * zombySpeed, rb.velocity.y);
					}
				}
				anim.SetFloat ("speed", Mathf.Abs (input * zombySpeed));
			} else if (stunned || !alive) {
				Impulse ();
			}
		}
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
			hit.transform.GetComponent<Unit> ().SetDamage (attack, direction, attackModify);
		}
	}

	//Получить урон
	public override void SetDamage (float damage, float impulseDirection, bool[] attackModify) {
		SetStun (impulseDirection);
		health -= damage;
		Die ();
	}

	//Получить стан
	public override void SetStun (float direction) {
		stunned = true;
		input = direction;
	}

	//Сбросить чек стана
	public void ResetStunCheck () {
		stunned = false;
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

	void Impulse () {
		zombySpeed = Mathf.Sqrt(Time.deltaTime) * impulsePower;
	}
}
