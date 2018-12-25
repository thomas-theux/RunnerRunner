using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public BestDistance bestDistanceScript;

	public GameObject levelGround;
	public GameObject startPlatform;
	public GameObject goalFlag;
	public GameObject playerOne;
	public GameObject playerTwo;
	public GameObject playerSpawnPoint;

	private float[] groundSizeZ = {200, 400, 600, 1000, 5000};

	private float levelSizeX;
	private float levelSizeZ;

	private float levelMinX;
	private float levelMaxX;

	private float levelMinZ;
	private float levelMaxZ;

	public GameObject newPlatform;
	public GameObject worldParent;
	private GameObject spawnNew;

	public GameObject newBottle;

	public GameObject newCheckpoint;
	public GameObject checkpointParent;
	private GameObject spawnCheckpoint;

	private float platformScaleX;
	private float platformScaleZ;

	private float setPosX;
	private float setPosY;
	private float setPosZ;

	private Vector3 newPosition;
	private Quaternion platformRotation;

	public float offsetX;
	public float offsetZ;

	private float spawnAmount;

	public bool floatingPlatforms;
	private float horizontalScale = 6f;
	[Range(0,10)]
	public int platformHeight;


	// Use this for initialization
	void Start () {

		if (SelectedElements.setGameMode == 0) { SelectedElements.setLevelLength = 4; }

		levelGround.transform.localScale = new Vector3(levelGround.transform.localScale.x, levelGround.transform.localScale.y, groundSizeZ[SelectedElements.setLevelLength]);
		startPlatform.transform.position = new Vector3(startPlatform.transform.position.x, startPlatform.transform.position.y, -(levelGround.transform.localScale.z / 2 - 20));
		goalFlag.transform.position = new Vector3(goalFlag.transform.position.x, goalFlag.transform.position.y, (levelGround.transform.localScale.z / 2 - 20));

		playerSpawnPoint.transform.position = new Vector3(playerSpawnPoint.transform.position.x, playerSpawnPoint.transform.position.y, -(levelGround.transform.localScale.z / 2 - 20));
		playerOne.transform.position = playerTwo.transform.position = playerSpawnPoint.transform.position;

		levelSizeX = levelGround.transform.localScale.x;
		levelSizeZ = levelGround.transform.localScale.z;

		levelMinX = 0f - (levelSizeX / 2) + offsetX;
		levelMaxX = 0f + (levelSizeX / 2) - offsetX;

		levelMinZ = 0f - (levelSizeZ / 2) + offsetZ;
		levelMaxZ = 0f + (levelSizeZ / 2) - offsetZ;

		platformScaleX = newPlatform.transform.localScale.x / 2;
		platformScaleZ = newPlatform.transform.localScale.z / 2;

		spawnAmount = (levelSizeX + levelSizeZ) / 4;

		for (int i = 0; i < spawnAmount; i++) {
			setPosX = Random.Range(levelMinX + platformScaleX, levelMaxX - platformScaleX);
			setPosY = Random.Range(0.0f, 0.4f);
			setPosZ = Random.Range(levelMinZ + platformScaleZ, levelMaxZ - platformScaleZ);

			newPosition = new Vector3(setPosX, setPosY, setPosZ);

			// Randomize scale of the instantiated platforms
			if (SelectedElements.randomPlatformsActive) { horizontalScale = Random.Range(3,10); }

			// platformRotation = Quaternion.identity;
			platformRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 90), 0));

			spawnNew = Instantiate(newPlatform, newPosition, platformRotation);
			spawnNew.transform.parent = worldParent.transform;
			spawnNew.transform.localScale = new Vector3 (horizontalScale, platformHeight, horizontalScale);

			if (SelectedElements.setGameMode == 0) {
				if (i % 8 == 0) {
					Instantiate(newBottle, new Vector3(spawnNew.transform.position.x, newBottle.transform.position.y, spawnNew.transform.position.z), newBottle.transform.rotation);
				}
			}
		}

		// Set checkpoints
		if (SelectedElements.setGameMode == 1) {
			float checkpointCount = (groundSizeZ[SelectedElements.setLevelLength] / 200);
			float checkpointDistance = groundSizeZ[SelectedElements.setLevelLength] / (checkpointCount + 1);

			for (int j = 0; j < checkpointCount; j++) {
				if (j > 0) {
					float checkpointPos = -(groundSizeZ[SelectedElements.setLevelLength] / 2) + (checkpointDistance * j);
					spawnCheckpoint = Instantiate(newCheckpoint, new Vector3(newCheckpoint.transform.position.x, newCheckpoint.transform.position.y, checkpointPos), newCheckpoint.transform.rotation);
					spawnCheckpoint.transform.parent = checkpointParent.transform;
				}
			}
		} else {

		}
		

		bestDistanceScript.enabled = true;

		Destroy(this);
	}

}
