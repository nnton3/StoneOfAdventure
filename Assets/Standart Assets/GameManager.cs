using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	//Сложность игры
	public static int complexity = 0;    //Сложность игры: определяет сложность мобов
	//Увеличить сложность
	public static void IncreaseCombplexity (int dx) {
		complexity += dx;
	}
	//Обнулить сложность
	public static void ComplexitySetToZero () {
		complexity = 0;
	}

	//Режим боя
	public static bool battleMode = false;
	public static void EnableBattleMode () {
		battleMode = true;
	}

	public static void DisableBattleMode () {
		battleMode = false;
	}
}
