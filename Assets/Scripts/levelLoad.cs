using UnityEngine;
using System.Collections;

public class levelLoad : MonoBehaviour {

	/* YO HERE'S HOW THIS WORKS
	 * LevelMaster has the levelLoad.cs script. This script has all of the level prefabs loaded into is GameObject array.
	 * EVERY LEVEL prefab must have the following elements:
	 * 	- Level itself is empty game object
	 * 	- Empty "SpawnOrigin" object, where this level structure starts
	 * 	- Level structure itself
	 * 	- Trigger, with the levelTrigger.cs script
	 * 	- Empty "SpawnNext" object, where this level ends and the next one is to start
	 * 
	 * When player walks thru the trigger, this spawns the nextLevel at the spawnPoint (taking into account the difference between the transform of nextLevel itself and nextLevel's Spawn Origin)
	 * 
	 */

	public GameObject[] allLevels;

	private int levelCount = 0;

	private GameObject thisLevel;
	private GameObject nextLevel;
	private Transform spawnPoint;
	private Vector3 spawnDifference;


	void Start () {
		levelSetup ();	
	}

	private void levelSetup() {
		string thisLevelName = allLevels [levelCount].name;
		thisLevel = GameObject.Find (thisLevelName + "(Clone)");
		nextLevel = allLevels [levelCount + 1];

		spawnPoint = thisLevel.transform.Find ("SpawnNext");
		spawnDifference = nextLevel.transform.Find ("SpawnOrigin").localPosition - nextLevel.transform.localPosition;
	}

	public void levelTrigger() {
		if (levelCount < allLevels.Length) {
			Instantiate (nextLevel, spawnPoint.position - spawnDifference, spawnPoint.rotation);

			levelCount += 1;

			// This shit is really finnicky so levelSetup() needs to be called after a 1 sec delay.
			Invoke ("levelSetup", 1);
		}
	}

	void Update () {
	
	}
}
