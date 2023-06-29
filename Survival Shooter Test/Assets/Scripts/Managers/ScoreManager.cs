using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour, IDataPersistence
{

    public static int score;

    Text text;

    public void LoadData(GameData data)
    {
        score = data.scoreData.score; 
    }

    public void SaveData(GameData data)
    {
        data.scoreData.score = score;
    }

    void Awake ()
    {
        text = GetComponent <Text> ();
        //score = 0;
    }

    void Update ()
    {
        text.text = "Score: " + score;
    }

}