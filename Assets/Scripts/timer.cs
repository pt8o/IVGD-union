using UnityEngine;
using System.Collections;

public class timer : MonoBehaviour {

	public float timeTotal;
	public float timeLeft;

	public GameObject[] timeChildren;


	void Start () { timerStart (); }
	void Update () { timeLeft -= Time.deltaTime; }

	// Call this every time new level is started (after declaring new timeTotal)
	private void timerStart() {
		timeLeft = timeTotal;

		for (int i = 0; i < timeChildren.Length; i++) {
			timeChildren [i].GetComponent<objectTimer> ().levelStart ();
		}
	}
}
