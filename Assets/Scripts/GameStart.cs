using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

	private GameOver gameOverScript;
	private BestDistance bestDistanceScript;
	private TitleSystem titleSystemScript;
	public PlayerController playerControllerScriptOne;
	public PlayerController playerControllerScriptTwo;

	public GameObject mrMeeseeks;
	public AudioSource soundCountdownTick;
	public AudioSource soundLevelStart;

	public Text countdownText;
	public Text levelClock;

	private float levelTime;

	private float counterTime = 3.5f;
	private float countdownZero = 0.55f;
	private float levelEndZero = 1.0f;
	private float lastSeconds = 6.0f;

	private bool startCounting;

	public static bool activatePlayerControl;
	public static bool activateCameraControl;
	public bool activateEventCountdown;
	public bool activateLevelClock;

	//STATS
	public static float countTimePlayed;


	// void Awake () {
	// 	Application.targetFrameRate = 60;
	// }


	void Start () {
		Time.timeScale = MainNavigation.setTimeScale + 0.5f;
		gameOverScript = GetComponent<GameOver>();
		bestDistanceScript = GetComponent<BestDistance>();
		titleSystemScript = GetComponent<TitleSystem>();

		activatePlayerControl = false;

		gameOverScript.darkScreen.SetActive(true);
		gameOverScript.darkScreen.GetComponent<Image>().CrossFadeAlpha(0, 1f, true);
		gameOverScript.darkScreen.SetActive(false);

		GameOver.gameOver = false;
		
		StartCoroutine(initialWait());
		// SelectedElements.setLevelTime += 1.0f;
		levelTime = SelectedElements.setLevelTime > 0 ? SelectedElements.setLevelTime : 10 ;
	}


	IEnumerator initialWait () {
		yield return new WaitForSeconds(1);
		startCounting = true;
		StartCoroutine(countdownTicking());
	}


	void Update () {
		if (startCounting) {
			if (counterTime > countdownZero) {
				counterTime -= Time.deltaTime;
				countdownText.text = Mathf.Round(counterTime) + "";
			} else {
				startCounting = false;
				StartCoroutine(countdownDone());
			}
		}

		if (activateLevelClock) {

			countTimePlayed += Time.deltaTime;

			playerControllerScriptOne.statSpeedArrayOne.Add(playerControllerScriptOne.mag);
			playerControllerScriptOne.statSpeedArrayTwo.Add(playerControllerScriptTwo.mag);

			levelClock.enabled = true;

			if (levelTime > levelEndZero) {
				levelTime -= Time.deltaTime;

				float minutes = Mathf.Floor(levelTime / 60);
				float seconds = Mathf.Floor(levelTime % 60);

				if (seconds < 10) { levelClock.text = minutes + ":0" + seconds; }
				else { levelClock.text = minutes + ":" + seconds; }

				if (levelTime <= lastSeconds) { levelClock.color = new Color(255, 0, 0); }
			} else {
				if (SelectedElements.setGameMode == 0) { bestDistanceScript.distanceWinner(); }
				else if (SelectedElements.setGameMode == 1) { gameOverScript.LevelDone(mrMeeseeks); }
			}
		}
	}


	IEnumerator countdownTicking () {
		for (int i = 0; i < 3; i++) {
			soundCountdownTick.Play();
			yield return new WaitForSeconds(1);
		}
	}


	IEnumerator countdownDone () {
		Time.timeScale = MainNavigation.setTimeScale;
		countdownText.text = "Go!";
		soundLevelStart.Play();

		titleSystemScript.collectData = true;

		activatePlayerControl = true;
		activateCameraControl = true;

		yield return new WaitForSeconds(1.4f);
		countdownText.text = "";
		activateLevelClock = true;
		activateEventCountdown = SelectedElements.randomEventsActive;
	}

}
