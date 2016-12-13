using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelJuice : MonoBehaviour {

	public float startFadeRate = 1.0f;
	public AnimationCurve startFadeCurve;

	List <List <Renderer> > rends = new List <List <Renderer> > ();

	// Use this for initialization
	void Start () {

		GetRenderers ();
		StartCoroutine ("FadeInLevel");

	}
	

	void GetRenderers () {

		for (int i = 0; i < transform.childCount; i++) {

			Transform t = transform.GetChild (i);
			rends.Add (new List <Renderer> ());

			for (int j = 0; j < t.childCount; j++) {

				Renderer r = t.GetChild (j).GetComponent <Renderer> ();

				ShowHideObject.StandardShaderUtils.ChangeRenderMode(r.material, ShowHideObject.StandardShaderUtils.BlendMode.Fade);
				r.material.color = new Color (1,1,1, 0);

				rends[i].Add (r);

			}

		}
			
	}

	IEnumerator FadeInBlock (Renderer rend) {

		float t = 0.0f;
		float opacity = 0.0f;

		while (t < 1.0f) {

			t += Time.deltaTime * 3;
			opacity = startFadeCurve.Evaluate (t);

			rend.material.color = new Color (rend.material.color.r, rend.material.color.g, rend.material.color.b, opacity);
			yield return new WaitForSeconds (0.0f);

		}

		ShowHideObject.StandardShaderUtils.ChangeRenderMode(rend.material, ShowHideObject.StandardShaderUtils.BlendMode.Opaque);
			
	}

	IEnumerator FadeInLevel () {

		float t = 1.0f;
		float waitTime = 0.0f;

		for (int i = 0; i < rends.Count; i++) {

			for (int j = 0; j < rends [i].Count; j++) {

				t -= Time.deltaTime * 10;
				waitTime = startFadeCurve.Evaluate (t);

				StartCoroutine (FadeInBlock (rends [i][j]));
				
			}

			yield return new WaitForSeconds (1.0f);

		}

	}

}
