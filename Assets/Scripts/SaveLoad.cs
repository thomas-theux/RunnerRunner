using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoad {

	public static void saveStats () {
		PlayerPrefs.SetFloat("saveTimePlayed", OverallStats.timePlayed);
		PlayerPrefs.SetInt("saveMatches", OverallStats.matches);
		PlayerPrefs.SetFloat("saveOverallDistance", OverallStats.overallDistance);
		PlayerPrefs.SetInt("saveJumps", OverallStats.jumps);
		PlayerPrefs.SetInt("saveDeaths", OverallStats.deaths);
		PlayerPrefs.SetInt("savePoppedHeads", OverallStats.poppedHeads);
		PlayerPrefs.SetInt("saveTouchedPlatforms", OverallStats.touchedPlatforms);
	}


	public static void loadStats () {
		OverallStats.timePlayed = PlayerPrefs.GetFloat("saveTimePlayed");
		OverallStats.matches = PlayerPrefs.GetInt("saveMatches");
		OverallStats.overallDistance = PlayerPrefs.GetFloat("saveOverallDistance");
		OverallStats.jumps = PlayerPrefs.GetInt("saveJumps");
		OverallStats.deaths = PlayerPrefs.GetInt("saveDeaths");
		OverallStats.poppedHeads = PlayerPrefs.GetInt("savePoppedHeads");
		OverallStats.touchedPlatforms = PlayerPrefs.GetInt("saveTouchedPlatforms");
	}


	public static void resetStats () {
		PlayerPrefs.SetFloat("saveTimePlayed", 0);
		PlayerPrefs.SetInt("saveMatches", 0);
		PlayerPrefs.SetFloat("saveOverallDistance", 0);
		PlayerPrefs.SetInt("saveJumps", 0);
		PlayerPrefs.SetInt("saveDeaths", 0);
		PlayerPrefs.SetInt("savePoppedHeads", 0);
		PlayerPrefs.SetInt("saveTouchedPlatforms", 0);
	}
}
