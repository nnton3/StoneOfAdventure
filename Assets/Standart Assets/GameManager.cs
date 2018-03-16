using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	static int i = 0;
	static int j = 0;

	//Сцены
	public static int SelectedScene;       //Запущенная сцена
	public static LoadScreen LoadScreen;
	public static EndWindow EndWindow;

	//Выбор персонажа
	public static int character = 0;

	//Прогресс прохождения
	public static int progress = 1;

	//Текущее значение опыта
	public static int Experience = 0;

	//Уровень персонажа
	public static int CharacterLevel = 1;

	//Количество доступных очков талантов для прокачки
	public static int SkillPointValue = 0;

	//Ссылка на объект игрока
	public static GameObject player;

	//Массив распределения очков талантов
	static int[] TalantsScores;

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start() {
		TalantsScores = new int[18];
		LoadScreen = transform.Find ("Canvas").GetComponent<LoadScreen> ();
		EndWindow = transform.Find ("Canvas").GetComponent<EndWindow> ();
	}

	public static void SaveGame() {
		PlayerPrefs.SetInt("character", character);
		PlayerPrefs.SetInt("progress", progress);
		PlayerPrefs.SetInt("Experience", Experience);
		PlayerPrefs.SetInt("CharacterLevel", CharacterLevel);
		PlayerPrefs.SetInt("SkillPointValue", SkillPointValue);

		for (int a = 1; a < 18; a++) {
			string name = "talant" + a;
			PlayerPrefs.SetInt (name, TalantsScores [a - 1]);
		}
	}

	public static void LoadGame() {
		PlayerPrefs.GetInt("character", character);
		PlayerPrefs.GetInt("progress", progress);
		PlayerPrefs.GetInt("Experience", Experience);
		PlayerPrefs.GetInt("CharacterLevel", CharacterLevel);
		PlayerPrefs.GetInt("SkillPointValue", SkillPointValue);
	}

	public static void SaveTalant(int talantNumber) {
		TalantsScores [j] = talantNumber;
		j++;
	}

	public static int LoadTalant(int talantNumber) {

		int TalantLevel = 0;

		foreach (int value in TalantsScores)
		{
			if (value == talantNumber) {
				TalantLevel += 1;
			}
		}
		return TalantLevel;
	}

	public static void ClearTalant() {
		for (int a = 1; a < 18; a++) {
			TalantsScores [a - 1] = 0;
		}
	}
		
	public static void EnableLoadScreen() {
		LoadScreen.EnableLoadScreenOb ();
	}

	public static void GameEnd() {
		SceneManager.LoadScene ("Menu");
		EndWindow.GameEnd ();
	}

	public static void LoadNewLvl() {
		LoadScreen.EnableLoadScreenOb ();
		SceneManager.LoadScene ("Level" + (SelectedScene + 1));
	}
}
