using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

	public string objectiveText;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public virtual string GetObjectiveText () {
		return objectiveText;
	}
}
