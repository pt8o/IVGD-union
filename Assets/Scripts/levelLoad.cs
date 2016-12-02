using UnityEngine;
using System.Collections;

public class levelLoad : MonoBehaviour {

	/* YO HERE'S HOW THIS WORKS
	 * LevelMaster has the levelLoad.cs script. This script has all of the level prefabs loaded into is GameObject array.
	 * EVERY LEVEL prefab must have the following elements:
	 * 	- Level itself is empty game object
	 * 	- Level structure itself
	 * 	- Make sure level structure is set up so that level parent object (which is empty) is centered on the intended spawn point!
	 * 	- Trigger, with the levelTrigger.cs script
	 * 	- Empty "SpawnNext" object, where this level ends and the next one is to start
	 * 
	 * When player walks thru the trigger, this spawns the nextLevel at the spawnPoint (taking into account the difference between the transform of nextLevel itself and nextLevel's Spawn Origin)
	 * 
	 */

	public GameObject[] allLevels;

	public int levelCount = 0;

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

		Debug.Log (spawnDifference);
	}

	public void levelTrigger() {
		if (levelCount < allLevels.Length) {
			Instantiate (nextLevel, spawnPoint.position, spawnPoint.rotation);

			levelCount += 1;

			// This shit is really finnicky so levelSetup() needs to be called after a 1 sec delay.
			Invoke ("levelSetup", 1);
		}
	}

	void Update () {
	
	}
}
