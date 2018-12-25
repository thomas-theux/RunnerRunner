using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBottle : MonoBehaviour {

	private PoppingHeads poppingHeadsScript;
	private RespawnManager respawnManagerScript;

	public GameObject collectEffect;
	public AudioSource soundCollectBottle;

	private Collider colliderComponent;

	private bool bottleCollected;


	void Start () {
		poppingHeadsScript = GameObject.Find("GameManager").GetComponent<PoppingHeads>();
		respawnManagerScript = GameObject.Find("Ground").GetComponent<RespawnManager>();

		soundCollectBottle = GameObject.Find("Collect Bottle").GetComponent<AudioSource>();

		colliderComponent = GetComponent<Collider>();
	}


	void OnTriggerEnter (Collider other) {

		soundCollectBottle.Play();
		Instantiate(collectEffect, transform.position, transform.rotation);
		
		if (other.name == "One") {
			poppingHeadsScript.headOne.transform.localScale = respawnManagerScript.defaultHeadSize;
		} else if (other.name == "Two") {
			poppingHeadsScript.headTwo.transform.localScale = respawnManagerScript.defaultHeadSize;
		}

		// gameObject.SetActive(false);
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(false);

		colliderComponent.enabled = false;

		bottleCollected = true;

	}

	void Update () {
		if (bottleCollected) {
			bottleCollected = false;
			StartCoroutine(reactivateDelay());
		}
	}

	IEnumerator reactivateDelay () {
		yield return new WaitForSeconds(3);

		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(true);

		colliderComponent.enabled = true;
	}

}
