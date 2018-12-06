using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
\brief Класс объекта "лифт"
*/
public class Elevator : MonoBehaviour {

	Rigidbody2D rb;   ///< Ссылка на Rigidbody2D
	float direction = 0;   ///< Переменная в которой хранится направление движения лифта
	public float movespeed = 2f;   ///< Скорость движения лифта

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.velocity = new Vector2 (rb.velocity.x, direction * movespeed);
	}
	///Запустить лифт
	public void StartMove () {
		direction = 1;
	}
	///Остановить лифт
	public void StopMove (float input_direction) {
		direction = 0f;
	}

	void OnTriggerEnter2D (Collider2D stopIndicator) {
		if (stopIndicator.CompareTag ("Indicator")) {
			direction = 0f;
			movespeed *= -1;
			return;
		}
	}
}
