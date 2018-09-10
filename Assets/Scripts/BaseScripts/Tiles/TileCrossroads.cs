using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCrossroads : Tile {
	//Позиция тайла на уровне
	public int tilePositionOnLevel = 0;


	void Start () {
		CreateTransition ();
	}

	//Создать переход
	public GameObject transition;
	public Vector2 transitionPosition;
	//Указатель: куда переход
	public string roadSignText;
	//Ссылка на уровень, куда будет сделан переход
	public string deadlockName;
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
