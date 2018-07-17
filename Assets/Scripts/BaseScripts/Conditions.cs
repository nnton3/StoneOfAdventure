using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour {

	[HideInInspector]
	public Unit unit;
	[HideInInspector]
	public Animator anim;

	void Start () {
		unit = GetComponent<Unit> ();
		anim = GetComponent<Animator> ();
		defaultMovespeed = unit.moveSpeed;
		defaultImpulsePower = unit.impulsePower;
	}

	//БЛОК
	[HideInInspector]
	public bool block = false;

	public virtual void EnableBlock() {
		block = true;
	}

	public virtual void DisableBlock() {
		block = false;
	}

	//НЕУЯЗВИМОСТЬ
	[HideInInspector]
	public bool invulnerability = false;

	public virtual void EnableInvulnerability () {
		invulnerability = true;
	}

	public virtual void DisableInvulnerability () {
		invulnerability = false;
	}

	//ОГЛУШЕНИЕ
	[HideInInspector]
	public bool stun = false;

	public virtual void EnableStun (float direction) {
		stun = true;
		unit.input = direction;
		SetMovespeed (unit.impulsePower);
	}

	public virtual void DisableStun () {
		unit.input = 0f;
		stun = false;
		SetImpulse (defaultImpulsePower);
		SetMovespeed (defaultMovespeed);
	}

	//АТАКА
	[HideInInspector]
	public bool attack = false;
	public LayerMask attackCollision = 8;        //Атакуемый слой

	//Стандартная атака
	public virtual void Default_Attack () {

		RaycastHit2D hit = MeleeTargetCheck (unit.attackRange, unit.direction, attackCollision);
		if (hit) {
			hit.transform.GetComponent<Damage> ().DefaultDamage(unit.attackPoints, unit.direction);
		}
	}

	//Удар, игнорирующий блок
	public virtual void Hit_Through_The_Block () {

		RaycastHit2D hit = MeleeTargetCheck (unit.attackRange, unit.direction, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Damage> ().DamageThroughTheBlock(unit.attackPoints, unit.direction);
		}
	}

	//Удар игнорирующий перекат
	public virtual void Hit_Ignores_The_Roll () {

		RaycastHit2D hit = MeleeTargetCheck (unit.attackRange, unit.direction, attackCollision);

		if (hit) {
			hit.transform.GetComponent<Damage> ().DamageIgnoresTheRoll(unit.attackPoints, unit.direction);
		}
	}

	//Построить луч атаки
	public RaycastHit2D MeleeTargetCheck(float attackRange, float direction, LayerMask attackCollision) {
		Vector2 targetVector = new Vector2 (direction, 0);
		Vector2 rayOrigin = new Vector2 (transform.position.x, transform.position.y + 0.7f);
		//Построить луч атаки, используя входные данные
		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, targetVector, attackRange, attackCollision);
		return hit;
	}

	//Выстрел из лука
	public GameObject patron;

	public virtual void Bow_Attack () {
		GameObject arrowInstance = Instantiate (patron, new Vector3 (transform.position.x, transform.position.y + 0.9f, transform.position.z), Quaternion.identity);
		Patron arrowScript = arrowInstance.GetComponent<Patron> ();
		arrowScript.SetDirection (unit.direction);
	}

	//Удар сбивающий с ног
	public virtual void KnockDown (Collider2D target) {
		target.GetComponent<Damage> ().KnockDown (unit.attackPoints, unit.direction);
	}

	//Завершение атаки
	public virtual IEnumerator FinishAttack () {
		yield return new WaitForSeconds (unit.attackSpeed);
		attack = false;
	}

	//_______________________________________________________________________________________________________

	//Назначить скорость бега
	[HideInInspector]
	public float defaultMovespeed = 1.5f;      //Значение по умолчанию

	public void SetMovespeed (float value) {
		if (value >= 0f) {	
			unit.moveSpeed = value;
		}
	}

	//Назначить силу импульса
	[HideInInspector]
	public float defaultImpulsePower = 1.5f;  //Значение по умолчанию

	public void SetImpulse (float value) {
		if (value >= 0f) {
			unit.impulsePower = value;
		}
	}

	//СМЕРТЬ
	[HideInInspector]
	public bool alive = true;

	public virtual void UnitDie () {
		alive = false;
	}

	//Уничтожить объект
	public void DestroyUnit () {
		Destroy (gameObject);
	}
}
