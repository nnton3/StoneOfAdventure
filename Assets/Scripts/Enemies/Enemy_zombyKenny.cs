using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombyKenny : Unit, IReaction<GameObject> {

	//Атакуемые слои
	public LayerMask attackCollision;
	//Область агра
	DangerArea start;
	Flip flip;

	//Ссылка на игрока
	GameObject target;
	public float bornDelay = 0f;
	bool idle = true;
	bool attackable = false;

	//Сила толчка во время получения урона
	public float impulsePower = 3;

	void Awake() {
		start = GetComponentInParent<DangerArea> ();
		start.AddEnemie (this);
	}

	void Start () {
		flip = GetComponent<Flip> ();
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		Debug.Log ("input = " + input);
		if (!idle && alive && !stunned) {
			if (Mathf.Abs (target.transform.position.x - transform.position.x) < (attackRange - 0.5f) && ((target.transform.position.x > transform.position.x && direction > 0f) || (target.transform.position.x < transform.position.x && direction < 0f))) {
				input = 0f;
				GetDamage ();
			} else if (attackCheck) {
				input = (target.transform.position.x > transform.position.x) ? 1 : -1;
			} 
		} else if (!alive || stunned) {
			float step = 0.01f * Time.time;
			moveSpeed = Mathf.MoveTowards (impulsePower, 0f, step);
		}

		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
		anim.SetBool ("run", Mathf.Abs (input) > 0.1f);
		anim.SetBool ("stunned", stunned);
	}

	public override void GetDamage (){
		if (attackCheck) {
			attackCheck = false;
			anim.SetTrigger ("attack");
		}
	}

	public IEnumerator SecondAttack() {
		attackable = true;
		stunned = true;
		input = (target.transform.position.x > transform.position.x) ? 1 : -1;
		yield return new WaitForSeconds (0.5f);
		attackable = false;
	}

	//Построить луч атаки
	public void CreateAttackVector() {
		Vector2 targetVector = new Vector2 (direction, 0);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);

		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, targetVector, attackRange, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Unit> ().SetDamage (attack, direction, true);
		}
	}

	public override void SetDamage (float damage, float impulseDirection, bool piercing_attack){
		if (health > damage) {
			SetStun ();
			input = impulseDirection;
			if (attackable) {
				anim.SetTrigger ("attackable");
			}
		} else {
			flip.enabled = false;
			SetStun ();
			input = impulseDirection;
			Die ();
		}

		health -= damage;
	}

	public IEnumerator ResetAttackCheck () {
		yield return new WaitForSeconds (attackSpeed);
		attackCheck = true;
	}

	public override void SetStun (){
		flip.enabled = false;
		stunned = true;
	}

	//Сбросить чек стана
	public void ResetStunCheck () {
		Debug.Log ("work");
		input = 0f;
		flip.enabled = true;
		moveSpeed = 2f;
		stunned = false;
	}
	public override void Die (){
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

	//Задержка перед воскрешением
	IEnumerator TimeToBorn() {
		yield return new WaitForSeconds (bornDelay);
		anim.SetTrigger ("born");
	}

	public void StartChase() {
		gameObject.layer = 9;
		idle = false;
	}

	//Остановить преследование
	public void Idle () {}
}
