using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

[System.Serializable]
public class Param {

	public int character;
	public int progress;
	public int Experience;
	public int CharacterLevel;
	public int SkillPointValue;
}

public class SaveSystem : MonoBehaviour {

	[HideInInspector]
	public int character;
	[HideInInspector]
	public int progress;
	[HideInInspector]
	public int Experience;
	[HideInInspector]
	public int CharacterLevel;
	[HideInInspector]
	public int SkillPointValue;


	void Start () {

	}

	void Update () {

	}

	public void SaveParams() {
		var xml = new XmlSerializer (typeof(Param));
		var param = new Param();

		param.character = character;
		param.progress = progress;
		param.Experience = Experience;
		param.CharacterLevel = CharacterLevel;
		param.SkillPointValue = SkillPointValue;

		using (var stream = new FileStream ("Param.xml", FileMode.Create, FileAccess.Write)) {
			xml.Serialize (stream, param);
		}
	}


	//Загрузка
	public void LoadParams() {
		var xml = new XmlSerializer (typeof(Param));
		var param = new Param ();

		using (var stream = new FileStream ("Param.xml", FileMode.Open, FileAccess.Read)) {

			param = xml.Deserialize (stream) as Param;

		}

		GameManager.character = param.character;
		GameManager.progress = param.progress;
		GameManager.Experience = param.Experience;
		GameManager.CharacterLevel = param.CharacterLevel;
		GameManager.SkillPointValue = param.SkillPointValue;

	}
}



