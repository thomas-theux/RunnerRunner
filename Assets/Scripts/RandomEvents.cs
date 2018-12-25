using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvents : MonoBehaviour {

    private GameStart gameStartScript;

    public GameObject[] players;

    public AudioSource shuffleEvents;
    public AudioSource selectEvent;

    public AudioSource[] eventSounds;
    // 0 soundNoDoubleJumps;
    // 1 soundTripleJumps;
    // 2 soundTooMuchGravity;
    // 3 soundMoonGravity;
    // 4 soundSlowDown;
    // 5 soundSpeedUp;
    // 6 soundTimeLapse;
    // 7 soundSlowTheMotion;

    public Text eventCountdownText;

    private float intervalTime;
    private bool getRandomInterval;
    private bool activateShuffler;
    private bool shufflerActivated;
    private bool activateReset;
    private bool activateEvent;

    private int eventIndex;
    private int currentEvent;
    private int tempEvent;
    private string[] eventArray = {
        "No Double Jumps",
        "Triple Jumps!",
        "Too much Gravity",
        "Moon Gravity",
        "Speed Up",
        "Slow Down",
        "Time Lapse",
        "Slow Motion"
    };

    public Image eventCard;
    public Sprite[] eventSprites;

    // Default values
    private int defaultJumpMax = 2;
    private float defaultGravityScale = 5;
    private float defaultMoveSpeed = 16;
    private float defaultTimeScale = MainNavigation.setTimeScale;


    void Start () {
        gameStartScript = GetComponent<GameStart>();
        // eventSounds = GetComponents<AudioSource>();

        getRandomInterval = true;
        currentEvent = -1;
        tempEvent = -1;

        // Default values
        defaultJumpMax = players[0].GetComponent<PlayerController>().jumpMax;
        defaultGravityScale = players[0].GetComponent<PlayerController>().gravityScale;
        defaultMoveSpeed = players[0].GetComponent<PlayerController>().moveSpeed;
    }


    void Update () {
        if (gameStartScript.activateEventCountdown) {

            if (getRandomInterval) {
                randomIntervalTime();
            }
            
            if (intervalTime > 0) {
                intervalCountdown();
            } else {
                if (!shufflerActivated) {
                    activateShuffler = true;
                    shufflerActivated = true;
                }
            }

            if (activateShuffler) {
                StartCoroutine(eventShuffler());
            }

            if (activateReset) {
                resetStats();
            }

            if (activateEvent) {
                eventActivation();
            }

        }
    }


    void randomIntervalTime () {
        intervalTime = Random.Range(5, 6);
        getRandomInterval = false;
    }


    void intervalCountdown () {
            intervalTime -= Time.deltaTime;
    }

    IEnumerator eventShuffler () {
        activateShuffler = false;
        eventCard.enabled = true;

        for (int i = 0; i < 20; i++) {
            eventIndex = Random.Range(0, eventArray.Length);
            while (eventIndex == tempEvent) {
                eventIndex = Random.Range(0, eventArray.Length);
            }
            while (eventIndex == currentEvent && i == 19) {
                eventIndex = Random.Range(0, eventArray.Length);
            }
            // eventCountdownText.text = eventArray[eventIndex];
            eventCard.sprite = eventSprites[eventIndex];
            tempEvent = eventIndex;

            shuffleEvents.Play();

            float changeCardDelay = 0.00006f * (i*i*i);
            yield return new WaitForSeconds(changeCardDelay);
        }

        yield return new WaitForSeconds(0.3f);

        eventSounds[tempEvent].Play();

        activateReset = true;
    }


    void resetStats () {
        activateReset = false;

        switch (currentEvent) {
            case 0:
            case 1:
                for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().jumpMax = defaultJumpMax; }
                break;
            case 2:
            case 3:
                for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().gravityScale = defaultGravityScale; }
                break;
            case 4:
            case 5:
                for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().moveSpeed = defaultMoveSpeed; }
                break;
            case 6:
            case 7:
                Time.timeScale = defaultTimeScale;;
                break;
        }

        currentEvent = eventIndex;

        activateEvent = true;
    }

    void eventActivation () {
        activateEvent = false;

        switch (eventIndex) {
            case 0:
                for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().jumpMax = 1; }
                break;
            case 1:
            for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().jumpMax = 3; }
                break;
            case 2:
                for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().gravityScale = 9; }
                break;
            case 3:
                for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().gravityScale = 3.5f; }
                break;
            case 4:
                for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().moveSpeed = 10; }
                break;
            case 5:
                for (int i = 0; i < players.Length; i++) { players[i].GetComponent<PlayerController>().moveSpeed = 26; }
                break;
            case 6:
                Time.timeScale = MainNavigation.setTimeScale + 0.5f;
                break;
            case 7:
                Time.timeScale = MainNavigation.setTimeScale - 0.5f;
                break;
        }

        StartCoroutine(eventTextDelay());
    }


    IEnumerator eventTextDelay () {
        yield return new WaitForSeconds(1);
        // eventCountdownText.text = "";
        // eventCard.enabled = false;

        getRandomInterval = true;
        shufflerActivated = false;
    }

}
