using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverallStats : MonoBehaviour {

	public static float timePlayed;
	public static int matches;
	public static float overallDistance;
	public static int jumps;
	public static int deaths;
	public static int poppedHeads;
	public static int touchedPlatforms;

	public Text timePlayedText;
	public Text matchesText;
	public Text overallDistanceText;
	public Text jumpsText;
	public Text deathsText;
	public Text poppedHeadsText;
	public Text touchedPlatformsText;


	void OnEnable() {
		// Load all the saved stats
		SaveLoad.loadStats();

		// Show overall played time
		int tempTimePlayed = Mathf.RoundToInt(timePlayed);
		float minutes = Mathf.Floor(tempTimePlayed / 60);
		float seconds = tempTimePlayed % 60;
		if (seconds < 10) { timePlayedText.text = minutes + ":0" + seconds + " min"; }
		else { timePlayedText.text = minutes + ":" + seconds + " min"; }

		// Show how many matches have been played
		matchesText.text = matches + "";

		// Show the overall walked distance
		float kilometers = overallDistance;
		if (overallDistance < 1000) {
			kilometers = Mathf.Floor(kilometers);
			overallDistanceText.text = kilometers + " m";
		} else {
			kilometers = overallDistance / 1000;
			kilometers = Mathf.Round(kilometers * 100f) / 100f;
			overallDistanceText.text = kilometers + " km";
		}
		
		// Show overall jumps
		jumpsText.text = jumps + "";
		// Show all deaths
		deathsText.text = deaths + "";
		// Show how many heads you've popped
		poppedHeadsText.text = poppedHeads + "";
		// Show how many platforms have been touched
		touchedPlatformsText.text = touchedPlatforms + "";
	}


	public static void addToOverallStats () {
		timePlayed += GameStart.countTimePlayed;
		matches++;
		overallDistance += RespawnManager.statOverallDistanceOne + RespawnManager.statOverallDistanceTwo;
		jumps += PlayerController.statJumpsOne + PlayerController.statJumpsTwo;
		deaths += RespawnManager.statDeathsOne + RespawnManager.statDeathsTwo;
		poppedHeads += PoppingHeads.statPoppedHeadsOne + PoppingHeads.statPoppedHeadsTwo;
		touchedPlatforms += TouchedPlatforms.statTouchedOne + TouchedPlatforms.statTouchedTwo;
	}

}
