using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	private GameModeManager checkForWinnerScript;
	private GameStart gameStartScript;
	private GameOver gameOverScript;
	private TitleSystem titleSystemScript;
	private RandomEvents randomEventsScript;
	private BestDistance bestDistanceScript;
	public PlayerController playerControllerScript;
	public RespawnManager respawnManagerScript;
	public CameraController cameraControllerScriptOne;
	public CameraController cameraControllerScriptTwo;

	private Image darkScreenComponent;

	public GameObject playerOne;
	public GameObject playerTwo;

	public AudioSource levelEndSound;

	public Image whiteOverlay;
	private float whiteOverlayTransparency;
	private float fadeSpeedOverlay;
	private float fadeSpeedText;
	private float fadeOutWait;
	private float loserDelay;

	public Text winnerText;
	public Text loserText;

	public Text whiteText;
	public Text blackText;

	public GameObject statsParent;

	private Transform winnerPosition;
	private Transform loserPosition;

	public GameObject statsGrid;

	public static bool gameOver;
	public GameObject darkScreen;


	void Start () {
		checkForWinnerScript = GetComponent<GameModeManager>();
		gameStartScript = GetComponent<GameStart>();
		gameOverScript = GetComponent<GameOver>();
		titleSystemScript = GetComponent<TitleSystem>();
		randomEventsScript = GetComponent<RandomEvents>();
		bestDistanceScript = GetComponent<BestDistance>();
		
		darkScreenComponent = darkScreen.GetComponent<Image>();

		fadeSpeedOverlay = 2.2f;
		fadeSpeedText = 2.0f;
		fadeOutWait = 0.01f;
		loserDelay = 0.1f;
		whiteOverlayTransparency = 0.9f;
	}


	public void LevelDone (GameObject levelWinner) {
		bestDistanceScript.setDistance(playerOne);
		bestDistanceScript.setDistance(playerTwo);

		Time.timeScale = MainNavigation.setTimeScale;

		// StartCoroutine(respawnManagerScript.DeathProcess(playerOne));
		// StartCoroutine(respawnManagerScript.DeathProcess(playerTwo));

		gameStartScript.levelClock.text = "";
		gameStartScript.activateLevelClock = false;
		PauseMenu.enablePauseMenu = false;

		gameStartScript.activateEventCountdown = false;
		randomEventsScript.eventCard.enabled = false;

		titleSystemScript.collectData = false;
		playerControllerScript.getSpeedData();
		titleSystemScript.compareData();

		GameStart.activatePlayerControl = false;
		gameOver = true;

		levelEndSound.Play();

		gameStartScript.activateEventCountdown = false;
		gameOverScript.enabled = false;

		if (levelWinner.name == "Meeseeks") { checkForWinnerScript.winnerIs = 0; }
		else if (levelWinner.name == "One") { checkForWinnerScript.winnerIs = 1; }
		else if (levelWinner.name == "Two") { checkForWinnerScript.winnerIs = 2; }

		OverallStats.addToOverallStats();

		SaveLoad.saveStats();
		SaveLoad.loadStats();

		StartCoroutine(showStats());
	}


	IEnumerator showStats () {
		yield return new WaitForSeconds(0.8f);

		whiteOverlay.enabled = true;

		while (whiteOverlay.color.a < whiteOverlayTransparency) {
			whiteOverlay.color = new Color(whiteOverlay.color.r, whiteOverlay.color.g, whiteOverlay.color.b, Mathf.MoveTowards(whiteOverlay.color.a, whiteOverlayTransparency, fadeSpeedOverlay * Time.deltaTime));
			yield return new WaitForSeconds(fadeOutWait);
		}

		yield return new WaitForSeconds(0.1f);

		StartCoroutine(showWinnerText());
	}


	IEnumerator showWinnerText () {

		if (checkForWinnerScript.winnerIs == 0) {
			winnerText.text = "You suck!";
			loserText.text = "You suck!";
			winnerText.transform.position = new Vector2(100, 840);
			loserText.transform.position = new Vector2(1388, 840);
		} else if (checkForWinnerScript.winnerIs == 1) {
			winnerText.transform.position = new Vector2(100, 840);
			loserText.transform.position = new Vector2(1388, 840);
		} else if (checkForWinnerScript.winnerIs == 2) {
			winnerText.transform.position = new Vector2(1388, 840);
			loserText.transform.position = new Vector2(100, 840);
		}

		winnerText.enabled = true;
		loserText.enabled = true;
		whiteText.enabled = true;
		blackText.enabled = true;

		while (winnerText.color.a < 1f) {
			winnerText.color = new Color(winnerText.color.r, winnerText.color.g, winnerText.color.b, Mathf.MoveTowards(winnerText.color.a, 1f, fadeSpeedText * Time.deltaTime));
			whiteText.color = new Color(whiteText.color.r, whiteText.color.g, whiteText.color.b, Mathf.MoveTowards(whiteText.color.a, 1f, fadeSpeedText * Time.deltaTime));
			yield return new WaitForSeconds(fadeOutWait);
		}

		yield return new WaitForSeconds(loserDelay);

		while (loserText.color.a < 1f) {
			loserText.color = new Color(loserText.color.r, loserText.color.g, loserText.color.b, Mathf.MoveTowards(loserText.color.a, 1f, fadeSpeedText * Time.deltaTime));
			blackText.color = new Color(blackText.color.r, blackText.color.g, blackText.color.b, Mathf.MoveTowards(blackText.color.a, 1f, fadeSpeedText * Time.deltaTime));
			yield return new WaitForSeconds(fadeOutWait);
		}

		yield return new WaitForSeconds(loserDelay);

		while (Vector3.Distance(statsParent.transform.position, new Vector3(statsParent.transform.position.x, 1320, 0)) > 0) {
			statsParent.transform.position = Vector3.MoveTowards(statsParent.transform.position, new Vector3(statsParent.transform.position.x, 1320, 0), 2000f * Time.deltaTime);
			yield return new WaitForSeconds(fadeOutWait);
		}

		Time.timeScale = 0;

		statsGrid.SetActive(true);

	}


	public IEnumerator resetEverything (string navigateTo) {
		Time.timeScale = MainNavigation.setTimeScale;

		darkScreen.SetActive(true);
		darkScreenComponent.CrossFadeAlpha(0, 0f, true);
		darkScreenComponent.CrossFadeAlpha(1, 0.5f, true);

		yield return new WaitForSeconds(0.5f);

		GameOver.gameOver = false;
		whiteOverlay.enabled = false;
		winnerText.enabled = false;
		loserText.enabled = false;
		whiteText.enabled = false;
		blackText.enabled = false;
		PauseMenu.enablePauseMenu = true;

		// Reset stats
		BestDistance.statBestDistanceOne = 0;
		BestDistance.statBestDistanceTwo = 0;
		PlayerController.statJumpsOne = 0;
		PlayerController.statJumpsTwo = 0;
		RespawnManager.statDeathsOne = 0;
		RespawnManager.statDeathsTwo = 0;
		RespawnManager.statOverallDistanceOne = 0;
		RespawnManager.statOverallDistanceTwo = 0;
		PoppingHeads.statPoppedHeadsOne = 0;
		PoppingHeads.statPoppedHeadsTwo = 0;
		GameStart.countTimePlayed = 0;

		statsGrid.SetActive(false);

		if (navigateTo == "Main Menu") { SceneManager.LoadScene("1 Main Menu"); }
		else if (navigateTo == "Restart Level") { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

		yield return null;
	}

}