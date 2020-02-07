using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainNavigation : MonoBehaviour {

	private EnabledOptions enabledOptionsScript;

	public static float setTimeScale = 1.0f;

	private bool mainMenuActive;
	public GameObject eventSystem;

	private bool startMenuActive;
	private bool splitScreenMenuActive;
	private bool statsMenuActive;
	private bool optionsMenuActive;

	private string recentlyVisited;

	private GameObject startMenu;
	private GameObject splitScreenMenu;
	private GameObject statsMenu;
	private GameObject optionsMenu;

	public GameObject quitDialog;

	public GameObject statsManagerActivate;

    public EventSystem firstSelectedItem;

	public GameObject startFirstItemSplit;
	public GameObject startFirstItemStats;
	public GameObject startFirstItemOptions;
	public GameObject splitScreenFirstItem;
	public GameObject statsFirstItem;
	public GameObject optionsFirstItem;

	public GameObject logo;
	public Text menuTitle;
	public Text cancelText;

	public static bool stopNavigationInput;

	public bool waitingTimeActive;
	private bool showDialog;

	private bool levelLoaded;


	// Set single GameObjects to variables
	void Start () {
		enabledOptionsScript = GetComponent<EnabledOptions>();

		Time.timeScale = setTimeScale;

		startMenu = GameObject.Find("Start Menu");
		splitScreenMenu = GameObject.Find("Split Screen Menu");
		statsMenu = GameObject.Find("Stats Menu");
		optionsMenu = GameObject.Find("Options Menu");

		startMenuActive = true;

		startMenu.SetActive(true);
		splitScreenMenu.SetActive(false);
		statsMenu.SetActive(false);
		optionsMenu.SetActive(false);
	}


	void Update () {
		// Check if the player presses the cancel button
		if (Input.GetButtonDown("[One] Circle") && !showDialog && !stopNavigationInput) {
			if (startMenuActive) {

				stopNavigationInput = true;
				showDialog = true;
				eventSystem.SetActive(false);
				quitDialog.SetActive(true);

				StartCoroutine(waitForDialog());


			} else {
				startScreen();
			}
		}

		if (splitScreenMenuActive) {
			if (enabledOptionsScript.getGamepads.Length == 1) { menuTitle.text = "SINGLE\nPLAYER"; }
			else { menuTitle.text = "SPLIT\nSCREEN"; }
		}

		// Show choices when quit dialog is open
		if (!waitingTimeActive && showDialog) {
			if (Input.GetButtonDown("[One] X")) {
				Application.Quit();
			} else if (Input.GetButtonDown("[One] Circle")) {
				showDialog = false;
				quitDialog.SetActive(false);
				eventSystem.SetActive(true);
				stopNavigationInput = false;
			}
		}

		if (splitScreenMenuActive && Input.GetButtonDown("[One] Options")) {
			StartCoroutine(loadLevel());
		}

		// Start the level after it has "loaded"
		if (levelLoaded) {
			levelLoaded = false;
			PauseMenu.enablePauseMenu = true;
			SceneManager.LoadScene("3 Level");
		}

	}

	public void loadLevelCo () {
		StartCoroutine(loadLevel());
	}

	// "Loading" the level
	IEnumerator loadLevel () {
		yield return new WaitForSeconds(1);
		levelLoaded = true;
	}


	IEnumerator waitForDialog () {
		waitingTimeActive = true;
		yield return new WaitForSeconds(0.1f);
		waitingTimeActive = false;
	}


	// Functions that get called when navigating

	public void startScreen () {
		startMenuActive = true;
		splitScreenMenuActive = false;
		statsMenuActive = false;
		optionsMenuActive = false;

		statsManagerActivate.SetActive(false);

		// Activate & deactivate UI sounds
		SoundManager.uiHorizontalSound = false;
		SoundManager.uiVerticalSound = true;
		SoundManager.uiSelectSound = true;
		SoundManager.uiCancelSound = true;
		SoundManager.startGameSound = false;

		activateMenus();
	}

	public void splitScreen () {
		startMenuActive = false;
		splitScreenMenuActive = true;
		statsMenuActive = false;
		optionsMenuActive = false;

		recentlyVisited = "Split Screen";

		// Activate & deactivate UI sounds
		SoundManager.uiHorizontalSound = true;
		SoundManager.uiVerticalSound = true;
		SoundManager.uiSelectSound = true;
		SoundManager.uiCancelSound = true;
		SoundManager.startGameSound = true;

		activateMenus();
	}

	public void statsScreen () {
		startMenuActive = false;
		splitScreenMenuActive = false;
		statsMenuActive = true;
		optionsMenuActive = false;

		recentlyVisited = "Stats";
		statsManagerActivate.SetActive(true);

		// Activate & deactivate UI sounds
		SoundManager.uiHorizontalSound = false;
		SoundManager.uiVerticalSound = false;
		SoundManager.uiSelectSound = true;
		SoundManager.uiCancelSound = true;
		SoundManager.startGameSound = false;

		activateMenus();
	}

	public void optionsScreen () {
		startMenuActive = false;
		splitScreenMenuActive = false;
		statsMenuActive = false;
		optionsMenuActive = true;

		recentlyVisited = "Options";

		// Activate & deactivate UI sounds
		SoundManager.uiHorizontalSound = false;
		SoundManager.uiVerticalSound = true;
		SoundManager.uiSelectSound = true;
		SoundManager.uiCancelSound = true;
		SoundManager.startGameSound = false;

		activateMenus();
	}


	// Activating and deactivating the proper menu screens

	public void activateMenus () {

		if (startMenuActive) {
			startMenu.SetActive(true);
			splitScreenMenu.SetActive(false);
			statsMenu.SetActive(false);
			optionsMenu.SetActive(false);

			// logo.SetActive(true);
			menuTitle.text = "RUNNER\nRUNNER";
			cancelText.text = "Leave the Party";

			if (recentlyVisited == "Split Screen") {
				firstSelectedItem.SetSelectedGameObject(startFirstItemSplit);
			} else if (recentlyVisited == "Stats") {
				firstSelectedItem.SetSelectedGameObject(startFirstItemStats);
			} else if (recentlyVisited == "Options") {
				firstSelectedItem.SetSelectedGameObject(startFirstItemOptions);
			}
		}

		if (splitScreenMenuActive) {
			startMenu.SetActive(false);
			splitScreenMenu.SetActive(true);
			statsMenu.SetActive(false);
			optionsMenu.SetActive(false);

			// logo.SetActive(false);
			// if (enabledOptionsScript.getGamepads.Length == 1) { menuTitle.text = "SINGLE\nPLAYER"; }
			// else { menuTitle.text = "SPLIT\nSCREEN"; }
			cancelText.text = "Back";

			firstSelectedItem.SetSelectedGameObject(splitScreenFirstItem);
		}

		if (statsMenuActive) {
			startMenu.SetActive(false);
			splitScreenMenu.SetActive(false);
			statsMenu.SetActive(true);
			optionsMenu.SetActive(false);

			// logo.SetActive(false);
			menuTitle.text = "STATS";
			cancelText.text = "Back";

			firstSelectedItem.SetSelectedGameObject(statsFirstItem);
		}

		if (optionsMenuActive) {
			startMenu.SetActive(false);
			splitScreenMenu.SetActive(false);
			statsMenu.SetActive(false);
			optionsMenu.SetActive(true);

			// logo.SetActive(false);
			menuTitle.text = "OPTIONS";
			cancelText.text = "Back";

			firstSelectedItem.SetSelectedGameObject(optionsFirstItem);
		}

	}

}
