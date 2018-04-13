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
	float impulseTime = 0.01f;

	//Ключевые расстояния
	[Header("Чеки по дальности")]
	public float longDistance = 10f;
	public float averageDistance = 7f;
	public float shortDistance = 3f;

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

		if (alive && !stunned) {
			if (Mathf.Abs (transform.position.x - target.transform.position.x) > longDistance) {
				input = (Mathf.Sign (transform.position.x - target.transform.position.x) < 0f) ? 1f : -1f;
			} else if (Mathf.Abs (transform.position.x - target.transform.position.x) <= longDistance && Mathf.Abs (transform.position.x - target.transform.position.x) > averageDistance) {
				input = (Mathf.Sign (transform.position.x - target.transform.position.x) < 0f) ? -1f : 1f;
			} else if (Mathf.Abs (transform.position.x - target.transform.position.x) <= averageDistance) {
				input = 0f;
				GetDamage ();
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

		if (Mathf.Abs (transform.position.x - target.transform.position.x) > shortDistance) {
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
			hit.transform.GetComponent<Unit> ().SetDamage (attack, direction, false);
		}
	}

	public override void SetDamage (float damage, float impulseDirection, bool piercing_attack){}

	public override void SetStun (){}

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

	void Impulse (float power, float direction, float time) {
		stunned = true;
		impulsePower = power;
		input = direction;
		impulseTime = time;
	}
}
