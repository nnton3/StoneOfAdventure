using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : Unit, IReaction<GameObject> {
	
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

	public GameObject zomby;

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

		if (!idle) {
			if (attackCheck && alive && !stunned) {
				GetDamage ();
			} else if (stunned || !alive) {
				float step = 1f * Time.time;
				moveSpeed = Mathf.MoveTowards (impulsePower, 0f, step);
			}
		}

		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	public override void GetDamage (){
		attackCheck = false;
		anim.SetTrigger ("attack");
	}

	//Сбросить чек атаки
	public IEnumerator ResetAttackCheck () {
		yield return new WaitForSeconds (attackSpeed);
		attackCheck = true;
	}

	public override void SetDamage (float damage, float impulseDirection, bool piercing_attack){
		if (health <= damage) {
			flip.enabled = false;
			input = impulseDirection;
			Die ();
			return;
		}
		anim.SetTrigger ("attackable");
		health -= damage;
	}

	public override void SetStun (){}

	public override void Die (){
		anim.SetTrigger ("die");
		start.AddCorpse ();
		alive = false;
		gameObject.layer = 2;
		gameObject.tag = "Puddle";
	}

	public void DestroyObject() {
		Destroy (gameObject);
	}

	//Начать преследование
	public void Chase (GameObject player) {
		target = player;
		input = -1f;
		//Запустить тайминг на остановку
		StartCoroutine("StopMovespeedTimer");	}

	public void StartChase() {
		gameObject.layer = 9;
		idle = false;
	}

	//Остановить преследование
	public void Idle () {
		
	}

	//Призвать зомби
	public void InvokeZomby() {
		GameObject zombyInstance = Instantiate (zomby, new Vector3 (transform.position.x + Random.Range (-3, -15), transform.position.y + 0.9f, transform.position.z), Quaternion.identity);
		Enemy_invokedZomby zombyScript = zombyInstance.GetComponent<Enemy_invokedZomby> ();
		zombyScript.target = target;
	}

	//Таймер на остановку
	IEnumerator StopMovespeedTimer () {
		yield return new WaitForSeconds (3f);
		input = 0f;
		rb.gravityScale = 1f;
		anim.SetTrigger ("stopFly");
	}
}
