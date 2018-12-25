using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public AudioSource uiMusic;

	public AudioSource navigateHorizontal;
	public AudioSource navigateVertical;
	public AudioSource selectItem;
	public AudioSource cancelItem;
	public AudioSource startGame;

	private bool padPressedHorizontal;
	private bool padPressedVertical;

	public static bool uiHorizontalSound;
	public static bool uiVerticalSound;
	public static bool uiSelectSound;
	public static bool uiCancelSound;
	public static bool startGameSound;


	void Start () {
		uiMusic.Play();

		uiHorizontalSound = false;
		uiVerticalSound = true;
		uiSelectSound = true;
		uiCancelSound = true;
	}

	void Update () {

		if (!MainNavigation.stopNavigationInput) {
			if (Input.GetAxis("[One] Left Horizontal") != 0 && !padPressedHorizontal && uiHorizontalSound) {
				padPressedHorizontal = true;
				navigateHorizontal.Play();
			} else if (Input.GetAxis("[One] Left Horizontal") == 0) {
				padPressedHorizontal = false;
			}
			if (Input.GetAxis("[One] Left Vertical") > 0.6f && !padPressedVertical && uiVerticalSound || Input.GetAxis("[One] Left Vertical") < -0.6f && !padPressedVertical && uiVerticalSound) {
				padPressedVertical = true;
				navigateVertical.Play();
			} else if (Input.GetAxis("[One] Left Vertical") == 0) {
				padPressedVertical = false;
			}
		}

		if (Input.GetButtonDown("[One] X") && uiSelectSound) {
			selectItem.Play();
		}

		if (Input.GetButtonDown("[One] Circle") && uiCancelSound) {
			cancelItem.Play();
		}

		if (Input.GetButtonDown("[One] Options") && startGameSound) {
			startGame.Play();
		}

	}
}