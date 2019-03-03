using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable
{
    public string playerName { get { return playerData.name; } private set { playerData.name = value; } }
    public int score { get { return playerData.score; } private set { playerData.score = value; } }
    public int coins { get { return playerData.coins; } private set { playerData.coins = value; } }

    public PlayerData playerData { get; private set; }
    private int _lives;
    [SerializeField]
    private int _maxLives;

    private float _timeRemaining;
    [SerializeField]
    private float _maxTime;
    
    public bool isDead { get; private set; }

    private UIController _uiController;
    private CameraShake _cameraShake;
    private void Start()
    {
        playerData = new PlayerData();
        GameManager.Instance.SetPlayer(this);
        _uiController = FindObjectOfType<UIController>();
        _cameraShake = FindObjectOfType<CameraShake>();

    }

    private void Update()
    {
        if (GameManager.Instance.currentState != GameState.InGame)
            return;
        _timeRemaining -= Time.deltaTime;
        _uiController.UpdateTimeText(_timeRemaining);


        if (_timeRemaining <= 0)
        {
            //game over;
            GameManager.Instance.GameOver();
        }
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void Kill()
    {
        //play death effects
        _lives--;
        _uiController.UpdateLivesText(_lives);
        if (_lives <= 0)
        {
            //game over
            GameManager.Instance.GameOver();
            return;
        }
        _cameraShake.StartShakeEffect();
        ParticleEffect particles = GameManager.Instance.destroyParticlesPool.GetObject().GetComponent<ParticleEffect>();
        particles.PlayAtPosition(transform.position);
        isDead = true;
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        _uiController.UpdateScoreText(score);
    }

    public void Respawn(Vector3 spawnPoint)
    {
        isDead = false;
        score = 0;
        _timeRemaining = _maxTime;
        transform.position = spawnPoint;
    }

    public void Reset()
    {
        score = 0;
        coins = 0;
        _lives = _maxLives;
        _timeRemaining = _maxTime;
    }


}
