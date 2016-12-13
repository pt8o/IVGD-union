/*
 * THIS IS TRASH LOL
 * 
using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;


[XmlRoot("DataCollection")]
public class DataCollection
{

    [XmlArray("PlayerDatas")]
    [XmlArrayItem("PlayerData")]
    public List<DataXML> playerdatas = new List<DataXML>();

}

[System.Serializable]
public class DataXML
{
    [XmlAttribute("Name")]
    public string objname;

    [XmlElement("PosX")]
    public float posX;

    [XmlElement("PosY")]
    public float posY;

    [XmlElement("PosZ")]
    public float posZ;

    [XmlElement("level")]
    public int lvl;
}

public class Data : MonoBehaviour
{
    public DataXML data = new DataXML();
    public string objname = "Player";

    public int lvl = 0;

/*
 * Let's ignore this function for now.
    public void StoreData()
    {
        data.objname = objname;
        Vector3 pos = GameObject.Find("Player").transform.position;
        lvl = GameObject.Find("LevelMaster").GetComponent<levelLoad>().levelCount;
        data.posX = pos.x;
        data.posY = pos.y;
        data.posZ = pos.z;
        data.lvl = lvl;
    }
*/
/*
    public void LoadData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DataCollection));
        FileStream stream = new FileStream(Application.dataPath + "/savedData.xml", FileMode.Open);
        DataCollection sceneData = serializer.Deserialize(stream) as DataCollection;
        stream.Close();


        for (int i = 0; i <= sceneData.playerdatas.Count; ++i)  //considering it's ONE element, it should be done once.
        {
              GameObject.Find("Player").transform.position = new Vector3(data.posX, data.posY, data.posZ);
            GameObject.Find("LevelMaster").GetComponent<levelLoad>().levelCount = data.lvl;
            //transform.position = new Vector3(data.posX, data.posY, data.posZ);
            GameObject.Find("LevelMaster").GetComponent<levelLoad>().levelCount = data.lvl;
            Debug.Log("loaded");
        }
        

    }

    public void SaveData()
    {
        DataXML sceneData = new DataXML();

        data.objname = objname;
        Vector3 pos = GameObject.Find("Player").transform.position;
        lvl = GameObject.Find("LevelMaster").GetComponent<levelLoad>().levelCount;
        data.posX = pos.x;
        data.posY = pos.y;
        data.posZ = pos.z;
        data.lvl = lvl;
        XmlSerializer serializer = new XmlSerializer(typeof(DataCollection));
        FileStream stream = new FileStream(Application.dataPath + "/savedData.xml", FileMode.Create);
        serializer.Serialize(stream, sceneData);
        stream.Close();
        Debug.Log("saved " + data.objname + " at level " + data.lvl + " and positions" + data.posX + "," + data.posY + "," + data.posZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }

    }

}
*/


