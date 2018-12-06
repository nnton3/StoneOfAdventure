using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

	public string descriptionText;
	public string objectiveText;
	public int ID = 0;
	public int progress = 0;
	public int target = 0;
	public bool objectivesComplete = false;
	public bool questFinish = false;
}
