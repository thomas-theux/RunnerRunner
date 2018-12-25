using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour {

	public GameObject gameManagerObject;

	private PoppingHeads poppingHeadsScript;
	private BestDistance bestDistanceScript;

	public static bool stopIncreasingOne;
	public static bool stopIncreasingTwo;
	public GameObject deathEffect;
	public GameObject spawnPoint;

	public Image blackScreenOne;
	public Image blackScreenTwo;

	private float fadeOutWait;
	private float fadeInWait;
	public float fadeSpeed;

	private float invincibilityLength;
	private float invincibilityCounter;
	private float flashCounter;
	private float flashLength;
	private float flashingWait;

	public Vector3 defaultHeadSize;


	// STATS
	public static int statDeathsOne;
	public static int statDeathsTwo;
	public static float statOverallDistanceOne;
	public static float statOverallDistanceTwo;


	void Start () {
        defaultHeadSize = new Vector3(1.0f, 1.0f, 1.0f);

		fadeOutWait = 0.01f;
		fadeInWait = 0.001f;
		fadeSpeed = 2.0f;

		invincibilityLength = 1.0f;
		flashLength = 0.1f;
		flashingWait = 0.08f;

		if(gameManagerObject) {
			if(gameManagerObject.GetComponent<BestDistance>()) {
				bestDistanceScript = gameManagerObject.GetComponent<BestDistance>();
			}
			if(gameManagerObject.GetComponent<PoppingHeads>()) {
				poppingHeadsScript = gameManagerObject.GetComponent<PoppingHeads>();
			}
		}
		
	}

	void OnTriggerExit (Collider other) {
		bestDistanceScript.setDistance(other.gameObject);
		StartCoroutine(DeathProcess(other.gameObject));
	}


	public IEnumerator DeathProcess (GameObject whoDies) {
		
		// Stop increasing the Head size if the game mode is set to Sprint
		// if (GameModeManager.sprintRaceActive) {
		// 	if (whoDies.name == "One") {
		// 		stopIncreasingOne = true;
		// 	} else if (whoDies.name == "Two") {
		// 		stopIncreasingTwo = true;
		// 	}
		// }

		// Reset combo
		// whoDies.GetComponent<PlayerController>().comboTime = 0;
		// whoDies.GetComponent<PlayerController>().comboCounterText.text = "";
		// whoDies.GetComponent<PlayerController>().comboText.enabled = false;

		// If game mode is Best Distance then call the script
		if (GameModeManager.bestDistanceActive) { bestDistanceScript.setDistance(whoDies); }
		

		Instantiate(deathEffect, whoDies.transform.position, whoDies.transform.rotation);

		whoDies.GetComponent<PlayerController>().pivot.transform.parent.GetComponent<CameraController>().disableCameraMovement = true;
		//whoDies.GetComponent<PlayerController>().enabled = false;
		whoDies.transform.GetChild(0).GetComponent<Animator>().enabled = false;
		whoDies.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		

		yield return new WaitForSeconds(0.5f);

		if (whoDies.name == "One") {
			statDeathsOne++;
			while (blackScreenOne.color.a < 1f) {
				blackScreenOne.color = new Color(blackScreenOne.color.r, blackScreenOne.color.g, blackScreenOne.color.b, Mathf.MoveTowards(blackScreenOne.color.a, 1f, fadeSpeed * Time.deltaTime));
				yield return new WaitForSeconds(fadeOutWait);
			}
		} else if (whoDies.name == "Two") {
			statDeathsTwo++;
			while (blackScreenTwo.color.a < 1f) {
				blackScreenTwo.color = new Color(blackScreenTwo.color.r, blackScreenTwo.color.g, blackScreenTwo.color.b, Mathf.MoveTowards(blackScreenTwo.color.a, 1f, fadeSpeed * Time.deltaTime));
				yield return new WaitForSeconds(fadeOutWait);
			}
		}

		ResetPosition(whoDies);

		ResetHead(whoDies);

		whoDies.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
		whoDies.transform.GetChild(0).GetComponent<Animator>().enabled = true;
		whoDies.GetComponent<PlayerController>().enabled = true;
		whoDies.GetComponent<PlayerController>().pivot.transform.parent.GetComponent<CameraController>().disableCameraMovement = false;

		StartCoroutine(Invincible(whoDies));

		if (whoDies.name == "One") {
			while (blackScreenOne.color.a > 0f) {
				blackScreenOne.color = new Color(blackScreenOne.color.r, blackScreenOne.color.g, blackScreenOne.color.b, Mathf.MoveTowards(blackScreenOne.color.a, 0f, fadeSpeed * Time.deltaTime));
				yield return new WaitForSeconds(fadeInWait);
			}
		} else if (whoDies.name == "Two") {
			while (blackScreenTwo.color.a > 0f) {
				blackScreenTwo.color = new Color(blackScreenTwo.color.r, blackScreenTwo.color.g, blackScreenTwo.color.b, Mathf.MoveTowards(blackScreenTwo.color.a, 0f, fadeSpeed * Time.deltaTime));
				yield return new WaitForSeconds(fadeInWait);
			}
		}

		yield return new WaitForSeconds(0.3f);

		// if (GameModeManager.sprintRaceActive) {
		// 	if (whoDies.name == "One") { stopIncreasingOne = false; }
		// 	else if (whoDies.name == "Two") { stopIncreasingTwo = false; }
		// }
	}


	void ResetPosition (GameObject whoDies) {
		if (SelectedElements.setGameMode == 0) { whoDies.transform.position = spawnPoint.transform.position; }
		else {
			if (whoDies.name == "One") { whoDies.transform.position = CheckpointTrigger.checkpointPosOne; }
			else if (whoDies.name == "Two") { whoDies.transform.position = CheckpointTrigger.checkpointPosTwo; }
		}
		
		
		whoDies.transform.GetChild(0).transform.rotation = Quaternion.identity;

		// Resetting the camera rotation
		// if (whoDies.name == "One") {
		// 	GameObject resetWhichCamera = GameObject.Find("Pivot One");
		// 	resetWhichCamera.transform.rotation = Quaternion.identity;
		// }
		// else if (whoDies.name == "Two") { 
		// 	GameObject resetWhichCamera = GameObject.Find("Pivot Two");
		// 	resetWhichCamera.transform.rotation = Quaternion.identity;
		//  }
		
		
	}


	void ResetHead (GameObject whoDies) {
		if (whoDies.name == "One") { poppingHeadsScript.headOne.transform.localScale = defaultHeadSize; }
		else if (whoDies.name == "Two") { poppingHeadsScript.headTwo.transform.localScale = defaultHeadSize; }
		
	}


	IEnumerator Invincible (GameObject whoDies) {
		invincibilityCounter = invincibilityLength;
		flashCounter = flashLength;

		Renderer playerHeadOther = whoDies.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Renderer>();
		Renderer playerBodyOther = whoDies.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Renderer>();
		Renderer playerHeadSelf = whoDies.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Renderer>();
		Renderer playerBodySelf = whoDies.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Renderer>();

		while (invincibilityCounter > 0) {
			invincibilityCounter -= Time.deltaTime;

			flashCounter -= Time.deltaTime;
			if (flashCounter <= 0) {
				playerHeadOther.enabled = !playerHeadOther.enabled;
				playerBodyOther.enabled = !playerBodyOther.enabled;
				playerHeadSelf.enabled = !playerHeadSelf.enabled;
				playerBodySelf.enabled = !playerBodySelf.enabled;
				flashCounter = flashLength;
				yield return new WaitForSeconds(flashingWait);
			}

			if (invincibilityCounter <= 0) {
				playerHeadOther.enabled = true;
				playerBodyOther.enabled = true;
				playerHeadSelf.enabled = true;
				playerBodySelf.enabled = true;
			}
		}
	}

}
