using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//--------------   WHAT THIS IS -----------------------
// A handy dandy class for handling fade in/out between levels/scenes/etc.
//-----------------------------------------------------


public class fade : MonoBehaviour {
    public Texture2D fadeOutTexture; //texture overlay that will fade...
    public float fadeSpeed = 0.8f;  //fading speed


    private int drawDepth = -1000; //texture's draw order, thus rendering on top of everything.
    private float alpha = 1.0f; //texture's alpha is between 0 and 1
    private int fadeDir = -1;  // the fade direction: in = -1  or out =1


    void OnGUI()
    {
        //fade out/in using a dir, speed, and Time.deltatime
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        //clamp the num between 0 & 1
        alpha = Mathf.Clamp01(alpha);

        //set color of GUI. All RGBA vals remain the same except the alpha.
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha); //set new alpha
        GUI.depth = drawDepth;  //render on top of everything.

        //TL;DR: draw a rectangle all over the screen, on top of everything.
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);

    }

    //sets fadeDir either in or out
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);  //returns fade speed to time SceneManager.LoadScene .
    }


    //OnLevelWasLoaded is deprecated but I kept it here to follow the tutorial & know what's up
    /* void OnLevelWasLoaded(){
     * //alpha =1;
     * BeginFade(-1); //call the fade in function
     * 
     * }
     * */

    void onEnable()
    {
        //tell OnLevelFinishedLoading to start listening for a scene change.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

    }

    void onDisable()
    {
        //tell OnLevelFinishedLoading to stop listening for scene changes.
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        alpha = 1.0f; //in case Alpha is not 1 by default.
        BeginFade(-1);
    }
}
