using UnityEngine;
using System.Collections;

public class ShowHideObject : MonoBehaviour {

	//This script will change the transparency of platforms
	//to 50% opacity so that the player can see throught them

	public Renderer rend;  			//The renderer attached to this object
	public float alpha = 0.3f;		//The alpha value of the 

	private Color initialColor; 	//The initialColor of the renderer
	private Color hiddenColor;		//The color of the renderer when it is "hidden"
	private bool isHidden = false;  //Whether or not the object is hidden.


	// Use this for initialization
	void Awake () {
		
		rend = GetComponent <Renderer> ();
		initialColor = rend.material.color;
		hiddenColor = new Color (rend.material.color.r, rend.material.color.g, rend.material.color.b, alpha);

	}
	
	// Update is called once per frame
	void Update () {

		if (isHidden) {
			Show ();
		}
	
	}

	public void Show () {
		StandardShaderUtils.ChangeRenderMode(rend.material, StandardShaderUtils.BlendMode.Opaque);
		rend.material.color = initialColor;
		isHidden = false;

	}

	public void Hide () {
		StandardShaderUtils.ChangeRenderMode(rend.material, StandardShaderUtils.BlendMode.Fade);
		rend.material.color = hiddenColor;
		isHidden = true;

	}

	public static class StandardShaderUtils
	{
		public enum BlendMode
		{
			Opaque,
			Cutout,
			Fade,
			Transparent
		}

		public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
		{
			switch (blendMode)
			{
			case BlendMode.Opaque:
				standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				standardShaderMaterial.SetInt("_ZWrite", 1);
				standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
				standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
				standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				standardShaderMaterial.renderQueue = -1;
				break;
			case BlendMode.Cutout:
				standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				standardShaderMaterial.SetInt("_ZWrite", 1);
				standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
				standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
				standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				standardShaderMaterial.renderQueue = 2450;
				break;
			case BlendMode.Fade:
				standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				standardShaderMaterial.SetInt("_ZWrite", 0);
				standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
				standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
				standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				standardShaderMaterial.renderQueue = 3000;
				break;
			case BlendMode.Transparent:
				standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				standardShaderMaterial.SetInt("_ZWrite", 0);
				standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
				standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
				standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
				standardShaderMaterial.renderQueue = 3000;
				break;
			}

		}
	}

}
