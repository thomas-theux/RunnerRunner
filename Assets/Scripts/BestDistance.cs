using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestDistance : MonoBehaviour {

	private GameOver gameOverScript;
	public LevelGenerator levelGeneratorScript;

	// public GameObject startingPoint;
	private float bestPosition;
	private float startPosition;

	private GameObject sprintWinner;

	// STATS
	public static float statBestDistanceOne;
	public static float statBestDistanceTwo;


	void Start () {
		gameOverScript = GetComponent<GameOver>();

		statBestDistanceOne = 0;
		statBestDistanceTwo = 0;

		bestPosition = levelGeneratorScript.playerSpawnPoint.transform.position.z;
		startPosition = levelGeneratorScript.playerSpawnPoint.transform.position.z;
	}

	void Update () {
	}


	public void setDistance (GameObject whoSetsDistance) {

		GameObject getSelf = whoSetsDistance.GetComponent<BestDistanceObjects>().bestSelf;
		// GameObject getOther = whoSetsDistance.GetComponent<BestDistanceObjects>().bestOther;

		bestPosition = getSelf.transform.position.z;

		float currentPosition = whoSetsDistance.transform.position.z;

		if (SelectedElements.setGameMode == 0) {
			if (currentPosition > bestPosition) {
				
				float tempPosition = Vector3.Distance(new Vector3(0, 0, currentPosition), new Vector3(0, 0, startPosition));
				if (tempPosition > bestPosition && currentPosition > startPosition) {
					bestPosition = tempPosition;

					if (!getSelf.activeSelf) { getSelf.SetActive(true); }
					// if (!getOther.activeSelf) { getOther.SetActive(true); }

					getSelf.transform.position = new Vector3(getSelf.transform.position.x, getSelf.transform.position.y, currentPosition);
					// getOther.transform.position = new Vector3(getSelf.transform.position.x, getSelf.transform.position.y, currentPosition);

					if (whoSetsDistance.name == "One") { statBestDistanceOne = bestPosition; } else
					if (whoSetsDistance.name == "Two") { statBestDistanceTwo = bestPosition; }
				}
			}
		}

		if (whoSetsDistance.name == "One") {
			RespawnManager.statOverallDistanceOne += Vector3.Distance(new Vector3(0, 0, currentPosition), new Vector3(0, 0, startPosition));
		} else if (whoSetsDistance.name == "Two") {
			RespawnManager.statOverallDistanceTwo += Vector3.Distance(new Vector3(0, 0, currentPosition), new Vector3(0, 0, startPosition));
		}

	}


	public void distanceWinner () {

		if (statBestDistanceOne > statBestDistanceTwo) {
			sprintWinner = GameObject.Find("One");
		} else if (statBestDistanceTwo > statBestDistanceOne) {
			sprintWinner = GameObject.Find("Two");
		} else {
			sprintWinner = GameObject.Find("Meeseeks");
		}

		gameOverScript.LevelDone(sprintWinner);
	}

}