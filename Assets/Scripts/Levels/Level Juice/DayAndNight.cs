using UnityEngine;
using System.Collections;

public class DayAndNight : MonoBehaviour {

	public static float currentTime;

	public Material skybox;
	public float timeStep = 1f;
	public Color dayColor;
	public Color nightColor;

	private bool isRising = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		HandleDayAndNight ();
	
	}

	void HandleDayAndNight () {

		if (isRising) {

			if (currentTime < 1) {

				currentTime += timeStep * Time.deltaTime;
				skybox.SetColor ("_GroundColor", Color.Lerp (nightColor, dayColor, currentTime));

			} else {

				isRising = false;

			}



		} else {

			if (currentTime > 0) {

				currentTime -= timeStep * Time.deltaTime;
				skybox.SetColor ("_GroundColor", Color.Lerp (nightColor, dayColor, currentTime));

			} else {

				isRising = true;

			}

		}

	}

}
