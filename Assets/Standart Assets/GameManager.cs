using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//Сложность игры
	public static int complexity = 3;    //Сложность игры: определяет сложность мобов
	//Увеличить сложность
	public static void IncreaseCombplexity (int dx) {
		complexity += dx;
	}
	//Обнулить сложность
	public static void ComplexitySetToZero () {
		complexity = 0;
	}


}
