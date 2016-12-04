

using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//--------------   WHAT THIS IS -----------------------
//this is a game engine managing saving and loading files using XML.
//it also incorporates the fade class for fading in/out between scenes
//-----------------------------------------------------

   
public class SceneObjectData
{
    //object name
    [XmlAttribute("name")]
    public string name;
    //object x,y,z positions
    [XmlAttribute("posX")]
    public float posX;
    [XmlAttribute("posY")]
    public float posY;
    [XmlAttribute("posZ")]
    public float posZ;
    //object x,y,z angles  GOSHDARNIT IT DOESN'T WORK
   //  [XmlAttribute("quaternion")]
    //  public Quaternion quaternion;

    //object health
   // [XmlAttribute("health")]
   // public int health;
}

[XmlRoot("SceneData")]
public class SceneData
{
    [XmlArray("SceneObjectDatas")]
    [XmlArrayItem("SceneObjectData")]
    public List<SceneObjectData> sceneObjectDatas = new List<SceneObjectData>();
}

public class gameEngine : MonoBehaviour
{
   

   


    void Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SceneData));
        FileStream stream = new FileStream(Application.dataPath + "/savedData.xml", FileMode.Open);
        SceneData sceneData = serializer.Deserialize(stream) as SceneData;
        stream.Close();

        for (int i = 0; i < sceneData.sceneObjectDatas.Count; ++i)
        {
            GameObject.Find(sceneData.sceneObjectDatas[i].name).transform.position = new Vector3(sceneData.sceneObjectDatas[i].posX, sceneData.sceneObjectDatas[i].posY, sceneData.sceneObjectDatas[i].posZ);
          //  GameObject.Find(sceneData.sceneObjectDatas[i].name).transform.rotation = sceneData.sceneObjectDatas[i].quaternion;

        }
    }

    void Save()
    {
        GameObject[] allGameObject = GameObject.FindObjectsOfType<GameObject>();
        SceneData sceneData = new SceneData();
        for (int i = 0; i < allGameObject.Length; ++i)
        {
            SceneObjectData sceneObjectData = new SceneObjectData();
            sceneObjectData.name = allGameObject[i].name;
            sceneObjectData.posX = allGameObject[i].transform.position.x;
            sceneObjectData.posY = allGameObject[i].transform.position.y;
            sceneObjectData.posZ = allGameObject[i].transform.position.z;
           // sceneObjectData.quaternion = allGameObject[i].transform.rotation;
            sceneData.sceneObjectDatas.Add(sceneObjectData);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(SceneData));
        FileStream stream = new FileStream(Application.dataPath + "/savedData.xml", FileMode.Create);
        serializer.Serialize(stream, sceneData);
        stream.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {

           // SceneManager.LoadScene("test-saveAndLoad");
            Load();
            
        }
        if (Input.GetKeyDown(KeyCode.S))
            Save();
    }
}

//fade class


//--------------   WHAT THIS IS -----------------------
// A handy dandy class for handling fade in/out between levels/scenes/etc.
//-----------------------------------------------------


public class Fade : MonoBehaviour
{
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

