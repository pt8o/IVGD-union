using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	GameObject bed;

	void Start () {

		bed = transform.parent.gameObject;

	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.CompareTag ("Player")) {
			
			GameObject smoke = Instantiate (Resources.Load ("Smoke"), transform.position, Quaternion.identity) as GameObject;
			GameObject.FindGameObjectWithTag ("Spawn Point").transform.position = transform.position;
			GameObject.FindGameObjectWithTag ("World").GetComponent <LevelProgression> ().ShowNextLevel ();
			bed.SetActive (false);

		}

	}

}
