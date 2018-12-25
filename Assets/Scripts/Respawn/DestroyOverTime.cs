using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	private float lifetime = 1.2f;
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject, lifetime);
	}
}
