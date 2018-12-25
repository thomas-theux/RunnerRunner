using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public CharacterController characterController;

	public float moveSpeed;
	// private float moveSpeedDefault;
	public float jumpForce;
	public float doubleForce;
	private int doubleJump;
	private bool activateDouble;
	public int jumpMax;

	public GameObject jumpEffect;
	public GameObject runEffect;

	public float gravityScale;

	private Vector3 moveDirection;

	public string horizontalInput;
	public string verticalInput;
	public string jumpInput;

	public Animator anim;

	public Transform pivot;
	public float turnSpeed;
	public GameObject playerModel;

	public AudioSource[] playerSounds;
	// 0 = Running
	// 1 = Jumping
	// 2 = Double Jump
	// 3 = Landing

	// Variables for the combo feature
	// public float comboTime;
	// public int comboCounter;
	// private float comboTimeMax = 2;

	// public Text comboCounterText;
	// public Text comboText;

	// STATS
	public static int statJumpsOne;
	public static int statJumpsTwo;

	public static float statAirtimeOne;
	public static float statAirtimeTwo;

	public static float statWalkTimeOne;
	public static float statWalkTimeTwo;

	public List<int> statSpeedArrayOne = new List<int>();
	public List<int> statSpeedArrayTwo = new List<int>();

	public static float statSpeedOne = 0;
	public static float statSpeedTwo = 0;

	public int mag;


	void Start () {
		characterController = GetComponent<CharacterController>();
		playerSounds = GetComponents<AudioSource>();

		// moveSpeedDefault = moveSpeed;
	}


	void Update () {

		if (!GameOver.gameOver) {

			// Code for the combo feature
			// if (GameStart.activatePlayerControl) {
			// 	if (comboTime <= 0) {
			// 		comboTime = 0;
			// 		comboCounter = 0;
			// 		moveSpeed = moveSpeedDefault;
			// 		comboCounterText.text = "";
			// 		comboText.enabled = false;
			// 	} else {
			// 		comboCounterText.text = comboCounter + "";
			// 		comboText.enabled = true;

			// 		if (comboTime > comboTimeMax) {
			// 			comboTime = comboTimeMax;
			// 		} else {
			// 			comboTime -= Time.deltaTime;
			// 		}
			// 	}
			// }
			
			float yStore = moveDirection.y;

			if (GameStart.activatePlayerControl) {
				moveDirection = (transform.forward * Input.GetAxis(verticalInput) * moveSpeed) + (transform.right * Input.GetAxis(horizontalInput) * moveSpeed);
			}
			moveDirection = Vector3.ClampMagnitude(moveDirection, moveSpeed);

			moveDirection.y = yStore;

			if (characterController.isGrounded) {
				moveDirection.y = 0f;
				doubleJump = 0;

				if (name == "One") {
					statWalkTimeOne++;
				} else if (name == "Two") {
					statWalkTimeTwo++;
				}	
			}

			if (!characterController.isGrounded && name == "One") {
				if (name == "One") {
					statAirtimeOne++;
				} else if (name == "Two") {
					statAirtimeTwo++;
				}	
			}

			// Walking sound
			if (characterController.isGrounded && characterController.velocity.magnitude > 1f) {
				if (!playerSounds[0].isPlaying) {
					playerSounds[0].Play();
				}
				// Instantiate(runEffect, new Vector3(characterController.transform.position.x, characterController.transform.position.y, characterController.transform.position.z), runEffect.transform.rotation);
			}

			// New jumping function (allowing n jumps)
			if (GameStart.activatePlayerControl) {
				if (Input.GetButtonDown(jumpInput)) {
					if (doubleJump < jumpMax) {

						// Jumping sound
						Instantiate(jumpEffect, new Vector3(characterController.transform.position.x, characterController.transform.position.y, characterController.transform.position.z), Quaternion.identity);
						playerSounds[1].Play();

						if (jumpInput == "[One] X") {
							statJumpsOne++;
						} else if (jumpInput == "[Two] X") {
							statJumpsTwo++;
						}

						if (doubleJump == 0) {
							moveDirection.y = jumpForce;
						} else {
							moveDirection.y = jumpForce + doubleForce;
						}
						doubleJump++;
						activateDouble = true;
					}
				}
			}

			if (!GameOver.gameOver) {
				moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
				characterController.Move(moveDirection * Time.deltaTime);
			}
			
			
			
			if (GameStart.activatePlayerControl) {
				if (Input.GetAxis(horizontalInput) != 0 || Input.GetAxis(verticalInput) != 0) {
					transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
					Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
					playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, turnSpeed * Time.deltaTime);
				}
			}

			// Tracking the characters speed
			var vel = characterController.velocity;
			mag = Mathf.RoundToInt(vel.magnitude);

			// if (name == "One") {
			// 	statSpeedArrayOne.Add(mag);
			// } else if (name == "Two") {
			// 	statSpeedArrayTwo.Add(mag);
			// }

			anim.SetBool("isGrounded", characterController.isGrounded);
			if (GameStart.activatePlayerControl) {
				anim.SetFloat("charSpeed", (Mathf.Abs(Input.GetAxis(verticalInput)) + Mathf.Abs(Input.GetAxis(horizontalInput))));
			}
			anim.SetInteger("doubleJump", doubleJump);

		} else {
			anim.SetFloat("charSpeed", 0);
		}
	}


	void LateUpdate() {
		if (doubleJump >= jumpMax) {
			activateDouble = false;
		}

		anim.SetBool("activateDouble", activateDouble);
	}


	public void getSpeedData () {
		if (statSpeedArrayOne.Count != 0) {
			for (int i = 0; i < statSpeedArrayOne.Count; i++) { statSpeedOne += statSpeedArrayOne[i]; }
			statSpeedOne = statSpeedOne / statSpeedArrayOne.Count;
		} else { statSpeedOne = 0; }
		
		if (statSpeedArrayTwo.Count != 0) {
			for (int i = 0; i < statSpeedArrayTwo.Count; i++) { statSpeedTwo += statSpeedArrayTwo[i]; }
			statSpeedTwo = statSpeedTwo / statSpeedArrayTwo.Count;
		} else { statSpeedTwo = 0; }
		
	}
	
}
