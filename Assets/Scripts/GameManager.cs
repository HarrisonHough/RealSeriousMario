using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {InMenu, InGame, Respawning}
public class GameManager : GenericSingleton<GameManager>
{
    public GameState CurrentState { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsLevelComplete { get; private set; }

    private Vector3 _spawnPoint = Vector3.zero;
    private Player _player = null;
    public UIController UIController { get; private set; }
    public HighScores highScores;

    // Start is called before the first frame update
    void Start()
    {
        UIController = FindObjectOfType<UIController>();
        highScores = FindObjectOfType<HighScores>();
    }

    public void UpdateSpawnPoint(Vector3 newSpawnPoint)
    {
        _spawnPoint = newSpawnPoint;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void StartGame(string playerName)
    {
        _player.SetPlayerName(playerName);
        StartCoroutine(GameLoop());
    }

    public void GameOver()
    {
        IsGameOver = true;
    }

    public void LevelComplete()
    {
        IsGameOver = true;
        IsLevelComplete = true;
        _player.AddToScore(1);
        highScores.AddNewScore(_player.playerData);
    }



    IEnumerator GameLoop()
    {
        IsLevelComplete = false;
        IsGameOver = false;
        //need to reset if new game
        _spawnPoint = Vector3.zero;
        _player.Reset();

        while(!IsGameOver)
        {
            yield return StartCoroutine(LevelLoop());
            if(!IsGameOver)
                yield return new WaitForSeconds(2);
        }
        CurrentState = GameState.InMenu;

        if (IsLevelComplete)
            UIController.ToggleLevelCompletePanel(true);
        else
            UIController.ToggleGameOverPanel(true);


    }

    IEnumerator LevelLoop()
    {
        _player.Respawn(_spawnPoint);
        CurrentState = GameState.InGame;
        while (!_player.isDead && !IsGameOver)
        {
            yield return null;
        }
        CurrentState = GameState.Respawning;
    }

}
