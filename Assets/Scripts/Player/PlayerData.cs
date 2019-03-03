using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int score;
    public int coins;

    public PlayerData()
    {
        name = "------";
        score = 0;
        coins = 0;
    }

    public PlayerData(Player player)
    {
        name = player.name;
        score = player.score;
        coins = player.coins;

    }

    public PlayerData(string name, int score, int coins)
    {
        this.name = name;
        this.score = score;
        this.coins = coins;
    }
}
