using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintGoal : MonoBehaviour {

	public GameOver gameOverScript;


	void OnTriggerEnter (Collider other){
		gameOverScript.LevelDone(other.gameObject);
	}
}
