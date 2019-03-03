using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HighScoreData
{
    public PlayerData[] highScoreArray;

    public HighScoreData(List<PlayerData> dataList)
    {
        highScoreArray = new PlayerData[dataList.Count];
        for (int i = 0; i < dataList.Count; i++)
        {
            highScoreArray[i] = dataList[i];
        }
    }
}
