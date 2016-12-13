using UnityEngine;
using System.Collections;

public class LevelProgression : MonoBehaviour {

	public int currentLevel = -1;

	public GameObject[] levels;
	public GameObject[] platforms;


	public void ShowNextLevel () {

		currentLevel++;

		levels [currentLevel].SetActive (true);
		platforms [currentLevel].SetActive (true);


	}

}
