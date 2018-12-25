using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawCardsGrid : MonoBehaviour {

	public GameOver gameOverScript;
	public TitleSystem titleSystemScript;

	private Image newCard;
	public Image cardImage;
	public GameObject canvasOne;
	public GameObject canvasTwo;

	public int howManyCanvas;
	public int secondCanvas;

	public int xCount;
	public int yCount;

	public int distX;
	public int distY;

	public int startX;
	public int startY;

	private int fifthX;
	private int fifthY = -845;

	private bool levelEndNavOn;
	public GameObject levelEndButtons;


	public void Awake () {

		for (int i = 0; i < howManyCanvas; i++) {
			for (int j = 0; j < xCount; j++) {
				for (int k = 0; k < yCount; k++) {
					float posX = j * distX + startX + i * secondCanvas;
					float posY = k * distY + startY;

					if (i == 0) {
						newCard = Instantiate(cardImage, new Vector3(posX, posY, 0), Quaternion.identity);
						newCard.transform.SetParent(canvasOne.transform, false);
					} else if (i == 1) {
						newCard = Instantiate(cardImage, new Vector3(posX, posY, 0), Quaternion.identity);
						newCard.transform.SetParent(canvasTwo.transform, false);
					}

					StartCoroutine(identifyCards(i, j, k));

					StartCoroutine(fadeIn());

					levelEndNavOn = true;
				}
			}

			fifthX = startX + i * secondCanvas;
			newCard = Instantiate(cardImage, new Vector3(fifthX, fifthY, 0), Quaternion.identity);

			if (i == 0) {
				newCard.transform.SetParent(canvasOne.transform, false);
				newCard.transform.GetChild(0).GetComponent<Text>().text = titleSystemScript.titleOne;
			}
			else if (i == 1) {
				newCard.transform.SetParent(canvasTwo.transform, false);
				newCard.transform.GetChild(0).GetComponent<Text>().text = titleSystemScript.titleTwo;
			}

			newCard.rectTransform.sizeDelta = new Vector2(1120, newCard.rectTransform.sizeDelta.y);
			StartCoroutine(fadeIn());
		}

	}


	IEnumerator identifyCards (int i, int j, int k) {
		if (j == 0 && k == 0) {
			if (i == 0) {
				float kilometers = 0;
				if (SelectedElements.setGameMode == 0) { kilometers = BestDistance.statBestDistanceOne; }
				else if (SelectedElements.setGameMode == 1) { kilometers = RespawnManager.statOverallDistanceOne; }
				kilometers = Mathf.Round(kilometers * 100f) / 100f;
				newCard.transform.GetChild(0).GetComponent<Text>().text = kilometers + " m";
			} else if (i == 1) {
				float kilometers = 0;
				if (SelectedElements.setGameMode == 0) { kilometers = BestDistance.statBestDistanceTwo; }
				else if (SelectedElements.setGameMode == 1) { kilometers = RespawnManager.statOverallDistanceTwo; }
				kilometers = Mathf.Round(kilometers * 100f) / 100f;
				newCard.transform.GetChild(0).GetComponent<Text>().text = kilometers + " m";
			}
			if (SelectedElements.setGameMode == 0) {
				newCard.transform.GetChild(1).GetComponent<Text>().text = "BEST DISTANCE";
			} else if (SelectedElements.setGameMode == 1) {
				newCard.transform.GetChild(1).GetComponent<Text>().text = "OVERALL DISTANCE";
			}
			
		} else if (j == 1 && k == 0) {
			if (i == 0) { newCard.transform.GetChild(0).GetComponent<Text>().text = PlayerController.statJumpsOne + ""; } else
			if (i == 1) { newCard.transform.GetChild(0).GetComponent<Text>().text = PlayerController.statJumpsTwo + ""; }
			newCard.transform.GetChild(1).GetComponent<Text>().text = "JUMPS";
		} else if (j == 0 && k == 1) {
			if (i == 0) { newCard.transform.GetChild(0).GetComponent<Text>().text = RespawnManager.statDeathsOne + ""; } else
			if (i == 1) { newCard.transform.GetChild(0).GetComponent<Text>().text = RespawnManager.statDeathsTwo + ""; }
			newCard.transform.GetChild(1).GetComponent<Text>().text = "DEATHS";
		} else if (j == 1 && k == 1) {
			if (SelectedElements.setGameMode == 0) {
				if (i == 0) { newCard.transform.GetChild(0).GetComponent<Text>().text = PoppingHeads.statPoppedHeadsOne + ""; } else
				if (i == 1) { newCard.transform.GetChild(0).GetComponent<Text>().text = PoppingHeads.statPoppedHeadsTwo + ""; }
				newCard.transform.GetChild(1).GetComponent<Text>().text = "POPPED HEADS";
			} else if (SelectedElements.setGameMode == 1) {
				if (i == 0) { newCard.transform.GetChild(0).GetComponent<Text>().text = (Mathf.Round(PlayerController.statSpeedOne * 10) / 10) + " km/h"; } else
				if (i == 1) { newCard.transform.GetChild(0).GetComponent<Text>().text = (Mathf.Round(PlayerController.statSpeedTwo * 10) / 10) + " km/h"; }
				newCard.transform.GetChild(1).GetComponent<Text>().text = "AVERAGE SPEED";
			}
		}

		yield return null;
	}


	IEnumerator fadeIn () {
		newCard.GetComponent<Image>().CrossFadeAlpha(0, 0f, true);
		newCard.GetComponent<Image>().CrossFadeAlpha(1, 0.5f, true);
		yield return null;
	}


	void Update () {
		if (levelEndNavOn) {
			levelEndButtons.SetActive(true);

			if (Input.GetButtonDown("[One] X")) {
				levelEndNavOn = false;
				levelEndButtons.SetActive(false);
				StartCoroutine(gameOverScript.resetEverything("Restart Level"));
			} else if (Input.GetButtonDown("[One] Circle")) {
				levelEndNavOn = false;
				levelEndButtons.SetActive(false);
				StartCoroutine(gameOverScript.resetEverything("Main Menu"));
			}
		}
	}

}