using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    public static void SaveHighScores(List<PlayerData> highScoreList)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/RSMarioScores.rsm";
        //Debug.Log("Saved File at " + path);

        FileStream stream = new FileStream(path, FileMode.Create);

        /*foreach (PlayerData data in playerArray)
        {

            formatter.Serialize(stream, data);
        }*/
        HighScoreData data = new HighScoreData(highScoreList);
        formatter.Serialize(stream, data);
        

        stream.Close();

    }

    public static HighScoreData LoadHighScores()
    {
        string path = Application.persistentDataPath + "/RSMarioScores.rsm";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HighScoreData data = formatter.Deserialize(stream) as HighScoreData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }
}
