using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*! \brief Класс отвечает за включение/выключение различных состояний юнита

	Бывают различные виды состояний, такие как "Блок", "Атака", "Оглушение" и др.
*/
public class Conditions : MonoBehaviour {

	[HideInInspector]
	public Unit unit;   //< Ссылка на контроллер юнита
	[HideInInspector]
	public Animator anim;   //< Ссылка на контроллер анимаций

	void Start () {
		unit = GetComponent<Unit> ();
		anim = GetComponent<Animator> ();
		defaultMovespeed = unit.moveSpeed;
		defaultImpulsePower = unit.impulsePower;
	}

	//БЛОК
	[HideInInspector]
	public bool block = false;   ///< Определяет находится ли юнит в состоянии "Блок"
	///Включение состояния "Блок"
	public virtual void EnableBlock() {
		block = true;
	}
	///Выключение состояния "Блок"
	public virtual void DisableBlock() {
		block = false;
	}

	//УВОРОТ
	[HideInInspector]
	public bool invulnerability = false;   ///< Определяет находится ли юнит в состоянии "Перекат"
	///Включение состояния "Перекат"
	public virtual void EnableInvulnerability () {
		invulnerability = true;
	}
	///Выключение состояния "Перекат"
	public virtual void DisableInvulnerability () {
		invulnerability = false;
	}

	//ОГЛУШЕНИЕ
	[HideInInspector]
	public bool stun = false;   ///< Определяет находится ли юнит в состоянии "Оглушение"
	/*! Включение состояния "Оглушение"
		
		\param[in] direction направление атаки
	*/
	public virtual void EnableStun (int direction) {
		stun = true;
		unit.inputX = direction;
		SetMovespeed (unit.impulsePower);
	}
	///Выключение состояния "Оглушение"
	public virtual void DisableStun () {
		unit.inputX = 0;
		stun = false;
		SetImpulse (defaultImpulsePower);
		SetMovespeed (defaultMovespeed);
	}

	//АТАКА
	[HideInInspector]
	public bool attack = false;   ///< Определяет находится ли юнит в состоянии "Атака"
	public LayerMask attackCollision = 8;   ///< Номер слоя, на котором находятся юниты способные получить урон

	///Нанести обычный урон
	public virtual void Default_Attack () {

		RaycastHit2D hit = MeleeTargetCheck (unit.attackRange, unit.direction, attackCollision);
		if (hit) {
			hit.transform.GetComponent<Damage> ().DefaultDamage(unit.attackPoints, unit.direction);
		}
	}

	///Нанести урон игнорирующий состояние "Блок"
	public virtual void Hit_Through_The_Block () {

		RaycastHit2D hit = MeleeTargetCheck (unit.attackRange, unit.direction, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Damage> ().DamageThroughTheBlock(unit.attackPoints, unit.direction);
		}
	}

	///Нанести урон игнорирующий состояние "Уворот"
	public virtual void Hit_Ignores_The_Roll () {

		RaycastHit2D hit = MeleeTargetCheck (unit.attackRange, unit.direction, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Damage> ().DamageIgnoresTheRoll(unit.attackPoints, unit.direction);
		}
	}

	/*! Построить вектор атаки
		
		\param[in] attackRange длина вектора, определяющая дальность атаки. Зависит от параметра Unit.attackRange
		\param[in] direction направление вектора, определяющее направление атаки/удара/заклинания/снаряда
		\param[in] attackCollision номер слоя, на котором находятся враги, которые могут быть атакованы
		\return Возвращает значение RaycastHit2D, определяющее попал удар в цель или нет
	*/
	public RaycastHit2D MeleeTargetCheck(float attackRange, float direction, LayerMask attackCollision) {
		Vector2 targetVector = new Vector2 (direction, 0);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);
		//Построить луч атаки, используя входные данные
		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, targetVector, attackRange, attackCollision);
		return hit;
	}

	//Выстрел из лука
	public GameObject patron;   ///< Определяет какими снарядами стреляет юнит (стрелы, заклинания и др.)
	///Выпустить снаряд
	public virtual void Bow_Attack () {
		GameObject arrowInstance = Instantiate (patron, new Vector3 (transform.position.x, transform.position.y + 0.9f, transform.position.z), Quaternion.identity);
		Patron arrowScript = arrowInstance.GetComponent<Patron> ();
		arrowScript.SetDirection (unit.direction);
	}

	public float knockDownPower = 5f;  ///< Определяет дальность полета юнита, которому нанесли удар 
	/*! Сбить с ног
		
		\param[in] target объект нанесения удара
	*/
	public virtual void KnockDown (Collider2D target) {
		target.GetComponent<Damage> ().KnockDown (unit.attackPoints, unit.direction, knockDownPower);
	}

	///Выключить состояние "Атака" и начать отсчет до возможности снова атаковать (зависит от скорости атаки)
	public virtual IEnumerator FinishAttack () {
		yield return new WaitForSeconds (unit.attackSpeed);
		attack = false;
	}

	//_______________________________________________________________________________________________________

	
	[HideInInspector]
	public float defaultMovespeed = 1.5f;      ///< Значение скорости бега по умолчанию
	/*! Назначить скорость бега
		
		\param[in] value новое (задаваемое) значение скорости бега
	*/
	public void SetMovespeed (float value) {
		if (value >= 0f) {	
			unit.moveSpeed = value;
		}
	}

	/*! Назначить силу импульса

		\param[in] value новое (задаваемое) значение силы отталкивания
	*/
	[HideInInspector]
	public float defaultImpulsePower = 1.5f;  //Значение силы отталкивания по умолчанию

	public void SetImpulse (float value) {
		if (value >= 0f) {
			unit.impulsePower = value;
		}
	}
	
	[HideInInspector]
	public bool alive = true;   ///< Показывает умер юнит или нет
	///Включение состояния "Смерть"
	public virtual void UnitDie () {
		alive = false;
	}

	///Убрать объект со сцены
	public void DestroyUnit () {
		Destroy (gameObject);
	}
}
