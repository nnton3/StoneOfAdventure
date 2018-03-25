using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieBrian : Unit, IReaction<GameObject> {

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
		Debug.Log("attackCheck = " + attackCheck);
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
		anim.SetBool ("run", Mathf.Abs (input) > 0.1f);
	}

	//Нанести урон
	public override void GetDamage () {
		if (attackCheck) {
			attackCheck = false;
			float attackAnim = Random.Range (0f, 1f);

			if (attackAnim <= 0.3f) {
				anim.SetTrigger ("attack1");
				return;
			}

			if (attackAnim > 0.3f && attackAnim <= 0.6f) {
				anim.SetTrigger ("attack2");
				return;
			}

			if (attackAnim > 0.6f) {
				anim.SetTrigger ("block");
				InBlock ();
				return;
			}
		}
	}

	//Сбросить чек атаки
	public IEnumerator ResetAttackCheck () {

		if (invulnerability) {
			invulnerability = false;
		}

		yield return new WaitForSeconds (attackSpeed);
		attackCheck = true;
		Debug.Log ("work");
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

	//Поставить блок
	void InBlock() {
		invulnerability = true;
		StartCoroutine ("BlockTimer");
	}

	public override void SetDamage (float damage){
		if (health <= damage && !invulnerability) {
			Die ();
			return;
		}

		anim.SetTrigger ("attackable");

		if (invulnerability) {
			return;
		}

		health -= damage;
	}

	public override void SetStun (){}

	public override void Die (){
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
		idle = false;
	}

	//Остановить преследование
	public void Idle () {
		
	}

	IEnumerator BlockTimer() {
		yield return new WaitForSeconds (5f);
		anim.SetTrigger ("block");
		StartCoroutine ("ResetAttackCheck");
	}
}
