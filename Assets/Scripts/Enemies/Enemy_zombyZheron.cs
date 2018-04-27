using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombyZheron : Unit, IReaction<GameObject> {

	//Атакуемые слои
	public LayerMask attackCollision;
	//Область агра
	DangerArea start;
	Flip flip;

	//Ссылка на игрока
	GameObject target;
	public float bornDelay = 0f;
	bool idle = true;

	//Сила толчка во время получения урона
	public float impulsePower = 3;

	//Ключевые расстояния
	[Header("Чеки по дальности")]
	public float longRange = 10f;
	public float midRange = 7f;
	public float meleeRange = 3f;

	//Местоположения относительно игрока
	float targetRange = 0f;
	float targetDirection =0f;

	void Awake() {
		start = GetComponentInParent<DangerArea> ();
		start.AddEnemie (this);
	}

	void Start () {
		flip = GetComponent<Flip> ();
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		invulnerability = true;
	}
	
	void Update () {

		if (alive && !stunned && !idle) {
			
			//Определение местоположения игрока
			targetRange = Mathf.Abs (transform.position.x - target.transform.position.x);
			targetDirection = Mathf.Sign (transform.position.x - target.transform.position.x);

			anim.SetBool ("goForward", targetRange > longRange);
			if (attackCheck) {
				if (targetRange < midRange) {
					if (((target.transform.position.x > transform.position.x && direction > 0f) || (target.transform.position.x < transform.position.x && direction < 0f))) {
						input = 0f;
						GetDamage ();
					} else
						MoveBackward ();
				} else if (targetRange > longRange) {
					MoveForward ();
				} else if (targetRange > (midRange + 2f) && targetRange < longRange) {
					flipParam = (targetDirection > 0f) ? -1f : 1f;
					input = 0f; 
				} else if (targetRange <= (midRange + 2f) && targetRange > midRange) {
					MoveBackward ();
				} 
			}

		} else if (!alive || stunned) {
			float step = 0.01f * Time.time;
			moveSpeed = Mathf.MoveTowards (impulsePower, 0f, step);
		}

		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
		anim.SetBool ("run", Mathf.Abs (input) > 0.1f);
	}

	public override void GetDamage (){
		
		attackCheck = false;

		if (targetRange > meleeRange) {
			anim.SetTrigger ("attack");
		} else {
			anim.SetTrigger ("attack2");
		}
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

	public IEnumerator ResetAttackCheck () {
		yield return new WaitForSeconds (attackSpeed);
		attackCheck = true;
	}
		
	public override void SetDamage (float damage, float impulseDirection, bool[] attackModify){}

	public override void SetStun (float direction){}

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
	public void Idle () {

	}

	//Методы движения
	void MoveBackward () {
		flipParam = (targetDirection > 0f) ? -1f : 1f;
		input = (targetDirection < 0f) ? -1f : 1f;
	}

	void MoveForward () {
		flipParam = (targetDirection > 0f) ? -1f : 1f;
		input = (targetDirection < 0f) ? 1f : -1f;
	}
}
