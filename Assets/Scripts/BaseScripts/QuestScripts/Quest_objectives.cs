using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_objectives : MonoBehaviour {

	//Найти предмет (предметы) или убить врага (врагов)
	bool allTargetObjectsFound = false;
	public GameObject targetObject;
	int currentNumberOfObjects = 0;
	public int totalNumberOfObjects = 0;

	public void TargetObjectFound () {
		currentNumberOfObjects++;
		if (currentNumberOfObjects == totalNumberOfObjects) {
			allTargetObjectsFound = true;
		}
	}
}
