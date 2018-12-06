using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
\brief Этот класс является родительским для всех юнитов в игре, в нем содержатся общие для всех свойства и методы
	   
	   Данный класс содержит общие параметры юнитов, такие как здоровье, урон, броня и др.,
	   а также некоторые часто используемые методы, такие как "Атака", "Достать/Убрать щит" и др.
*/
public abstract class Unit : MonoBehaviour {

	public float health = 0f;   ///< Общий запас здоровья юнита в данный момент
	public float armor = 0f;   ///< Показатель брони  
	public float attackPoints = 0f;   ///< Количество урона наносимого за один удар
	public float attackSpeed = 0f;   ///< Количество секунд до возможности снова произвести атаку
	public float attackRange = 1f;   ///< Дальность атаки

	public float moveSpeed;   ///< Скорость бега
	public float impulsePower;   ///< Силы с которой юнит будет отброшен при получении урона
	[HideInInspector]
	public int inputX = 0;   ///< Направление движения по Х
	[HideInInspector]
	public int inputY = 0;   ///< Направление движения по Y
	[HideInInspector]
	public Rigidbody2D rb;   ///< Ссылка на компонент Rigidbody2D
	[HideInInspector]
	public Animator anim;   ///< Ссылка на Animator
	[HideInInspector]
	public Damage damage;   ///< Ссылка на контроллер входящего урона
	[HideInInspector]
	public Conditions conditions;   ///< Ссылка на контроллер состояний юнита
	[HideInInspector]
	public int direction = 1;   ///< Переменная в которой хранится текущее направление юнита
	[HideInInspector]
	public int flipParam = 0;   ///< Переменная в которой записан параметр от которого зависит направление игрока

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		conditions = GetComponent<Conditions> ();
		damage = GetComponent<Damage> ();
		spRender = GetComponent<SpriteRenderer> ();
	}

	///Запускает анимацию атаки и переход в состояние "Атака"
	public virtual void Attack () {
		CheckBlock ();
		conditions.attack = true;
		anim.SetTrigger ("attack");
	}

	///Запускает анимацию "достать/убрать щит" и переход в/из состояния "Блок"
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

	///Проверяет, находится ли юнит в состоянии блока
	public void CheckBlock() {
		//Если игрок в блоке
		if (conditions.block) {
			//Убрать щит
			conditions.DisableBlock ();
		}
	}

	///Отвечает за передвижение юнита и включение/выключение анимации бега
	public virtual void Run () {
		rb.velocity = new Vector2 (inputX * moveSpeed, rb.velocity.y);
		if (CanMove ()) {
			anim.SetBool ("run", Mathf.Abs (inputX * moveSpeed) > 0.1f);
		}
	}

	///Проверка на возможность двигаться
	public virtual bool CanMove() {
		return (conditions.alive && !conditions.stun);
	}

	///Проверка на возможность атаковать
	public virtual bool CanAttack() {
		return (!conditions.attack && !conditions.stun);
	}

	/*! Используется только для вражеских юнитов для помещения их в триггер,
		при срабатывании которого они начинают преследовать и атаковать игрока
		\param[in] myStack ссылка на триггер в котором будет находиться юнит
	*/
	public DangerArea enemieTriggerScript;
	public virtual void RegistrationInStack () {
		enemieTriggerScript = GetComponentInParent<DangerArea> ();
		enemieTriggerScript = GetComponentInParent<DangerArea> ();
		enemieTriggerScript.AddEnemie (this);
	}

	public void StartChangeColor () {
		StopCoroutine ("ChangeColor");
		spRender.color = Color.white;
		StartCoroutine ("ChangeColor");
	}
	///Изменение цвета при получении урона
	SpriteRenderer spRender;
	IEnumerator ChangeColor () {
		Color currentColor = spRender.color;
		for (int i = 1; i <= 10; i++) {
			if (i <= 5) {
				currentColor.g -= 0.2f;
				currentColor.b -= 0.2f;
				spRender.color = currentColor;
			} else {
				currentColor.g += 0.2f;
				currentColor.b += 0.2f;
				spRender.color = currentColor;
			}
			yield return new WaitForSeconds (0.02f);
		}
	}

	public GameObject bloodPref;
	public void CreateOutputBlood (int direction) {
		GameObject blood = Instantiate (bloodPref, transform);
		Blood bloodScript = blood.GetComponent<Blood> ();
		bloodScript.Impulse (direction);
	}
}
