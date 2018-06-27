using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	public float health = 0f;
	public float armor = 0f;
	public float attackPoints = 0f;
	public float attackSpeed = 0f;
	public float attackRange = 1f;

	public float moveSpeed;
	public float impulsePower;
	[HideInInspector]
	public float input = 0f;
	[HideInInspector]
	public Rigidbody2D rb;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public Damage damage;
	[HideInInspector]
	public Conditions conditions;
	[HideInInspector]
	public float direction = 1f;
	[HideInInspector]
	public float flipParam = 0f;

	void Start () {
		//hpBar = transform.Find ("HPBar").gameObject;

		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		conditions = GetComponent<Conditions> ();
		damage = GetComponent<Damage> ();
	}

	//Атаковать
	public virtual void Attack () {
		CheckBlock ();
		conditions.attack = true;
		anim.SetTrigger ("attack");
	}

	//Использовать щит
	public void UseShield () {
		//Если блок не включен
		if (!conditions.block) {
			//Достать щит
			conditions.EnableBlock ();
		} else
			//Убрать щит
			conditions.DisableBlock ();
		anim.SetTrigger ("block");
	}

	//Проверка на блок
	public void CheckBlock() {
		//Если игрок в блоке
		if (conditions.block) {
			//Убрать щит
			UseShield();
		}
	}

	public virtual void Run () {
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
		if (CanMove ()) {
			anim.SetFloat ("speed", Mathf.Abs (input * moveSpeed));
		}
	}

	//Проверка на возможность двигаться
	public virtual bool CanMove() {
		return (conditions.alive && !conditions.stun);
	}

	//Проверка на возможность атаковать
	public virtual bool CanAttack() {
		return (!conditions.attack && !conditions.stun);
	}

	//Зарегистрироваться в родительском стаке врагов
	[HideInInspector]
	public DangerArea myStack;
	public virtual void RegistrationInStack () {
		myStack = GetComponentInParent<DangerArea> ();
		myStack.AddEnemie (this);
	}
}
