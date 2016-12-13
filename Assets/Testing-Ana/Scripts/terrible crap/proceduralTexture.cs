using UnityEngine;
using System.Collections;

public class proceduralTexture : MonoBehaviour {
    public int tileResolution = 8;
    public int sizeX = 10;
    public int sizeY = 10;

    void BuildTexture()
    {
        Texture2D procTexture = new Texture2D(sizeX * tileResolution , sizeY *tileResolution);
        int texWidth = 10;
        int texHeight = 10;

        for(int y=0; y < texHeight; y++)
        {

            for (int x = 0; x < texWidth; x++)
            {
                Debug.Log("SHIT");
            }
        }

        Debug.Log("Done texture");
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
