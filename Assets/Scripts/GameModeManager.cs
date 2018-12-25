using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour {

	private BestDistance bestDistanceScript;
	private PoppingHeads poppingHeadsScript;
	private RandomEvents randomEventsScript;

	public static bool bestDistanceActive;
	public static bool sprintRaceActive;

	public GameObject sprintGoalTrigger;

	public int winnerIs;


	void Start () {
		bestDistanceScript = GetComponent<BestDistance>();
		poppingHeadsScript = GetComponent<PoppingHeads>();
		randomEventsScript = GetComponent<RandomEvents>();

		gameMode();
	}


	public void gameMode () {

		switch (SelectedElements.setGameMode) {
			case 0:
				bestDistanceActive = true;
				sprintRaceActive = false;
				bestDistanceScript.enabled = true;
				poppingHeadsScript.enabled = true;
				sprintGoalTrigger.SetActive(false);
				break;
			case 1:
				bestDistanceActive = false;
				sprintRaceActive = true;
				bestDistanceScript.enabled = true;
				poppingHeadsScript.enabled = false;
				SelectedElements.drunkModeActive = false;
				sprintGoalTrigger.SetActive(true);
				break;
		}
		
		// if (SelectedElements.setGameMode == 1) { poppingHeadsScript.enabled = true; }
		// else { poppingHeadsScript.enabled = false; }

		if (SelectedElements.randomEventsActive) { randomEventsScript.enabled = true; }
		else { randomEventsScript.enabled = false; }

	}

}