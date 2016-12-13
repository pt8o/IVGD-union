using UnityEngine;
using System.Collections;

public class PlatformJuice : MonoBehaviour {

	public static PlatformJuice selectedPlatform;

	public float startGlowRate = 1.0f;
	public AnimationCurve startGlowCurve;

	public float endGlowRate = 1.0f;
	public AnimationCurve endGlowCurve;

	public float startGrowingRate = 1.0f;
	public AnimationCurve startGrowingCurve;

	public float endGrowingRate = 1.0f;
	public AnimationCurve endGrowingCurve;

	public float minScale = 1.0f;
	public float maxScale = 1.2f;

	private GameObject[] containedPieces;
	private Renderer[] renderers;

	// Use this for initialization
	void Start () {

		containedPieces = new GameObject [transform.childCount];
		renderers = new Renderer [transform.childCount];

		for (int i = 0; i < transform.childCount; i++) {
			containedPieces [i] = transform.GetChild (i).gameObject;
			renderers [i] = containedPieces [i].GetComponent <Renderer> ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	void OnCollisionEnter (Collision other) {

		if (other.gameObject.CompareTag ("Player")) {

			Select ();

		}

	}

	void Select () {

		if (selectedPlatform == this) return;

		StartCoroutine ("StartGlow");
		StartCoroutine ("StartGrowing");

		if (selectedPlatform != null)
			selectedPlatform.Deselect ();

		selectedPlatform = this;

	}

	public void Deselect () {
		
		StartCoroutine ("EndGlow");
		StartCoroutine ("EndGrowing");

	}

	IEnumerator StartGlow () {

		float t = 0.0f;
		float emission = 0.0f;

		while (t < 1.0f) {

			t += Time.deltaTime * 3;
			emission = startGlowCurve.Evaluate (t);

			for (int i = 0; i < renderers.Length; i++) {
				renderers [i].material.SetColor ("_EmissionColor", new Color (emission, emission, emission));
			}
				
			yield return new WaitForSeconds (0.0f);

		}


	}

	IEnumerator EndGlow () {

		float t = 1.0f;
		float emission = 0.0f;

		while (t > 0.0f) {

			t -= Time.deltaTime * 3;
			emission = startGlowCurve.Evaluate (t);

			for (int i = 0; i < renderers.Length; i++) {
				renderers [i].material.SetColor ("_EmissionColor", new Color (emission, emission, emission));
			}

			yield return new WaitForSeconds (0.0f);

		}

	}

	IEnumerator StartGrowing () {

		float t = 0.0f;
		float scale = 0.0f;

		while (t < 1.0f) {

			t += Time.deltaTime * 3;
			scale = Easing.Quintic.In (t);

			transform.localScale = Vector3.one * (minScale + (maxScale - minScale) * scale);

			yield return new WaitForSeconds (0.0f);

		}


	}
		
	IEnumerator EndGrowing () {

		float t = 1.0f;
		float scale = 0.0f;

		while (t > 0.0f) {

			t -= Time.deltaTime * 3;
			scale = Easing.Quintic.In (t);

			transform.localScale = Vector3.one * (minScale + (maxScale - minScale) * scale);

			yield return new WaitForSeconds (0.0f);

		}


	}
		
}
