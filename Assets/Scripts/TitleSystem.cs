using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSystem : MonoBehaviour {

	public string titleOne;
	public string titleTwo;

	public bool collectData;

	private string[] titleList = {
		"Super Mario",
		"Touch Much?",
		"Like Dynamite",
		"Gravedigger",
		"Moonwalker",
		"Traveler",
		"One Smiley Too Many",
		"Run, Forrest, Run",
		"Chicken"
	};

	private int[] winnerList;
	List<int> winnerIndexOne = new List<int>();
	List<int> winnerIndexTwo = new List<int>();

	private float[] collectedDataOne;
	private float[] collectedDataTwo;

	private int randomTitleOne;
	private int randomTitleTwo;

	private int jumpsOne;
	private int jumpsTwo;

	private int touchedPlatformsOne;
	private int touchedPlatformsTwo;

	private int poppedHeadsOne;
	private int poppedHeadsTwo;


	void Update () {
		if (collectData) {

			collectedDataOne = new float[] {
				PlayerController.statJumpsOne,
				TouchedPlatforms.statTouchedOne,
				PoppingHeads.statPoppedHeadsOne,
				RespawnManager.statDeathsOne,
				PlayerController.statAirtimeOne,
				PlayerController.statWalkTimeOne,
				PlayerController.statSpeedOne,
				RespawnManager.statOverallDistanceOne,
				TouchedPlatforms.statRestingTimeOne
			};

			collectedDataTwo = new float[] {
				PlayerController.statJumpsTwo,
				TouchedPlatforms.statTouchedTwo,
				PoppingHeads.statPoppedHeadsTwo,
				RespawnManager.statDeathsTwo,
				PlayerController.statAirtimeTwo,
				PlayerController.statWalkTimeTwo,
				PlayerController.statSpeedTwo,
				RespawnManager.statOverallDistanceTwo,
				TouchedPlatforms.statRestingTimeTwo
			};

		}
	}

	public void compareData () {

		for (int i = 0; i < titleList.Length; i++) {
			if (collectedDataOne[i] > collectedDataTwo[i]) {
				winnerIndexOne.Add(i);
			} else if (collectedDataOne[i] < collectedDataTwo[i]) {
				winnerIndexTwo.Add(i);
			}
		}

		if (winnerIndexOne.Count <= 0) { randomTitleOne = -1; }
		else { randomTitleOne = winnerIndexOne[Random.Range(0, winnerIndexOne.Count)]; }

		if (winnerIndexTwo.Count <= 0) { randomTitleTwo = -1; }
		else { randomTitleTwo = winnerIndexTwo[Random.Range(0, winnerIndexTwo.Count)]; }


		if (randomTitleOne == -1) { titleOne = "—"; }
		else { titleOne = titleList[randomTitleOne]; }

		if (randomTitleTwo == -1) { titleTwo = "—"; }
		else { titleTwo = titleList[randomTitleTwo]; }
		
	}

}
