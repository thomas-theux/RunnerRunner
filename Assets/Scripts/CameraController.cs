using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public string horizontalCameraInput;

	public Transform target;
	public Vector3 offset;

	public bool useOffsetValues;
	public float rotateSpeed;

	public Transform pivot;

	public bool disableCameraMovement;

	// TODO: Delete these when we have the Game Mode Manager
	// public static bool drunkMode = false;

	// drunk mode
	private Vector3 drunkModeRotation = new Vector3();
	private Vector3 drunkModePosition = new Vector3();
	private float[] rotSpeeds = {0.29f, 0.47f, 0.35f};
	private float[] posSpeeds = {0.1f, 0.08f, 0.07f};

	private int levelSize = 2480;


	void Start () {
		if (!useOffsetValues) {
			offset = target.position - transform.position;
		}

		pivot.transform.position = target.transform.position;
		// pivot.transform.parent = target.transform;
		// pivot.transform.parent = null;
	}


	void Update () {

		if (!SelectedElements.drunkModeActive) {
			pivot.transform.position = target.transform.position;

			if (GameStart.activateCameraControl) {
				float horizontal = Input.GetAxis(horizontalCameraInput) * rotateSpeed;
				pivot.Rotate(0, horizontal, 0);
			}
			
			// float desiredYAngle = pivot.eulerAngles.y;

			// Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
			if (!disableCameraMovement) {
				transform.position = target.position - offset;
				// transform.position = target.position - (rotation * offset);
				transform.LookAt(target.transform);
			}
		} else {
			if(target.transform.position.z != -levelSize) {
				float[] rotMax = {24, 16, 25};
				float[] posMax = {2, 2, 2};

				float distanceMultiplier = ((levelSize + target.transform.position.z) * 0.015f);

				for (int i = 0; i < 3; i++) {
					rotSpeeds[i] = getDrunkenValue(drunkModeRotation[i], rotMax[i], rotSpeeds[i]);
					drunkModeRotation[i] += rotSpeeds[i] * distanceMultiplier;

					posSpeeds[i] = getDrunkenValue(drunkModePosition[i], posMax[i], posSpeeds[i]);
					drunkModePosition[i] += posSpeeds[i] * distanceMultiplier;
				}
			} else {
				drunkModePosition = Vector3.zero;
				drunkModeRotation = Vector3.zero;
			}

			transform.localRotation = Quaternion.Euler(drunkModeRotation + new Vector3(35, 0, 0));

			transform.position = target.transform.position + new Vector3(
				0,
				10,
				-17.5f
			) + drunkModePosition;
		}

		
	}


	private float getDrunkenValue(float initialValue, float maxValue, float speed) {
		if(initialValue > maxValue || initialValue < maxValue * -1) speed *= -1;
		return speed;
	}

}
