using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SelectedElements : MonoBehaviour {

    public GameObject hoverInfoActivated;

    private GameObject currentMode;

    public static int setGameMode;
    public static bool drunkModeActive;
    public static int setLevelLength;
    public static bool randomEventsActive;
    public static float setLevelTime;
    public static bool randomPlatformsActive;

    private float minLevelTime;
    private float maxLevelTime;

    public GameObject gameModeSelection;

    public GameObject drunkModeOption;
    public GameObject levelLengthOption;
    public GameObject randomEventsOption;
    public GameObject levelTimeOption;
    public GameObject randomPlatformsOption;

    public EventSystem selectGameMode;

    public GameObject gameModeObject;
    private Image gameModeImage;
    public Sprite sprintImage;
    public Sprite distanceImage;
    public Sprite sprintImageActive;
    public Sprite distanceImageActive;

    public Text drunkModeText;
    public Text levelLengthText;
    public Text randomEventsText;
    public Text levelTimeText;
    public Text randomPlatformsText;

    public GameObject drunkModeTitle;
    public GameObject levelLengthTitle;

    public Button gameModeNavigation;
    public Button randomEventsNavigation;

    public Button selectDrunkMode;
    public Button selectLevelLength;
    public Button selectRandomEvents;
    public Button selectLevelTime;
    public Button selectRandomPlatforms;

    private Navigation navigationGameMode;
    private Navigation navigationRandomEvents;

    private string[] levelLengthArray = {"Short", "Medium", "Long", "Insane"};
    public int currentModeIndex;

    private bool padPressed;
    private bool padLeft;
    private bool padUp;
    private bool padRight;
    private bool padDown;

    public GameObject hoverBox;
    public Text hoverTitle;
    public Text hoverText;

    private int countedSpaces;

    private Vector3 hoverInfoGameMode;
    private Vector3 hoverInfoDrunkMode;
    private Vector3 hoverInfoLevelLength;
    private Vector3 hoverInfoRandomEvents;
    private Vector3 hoverInfoLevelTime;
    private Vector3 hoverInfoRandomPlatforms;


    void Start () {
        // Initially setting the Hover Info Boxes positions on the canvas
        hoverInfoGameMode = new Vector3(hoverBox.transform.position.x, 720, 0);
        hoverInfoDrunkMode = hoverInfoLevelLength = new Vector3(hoverBox.transform.position.x, 315, 0);
        hoverInfoRandomEvents = new Vector3(hoverBox.transform.position.x, 225, 0);
        hoverInfoLevelTime = new Vector3(hoverBox.transform.position.x, 135, 0);
        hoverInfoRandomPlatforms = new Vector3(hoverBox.transform.position.x, 45, 0);

        gameModeImage = gameModeObject.GetComponent<Image>();

        // randomEventsActive = true;

        setLevelTime = 60f;
        minLevelTime = 30f;
        maxLevelTime = 120f;

        currentModeIndex = 1;
        setLevelLength = currentModeIndex;
    }

    void Update () {

        if (!MainNavigation.stopNavigationInput) {
            currentMode = EventSystem.current.currentSelectedGameObject;
        }
        
        // Translate the DPad from GetAxis to GetButtonDown to only execute once
        if (!MainNavigation.stopNavigationInput) {
            if (Input.GetAxis("[One] Left Horizontal") < 0 && !padPressed) {
                padPressed = true;
                padLeft = true;
                padPresser();
            } else if (Input.GetAxis("[One] Left Horizontal") > 0 && !padPressed) {
                padPressed = true;
                padRight = true;
                padPresser();
            } else if (Input.GetAxis("[One] Left Horizontal") == 0) {
                padPressed = false;
                padLeft = false;
                padRight = false;
            }
        }

        // Checking for current game mode and displaying appropriate options
        if (setGameMode == 0) {

            drunkModeTitle.SetActive(true);
            levelLengthTitle.SetActive(false);
            drunkModeOption.SetActive(true);
            levelLengthOption.SetActive(false);

            navigationGameMode = new Navigation();
            navigationGameMode.mode = Navigation.Mode.Explicit;
            navigationGameMode.selectOnUp = selectRandomPlatforms;
            navigationGameMode.selectOnDown = selectDrunkMode;
            gameModeNavigation.navigation = navigationGameMode;

            navigationRandomEvents = new Navigation();
            navigationRandomEvents.mode = Navigation.Mode.Explicit;
            navigationRandomEvents.selectOnUp = selectDrunkMode;
            navigationRandomEvents.selectOnDown = selectLevelTime;
            randomEventsNavigation.navigation = navigationRandomEvents;

        } else if (setGameMode == 1) {

            drunkModeTitle.SetActive(false);
            levelLengthTitle.SetActive(true);
            drunkModeOption.SetActive(false);
            levelLengthOption.SetActive(true);

            navigationGameMode = new Navigation();
            navigationGameMode.mode = Navigation.Mode.Explicit;
            navigationGameMode.selectOnUp = selectRandomPlatforms;
            navigationGameMode.selectOnDown = selectLevelLength;
            gameModeNavigation.navigation = navigationGameMode;

            navigationRandomEvents = new Navigation();
            navigationRandomEvents.mode = Navigation.Mode.Explicit;
            navigationRandomEvents.selectOnUp = selectLevelLength;
            navigationRandomEvents.selectOnDown = selectLevelTime;
            randomEventsNavigation.navigation = navigationRandomEvents;

        }
       
        // Setting the colors of the options when they are active and when they are not
        if (currentMode == gameModeSelection) {
            if (setGameMode == 1) { gameModeImage.sprite = sprintImageActive; }
            else { gameModeImage.sprite = distanceImageActive; }
            drunkModeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelLengthText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomEventsText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelTimeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomPlatformsText.color = new Color(0.23f, 0.25f, 0.3f, 100);

        } else if (currentMode == drunkModeOption) {
            if (setGameMode == 1) { gameModeImage.sprite = sprintImage; }
            else { gameModeImage.sprite = distanceImage; }
            drunkModeText.color = new Color(1, 1, 1, 100);
            levelLengthText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomEventsText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelTimeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomPlatformsText.color = new Color(0.23f, 0.25f, 0.3f, 100);

        } else if (currentMode == levelLengthOption) {
            if (setGameMode == 1) { gameModeImage.sprite = sprintImage; }
            else { gameModeImage.sprite = distanceImage; }
            drunkModeText.color = new Color(1, 1, 1, 100);
            levelLengthText.color = new Color(1, 1, 1, 100);
            randomEventsText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelTimeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomPlatformsText.color = new Color(0.23f, 0.25f, 0.3f, 100);

        } else if (currentMode == randomEventsOption) {
            drunkModeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelLengthText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomEventsText.color = new Color(1, 1, 1, 100);
            levelTimeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomPlatformsText.color = new Color(0.23f, 0.25f, 0.3f, 100);

        } else if (currentMode == levelTimeOption) {
            drunkModeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelLengthText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomEventsText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelTimeText.color = new Color(1, 1, 1, 100);
            randomPlatformsText.color = new Color(0.23f, 0.25f, 0.3f, 100);

        } else if (currentMode == randomPlatformsOption) {
            if (setGameMode == 1) { gameModeImage.sprite = sprintImage; }
            else { gameModeImage.sprite = distanceImage; }
            drunkModeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelLengthText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomEventsText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            levelTimeText.color = new Color(0.23f, 0.25f, 0.3f, 100);
            randomPlatformsText.color = new Color(1, 1, 1, 100);

        }

        // Setting the Hover Info Boxes position next to the currently selected option
        if (EnabledOptions.hoverInfoActive) {
            if (currentMode == gameModeSelection) {
                hoverBox.transform.position = hoverInfoGameMode;
                if (setGameMode == 1) {
                    hoverTitle.text = "Men overboard";
                    countSpaces();
                    hoverText.text = new string(' ', countedSpaces + 17) + " says the Captain. Try to get back to the ship quickly. The captain only saves one of you.";
                } else if (setGameMode == 0) {
                    hoverTitle.text = "Feierabend";
                    countSpaces();
                    hoverText.text = new string(' ', countedSpaces + 13) + " is what you want to get. Try to run as far as you can and beat the others.";
                }
            } else if (currentMode == drunkModeOption) {
                hoverBox.transform.position = hoverInfoDrunkMode;
                hoverTitle.text = "Drunk Mode";
                countSpaces();
                hoverText.text = new string(' ', countedSpaces + 18) + " are making the runs shorter. You have limited time until your head pops.";
            } else if (currentMode == levelLengthOption) {
                hoverBox.transform.position = hoverInfoLevelLength;
                hoverTitle.text = "Level Length";
                countSpaces();
                hoverText.text = new string(' ', countedSpaces + 14) + " is the distance you need to cover to reach the flag at the end of the level.";
            } else if (currentMode == randomEventsOption) {
                hoverBox.transform.position = hoverInfoRandomEvents;
                hoverTitle.text = "Random Events";
                countSpaces();
                hoverText.text = new string(' ', countedSpaces + 18) + " will influence the race from time to time. So try to be prepared for the worst.";
            } else if (currentMode == levelTimeOption) {
                hoverBox.transform.position = hoverInfoLevelTime;
                hoverTitle.text = "Level Time";
                countSpaces();
                hoverText.text = new string(' ', countedSpaces + 12) + " is the time you have for a single level to try your best.";
            } else if (currentMode == randomPlatformsOption) {
                hoverBox.transform.position = hoverInfoRandomPlatforms;
                hoverTitle.text = "Random Platforms";
                countSpaces();
                hoverText.text = new string(' ', countedSpaces + 22) + " with random sizes will be generated in the level for you to jump on.";
            }
        }

    }

    void countSpaces () {
        countedSpaces = hoverTitle.text.Length;
    }

    void padPresser () {

        // Set Game Mode
        if (currentMode == gameModeSelection) {
            if (padLeft || padRight) {
                if (gameModeImage.sprite == sprintImageActive) {
                    gameModeImage.sprite = distanceImageActive;
                    setGameMode = 0;
                } else {
                    gameModeImage.sprite = sprintImageActive;
                    setGameMode = 1;
                }
            }
        }

        // Set Drunk Mode
        if (currentMode == drunkModeOption) {
            if (padLeft || padRight) { drunkModeActive = !drunkModeActive; }
        }
        if (drunkModeActive) { drunkModeText.text = "On"; }
        else { drunkModeText.text = "Off"; }

        // Set Level Length
        if (currentMode == levelLengthOption) {
            currentModeIndex += padRight ? 1 : -1;
            levelLengthText.text = levelLengthArray[Mathf.Abs(currentModeIndex % levelLengthArray.Length)];
            setLevelLength = currentModeIndex % levelLengthArray.Length;
        }

        // Set Random Events
        if (currentMode == randomEventsOption) {
            if (padLeft || padRight) { randomEventsActive = !randomEventsActive; }
        }
        if (randomEventsActive) { randomEventsText.text = "Yes"; }
        else { randomEventsText.text = "No"; }

        // Set Level Time
        if (currentMode == levelTimeOption) {
            if (padLeft && levelTimeText.text == (minLevelTime + "s")) {
                setLevelTime = maxLevelTime;
            } else if (padLeft && levelTimeText.text != (minLevelTime + "s")) {
                setLevelTime -= 30f;
            } else if (padRight && levelTimeText.text == (maxLevelTime + "s")) {
                setLevelTime = minLevelTime;
            } else if (padRight && levelTimeText.text != (maxLevelTime + "s")) {
                setLevelTime += 30f;
            }
            levelTimeText.text = setLevelTime + "s";
        }

        // Set Random Platforms
        if (currentMode == randomPlatformsOption) {
            if (padLeft || padRight) { randomPlatformsActive = !randomPlatformsActive; }
        }
        if (randomPlatformsActive) { randomPlatformsText.text = "Yes"; }
        else { randomPlatformsText.text = "No"; }

    }

}
