using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
\brief Дочерний класс от класса Tile. Используется при создании тайлов-перекрестков
	   
	   Тайлы-перекрестки содержат переходы в отдельные зоны уровня. Ими могут быть зоны с квестовыми NPC, участками с квестовыми мобами и предметами.
	   Местоположение тайлов на уровне задается вручную, по-умолчанию на уровне генерируются тайлы с врагами в соответствии с текущим уровнем сложности.
	   На тайлах-перекрестках имеется объект "переход" (transition). Это интерактивный объект, ведущий в отельную игровую зону. 
*/
public class TileCrossroads : Tile {

	public int tilePositionOnLevel = 0;   ///< Позиция тайла-перекрестка в очереди создания обычных тайлов

	void Start () {
		CreateTransition ();
	}

	public GameObject transition;   ///< Ссылка на префаб "переход"
	public Vector2 transitionPosition;   ///< Координаты относительно данного тайла, в которых создается "переход"
	public string roadSignText;   ///< Текст на указателе, который говорит игроку, в какую зону будет выполнен переход
	public string deadlockName;   ///< Родительский объект, в которой будет помещена сгенерированная зона, в которую будет выполнен переход

	/*!
		Создание перехода по заданным параметрам
	*/
	void CreateTransition () {
		Vector2 positionToInstance = new Vector2 (transform.position.x + transitionPosition.x, transform.position.y + transitionPosition.y);
		//Создать переход
		GameObject transitionObj = Instantiate (transition, positionToInstance, Quaternion.identity);
		TransitionToQuest transitionScript = transitionObj.GetComponent<TransitionToQuest> ();
		//Передать ссылку на уровень который необходимо включить
		transitionScript.sideBG = GameObject.Find (deadlockName);
		//Передать надпись на указателе
		transitionScript.textInSign.text = roadSignText;
		//Скрыть кнопку
		transitionScript.button.SetActive(false);
	}
}
