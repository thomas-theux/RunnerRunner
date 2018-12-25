using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchedPlatforms : MonoBehaviour {

	public PlayerController playerControllerScriptOne;
	public PlayerController playerControllerScriptTwo;

	public static int statTouchedOne;
	public static int statTouchedTwo;

	public static float statRestingTimeOne;
	public static float statRestingTimeTwo;


	void Awake () {
		playerControllerScriptOne = GameObject.Find("One").GetComponent<PlayerController>();
		playerControllerScriptTwo = GameObject.Find("Two").GetComponent<PlayerController>();
	}


	void OnTriggerEnter (Collider other) {
		if (other.name == "One") {
			statTouchedOne++;
			// playerControllerScriptOne.comboTime += 1;
			// playerControllerScriptOne.comboCounter++;
			// playerControllerScriptOne.moveSpeed += 0.4f;
		} else if (other.name == "Two") {
			statTouchedTwo++;
			// playerControllerScriptTwo.comboTime += 1;
			// playerControllerScriptTwo.comboCounter++;
			// playerControllerScriptTwo.moveSpeed += 0.4f;
		}
	}


	void OnTriggerStay (Collider other) {
		if (other.name == "One") {
			statRestingTimeOne += Time.deltaTime;
		} else if (other.name == "Two") {
			statRestingTimeTwo += Time.deltaTime;
		}
	}

}
