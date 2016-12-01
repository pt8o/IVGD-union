using UnityEngine;
using System.Collections;

public class objectTimer : MonoBehaviour {

	private float timeTotal;
	private float timeLeft;
	private GameObject timer;

	public float startAngle;
	public float endAngle;

	public string rotateDirection;

	private bool levelStarted = false;

	// This is called by Timer object every time new level started.
	public void levelStart () {
		timer = GameObject.Find ("Timer");
		timeTotal = timer.GetComponent<timer> ().timeTotal;
		gameObject.transform.localEulerAngles = checkDirection (rotateDirection, startAngle);
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft = timer.GetComponent<timer> ().timeLeft;

		float currentAngle = (timeLeft - timeTotal) / timeTotal * (startAngle - endAngle) + startAngle;

		if (currentAngle <= endAngle) {
			gameObject.transform.localEulerAngles = checkDirection(rotateDirection, currentAngle);
	
		}
	}

	// I'm sure this isn't optimal but whatever.
	private Vector3 checkDirection(string dir, float num) {
		switch (dir) {
			case "x":
				return new Vector3(num,0,0);	
			case "y":
				return new Vector3(0,num,0);
			case "z":
			default:
				return new Vector3(0,0,num);
		}
	}
}
