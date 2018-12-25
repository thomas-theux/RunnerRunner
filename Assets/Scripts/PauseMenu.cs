using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool enablePauseMenu;

	public GameObject whiteOverlay;
	public AudioSource soundCancel;
	public AudioSource soundSelect;

	private float tempTimeScale;
	private bool cancelPauseMenu;


	void Update () {
		if (enablePauseMenu) {
			if (Input.GetButtonDown("[One] Options")) {
				GameStart.activatePlayerControl = false;
				whiteOverlay.SetActive(true);
				tempTimeScale = Time.timeScale;
				Time.timeScale = 0;
				cancelPauseMenu = true;
			}

			if (cancelPauseMenu) {
				if (Input.GetButtonDown("[One] X")) {
					soundSelect.Play();
					Time.timeScale = tempTimeScale;
					StartCoroutine(cancelMenuDelay());
					whiteOverlay.SetActive(false);
					cancelPauseMenu = false;
					GameStart.activatePlayerControl = true;
				}

				if (Input.GetButtonDown("[One] Circle")) {
					soundCancel.Play();
					Time.timeScale = MainNavigation.setTimeScale;
					StartCoroutine(cancelMenuDelay());
					cancelPauseMenu = false;
					GameStart.activatePlayerControl = true;
					SceneManager.LoadScene("1 Main Menu");
				}
			}
		}
	}


	IEnumerator cancelMenuDelay () {
		yield return new WaitForSeconds(0.5f);
	}

}
