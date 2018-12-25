using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingHeads : MonoBehaviour {

    public RespawnManager respawnManagerScript;

    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject headOne;
    public GameObject headTwo;

    private float maxHeadSize;
    private Vector3 inflationSpeed;

    // STATS
    public static int statPoppedHeadsOne;
    public static int statPoppedHeadsTwo;


    void Start () {
        maxHeadSize = 6.0f;
        inflationSpeed = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void Update () {
        if (GameStart.activatePlayerControl) {
             if (!RespawnManager.stopIncreasingOne) {
                if (headOne.transform.localScale.x < maxHeadSize) {
                    headOne.transform.localScale += inflationSpeed * Time.deltaTime * ((Mathf.Abs(-2480 - playerOne.transform.position.z)) / 130);
                } else {
                    headOne.transform.localScale = Vector3.zero;

                    statPoppedHeadsOne++;

                    StartCoroutine(respawnManagerScript.DeathProcess(playerOne));
                }
            }

            if (!RespawnManager.stopIncreasingTwo) {
                if (headTwo.transform.localScale.x < maxHeadSize) {
                    headTwo.transform.localScale += inflationSpeed * Time.deltaTime * ((Mathf.Abs(-2480 - playerTwo.transform.position.z)) / 130);
                } else {
                    headTwo.transform.localScale = Vector3.zero;

                    statPoppedHeadsTwo++;

                    StartCoroutine(respawnManagerScript.DeathProcess(playerTwo));
                }
            }
        }
       
    }

}
