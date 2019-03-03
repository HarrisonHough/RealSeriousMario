using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {InMenu, InGame, Respawning}
public class GameManager : GenericSingleton<GameManager>
{
    public GameState currentState { get; private set; }
    public bool isGameOver { get; private set; }
    public bool isLevelComplete { get; private set; }

    private Vector3 _spawnPoint = Vector3.zero;
    private Player _player = null;
    public UIController uiController { get; private set; }
    public HighScores highScores;
    public Pool destroyParticlesPool;

    // Start is called before the first frame update
    void Start()
    {
        uiController = FindObjectOfType<UIController>();
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
        isGameOver = true;
    }

    public void LevelComplete()
    {
        isGameOver = true;
        isLevelComplete = true;
        _player.AddToScore(1);
        highScores.AddNewScore(_player.playerData);
    }



    IEnumerator GameLoop()
    {
        isLevelComplete = false;
        isGameOver = false;
        //need to reset if new game
        _spawnPoint = Vector3.zero;
        _player.Reset();

        while(!isGameOver)
        {
            yield return StartCoroutine(LevelLoop());
            if(!isGameOver)
                yield return new WaitForSeconds(2);
        }
        currentState = GameState.InMenu;

        if (isLevelComplete)
            uiController.ToggleLevelCompletePanel(true);
        else
            uiController.ToggleGameOverPanel(true);


    }

    IEnumerator LevelLoop()
    {
        _player.Respawn(_spawnPoint);
        currentState = GameState.InGame;
        while (!_player.isDead && !isGameOver)
        {
            yield return null;
        }
        currentState = GameState.Respawning;
    }

}
