using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnabledOptions : MonoBehaviour {

	public static bool hoverInfoActive;
	public Text enableHoverInfoText;
	public GameObject hoverInfoBox;

	public Text singleOrSplit;

	public GameObject credits;

	public string[] getGamepads;

	public GameObject gamepadOne;
	public GameObject gamepadTwo;
	public Text noGamepads;

	private Image imageOne;
	private Image imageTwo;
	

	void Start () {
		// Cursor.visible = false;
		// Cursor.lockState = CursorLockMode.Locked;

		hoverInfoActive = true;
		imageOne = gamepadOne.GetComponent<Image>();
		imageTwo = gamepadTwo.GetComponent<Image>();
	}

	// Enable/Disable the Hover Info Boxes
	public void enableHoverInfo () {
		hoverInfoActive = !hoverInfoActive;
		if (enableHoverInfoText.text == "Enable Hover Info") {
			enableHoverInfoText.text = "Disable Hover Info";
			hoverInfoBox.SetActive(true);
		} else {
			enableHoverInfoText.text = "Enable Hover Info";
			hoverInfoBox.SetActive(false);
		}
	}

	// Show Credits
	public void showCredits () {
		MainNavigation.stopNavigationInput = true;
		credits.SetActive(true);
	}

	void Update () {
		// Detect all the connected Gamepads and show their status in the game
		getGamepads = Input.GetJoystickNames();

		if (getGamepads.Length == 0) {
			imageOne.color = new Color32(255, 255, 225, 0);
			imageTwo.color = new Color32(255, 255, 225, 0);
			noGamepads.enabled = true;
		}
		if (getGamepads.Length == 1) {
			imageOne.color = new Color32(255, 255, 225, 255);
			imageTwo.color = new Color32(255, 255, 225, 80);
			noGamepads.enabled = false;
			singleOrSplit.text = "Single Player";
		}
		if (getGamepads.Length == 2) {
			imageOne.color = new Color32(255, 255, 225, 255);
			imageTwo.color = new Color32(255, 255, 225, 255);
			noGamepads.enabled = false;
			singleOrSplit.text = "Split Screen";
		}

		// Kill Credits
		if (Input.GetButtonDown("[One] Circle") && MainNavigation.stopNavigationInput) {
			credits.SetActive(false);
			MainNavigation.stopNavigationInput = false;
		}
	}
}
