using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level1Manager: MonoBehaviour {

	[HideInInspector]
	public Animator animator;
	public Slider healthSlider;                                 // Ссылка на UI полосы здоровья
	Slider experienceSlider;
	float velocityXSmoothing;
	GameObject MainCamera;
	public GameObject warriorInterface;
	public GameObject archerInterface;
	Slider resourceSlider;
	public Slider rageSlider;
	public Slider agilitySlider;
	public GameObject warrior;
	public GameObject archer;
	public Text warriorskillpointQuantity;
	public Text archerskillpointQuantity;
	[HideInInspector]
	public static GameObject player;
	public Text resourceName;
	public Text WarriorScoresOfSkillPoints;
	public Text ArcherScoresOfSkillPoints;
	public GameObject skillsWarrior;
	public GameObject skillsArcher;

	public CameraFollow cameratarget;

	Vector3 velocity;
	bool PreviewCheck = false;

	[HideInInspector]
	public bool startRegeneration = false;

	void Awake() {

		if (GameManager.character == 1) {
			archer.SetActive (false);
			archerInterface.SetActive (false);
			player = warrior;
			resourceSlider = rageSlider;
			resourceName.text = "Rage";
			cameratarget.player = warrior;
		}

		if (GameManager.character == 2) {
			warrior.SetActive(false);
			warriorInterface.SetActive (false);
			resourceSlider = agilitySlider;
			player = archer;
			cameratarget.player = archer;
			resourceName.text = "Agility";
		}

		MainCamera = GameObject.Find ("MainCamera");
		resourceSlider = GameObject.FindGameObjectWithTag ("ResSlider").GetComponent<Slider> ();
		experienceSlider = GameObject.Find ("Experience").GetComponent<Slider> ();

		CharacterLevelCheck ();

		experienceSlider.value = GameManager.Experience;
		GameManager.SelectedScene = 1;
	}

	void Start () {
		
		if (GameManager.character == 1) {
			healthSlider.maxValue = 100;
			resourceSlider.maxValue = 100;
		}

		if (GameManager.character == 2) {
			healthSlider.maxValue = 50;
			resourceSlider.maxValue = 100;
		}

		StartCoroutine ("Preview");

		if (GameManager.character == 1) {
			warriorskillpointQuantity.text = "Skill points: " + GameManager.SkillPointValue;
			skillsWarrior.SetActive (false);
		}

		if (GameManager.character == 2) {
			archerskillpointQuantity.text = "Skill points: " + GameManager.SkillPointValue;
			skillsArcher.SetActive (false);
		}

		GameManager.SaveGame ();
	}
	
	void Update () {

		if (PreviewCheck) {
			cameratarget.transform.Translate (velocity);
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			GameManager.SaveGame ();
		}
			
		//LevelUP!
		if (experienceSlider.value == experienceSlider.maxValue) {
			GameManager.CharacterLevel += 1;
			CharacterLevelCheck ();
			GameManager.SkillPointValue += 1;
			GameManager.Experience = 0;
			if (GameManager.character == 1) {
				warriorskillpointQuantity.text = "Skill points: " + GameManager.SkillPointValue;
			}

			if (GameManager.character == 2) {
				archerskillpointQuantity.text = "Skill points: " + GameManager.SkillPointValue;
			}

			GameManager.SaveGame ();
		}

		//Полоса здоровья, энергии и опыта
		healthSlider.value = player.GetComponent<Controller2D> ().Health;
		resourceSlider.value = player.GetComponent<Controller2D> ().Resource;
		experienceSlider.value = GameManager.Experience;

		if (GameManager.character == 1) {
			WarriorScoresOfSkillPoints.text = " " + GameManager.SkillPointValue + " ";
		}

		if (GameManager.character == 2) {
			ArcherScoresOfSkillPoints.text = " " + GameManager.SkillPointValue + " ";
		}

		if (GameManager.character == 2 && player.GetComponent<Controller2D> ().Resource < resourceSlider.maxValue && !startRegeneration) {
			startRegeneration = true;
			InvokeRepeating ("StartRegeneration", 0.3f, 0.3f);
		}
		if (GameManager.character == 2 && player.GetComponent<Controller2D> ().Resource == resourceSlider.maxValue) {
			startRegeneration = false;
			CancelInvoke ("StartRegeneration");
		}
	}

	public void StartRegeneration () {
		player.GetComponent<Controller2D> ().Resource += 1f;
	}

	public void CharacterLevelCheck () {
		switch (GameManager.CharacterLevel) {
		case 1:
			experienceSlider.maxValue = 50;
			break;
		case 2: 
			experienceSlider.maxValue = 100;
			break;
		case 3:
			experienceSlider.maxValue = 120;
			break;
		case 4:
			experienceSlider.maxValue = 140;
			break;
		case 5:
			experienceSlider.maxValue = 160;
			break;
		}
	}

	IEnumerator Preview() {


	}

}
