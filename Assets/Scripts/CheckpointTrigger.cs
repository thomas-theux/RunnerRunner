using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {

	public static Vector3 checkpointPosOne;
	public static Vector3 checkpointPosTwo;
	private GameObject spawnPoint;
	private Vector3 thisCheckpointsPos;

	public GameObject checkpointEffect;
	private AudioSource soundCheckpoint;

	void Awake () {
		soundCheckpoint = GameObject.Find("Checkpoint Sound").GetComponent<AudioSource>();
	}

	void Start () {
		spawnPoint = GameObject.Find("Spawn Point");
		checkpointPosOne = spawnPoint.transform.position;
		thisCheckpointsPos = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
	}

	void OnTriggerEnter (Collider other) {
		soundCheckpoint.Play();
		Instantiate(checkpointEffect, transform.position, transform.rotation);

		if (other.name == "One") {
			checkpointPosOne = thisCheckpointsPos;
		} else if (other.name == "Two") {
			checkpointPosTwo = thisCheckpointsPos;
		}
	}

}
