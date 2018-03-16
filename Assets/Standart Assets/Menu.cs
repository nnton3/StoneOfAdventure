using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	GameObject Characters;
	GameObject MainMenu;
	GameObject LevelMenu;

	public GameObject gameManager;

	void Start() {

		if (GameObject.Find ("GameManager(Clone)") == null) {
			Instantiate (gameManager);
		}

		//Объекты на сцене "Меню"
		MainMenu = GameObject.Find ("MainMenu");
		LevelMenu = GameObject.Find ("LevelMenu");
		Characters = GameObject.Find ("Characters");

		//Начальные условия сцены "Меню"
		MainMenu.SetActive (true);
		LevelMenu.SetActive (false);
		Characters.SetActive (false);

	}

	public void OnClickWarriorBtn() {
		GameManager.character = 1;
		Characters.SetActive (false);
		LevelMenu.SetActive (true);
	}

	public void OnClickArcher() {
		GameManager.character = 2;
		Characters.SetActive (false);
		LevelMenu.SetActive (true);
	}
		
	public void OnClickContinue() {
		GameManager.LoadGame ();

		if (GameManager.character != 0) {
			MainMenu.SetActive (false);
			LevelMenu.SetActive (true);
		}
	}

	public void OnClickQuit() {
		Application.Quit ();
	}
		
	public void OnClickStart() {
		GameManager.character = 0;
		GameManager.progress = 1;
		GameManager.Experience = 0;
		GameManager.CharacterLevel = 1;
		GameManager.SkillPointValue = 50;
		GameManager.ClearTalant ();
		GameManager.SaveGame ();

		MainMenu.SetActive (false);
		Characters.SetActive (true);
	}
		
	public void OnClickLevel1() {
		SceneManager.LoadScene ("Level1");
		GameManager.EnableLoadScreen();
	}

	public void OnClickLevel2() {
		//if (GameManager.progress > 1) {
		SceneManager.LoadScene ("Level2");
		GameManager.EnableLoadScreen();
		//}
	}

	public void OnClickLevel3() {
		//if (GameManager.progress > 1) {
		SceneManager.LoadScene ("Level3");
		GameManager.EnableLoadScreen();
		//}
	}

	public void OnClickLevel4() {
		//if (GameManager.progress > 1) {
		SceneManager.LoadScene ("Level4");
		GameManager.EnableLoadScreen();
		//}
	}
		
	public void OnClickTutorial() {
		SceneManager.LoadScene ("Tutorial");
	}
}
