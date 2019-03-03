using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HighScores : MonoBehaviour
{
    public List<PlayerData> highScoreList;
    public PlayerData[] highScoreArray;
    private int maxHighScores = 10;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        SaveScores();
    }

    public void SaveScores()
    {
        SaveSystem.SaveHighScores(highScoreList);
    }

    public void AddNewScore(PlayerData newPlayerData)
    {
        //check against lowest score
        if (newPlayerData.score <= highScoreList[highScoreList.Count - 1].score)
        {
            return;
        }

        int index = 9;
        for (int i = 0; i < highScoreList.Count; i++)
        {
            if (newPlayerData.score > highScoreList[i].score)
            {
                index = i;
                break;
            }
        }

        PlayerData newDataEntry = new PlayerData(newPlayerData.name, newPlayerData.score, newPlayerData.coins);
        highScoreList.Insert(index, newDataEntry);

        //ensure there is always only 10 high scores
        LogScores();
        Debug.Log("list count = " + highScoreList.Count);
        highScoreList.RemoveAt(highScoreList.Count-1);
        Debug.Log("list count is NOW = " + highScoreList.Count);
        LogScores();
        //now save scores
        SaveScores();

    }



    private void Initialize()
    {
        highScoreList = new List<PlayerData>();
        //check for save file
        HighScoreData savedData = SaveSystem.LoadHighScores();

        if (savedData != null)
        {
            foreach (PlayerData data in savedData.highScoreArray)
            {
                highScoreList.Add(data);
            }
        }
        else
        {
            for (int i = 0; i < maxHighScores; i++)
            {
                //create temp data
                PlayerData tempData = new PlayerData();
                highScoreList.Add(tempData);
            }
        }

        
    }

    private void LogScores()
    {
        for (int i = 0; i < highScoreList.Count; i++)
        {
            Debug.Log(highScoreList[i].name + " " + highScoreList[i].score + " " + highScoreList[i].coins);
        }
    }

    



}
