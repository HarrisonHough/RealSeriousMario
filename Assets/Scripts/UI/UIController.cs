using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _coinsText;
    [SerializeField]
    private TextMeshProUGUI _worldText;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private TextMeshProUGUI _livesText;

    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _levelCompletePanel;
    [SerializeField]
    private GameObject _highScoresPanel;
    [SerializeField]
    private GameObject _newGamePanel;

    [SerializeField]
    private ScoreLabel[] _scoreLabels;

    private HighScores _highScores;
    [SerializeField]
    private TMP_InputField _nameInput;

    // Start is called before the first frame update
    void Start()
    {
        _highScores = FindObjectOfType<HighScores>();
        InitalizeText();
    }

    private void InitalizeText()
    {
        UpdateScoreText(0);
        UpdateCoinsText(0);
        UpdateTimeText(300);
        UpdateLivesText(3);
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = "SCORE \n" + score;
    }
    public void UpdateCoinsText(int coins)
    {
        _coinsText.text = "COINS \n" + coins;
    }

    public void UpdateTimeText(float timeRemaining)
    {
        _timeText.text = "TIME \n" + (int)timeRemaining;
    }
    public void UpdateLivesText(int lives)
    {
        _livesText.text = "LIVES \n" + lives;
    }

    public void ToggleGameOverPanel(bool show)
    {
        _gameOverPanel.SetActive(show);
    }

    public void ToggleLevelCompletePanel(bool show)
    {
        _levelCompletePanel.SetActive(show);
    }
    public void ToggleHighScoresPanel(bool show)
    {
        if (show)
            UpdateHighScores(_highScores.highScoreList);
        _highScoresPanel.SetActive(show);
    }

    public void ToggleNewGamePanel(bool show)
    {
        _newGamePanel.SetActive(show);
    }

    public void RetryButtonClick()
    {
        ToggleGameOverPanel(false);
        ToggleLevelCompletePanel(false);
        ToggleHighScoresPanel(false);
        ToggleNewGamePanel(true);
    }

    public void ScoresButtonClick()
    {
        ToggleGameOverPanel(false);
        ToggleLevelCompletePanel(false);
        ToggleHighScoresPanel(true);
    }
    public void StartButtonClick()
    {
        string playerName = string.IsNullOrEmpty(_nameInput.text) ? _nameInput.placeholder.GetComponent<TextMeshProUGUI>().text : _nameInput.text ;
        GameManager.Instance.StartGame(playerName);
        ToggleNewGamePanel(false);
    }

    public void UpdateHighScores(IList<PlayerData> highScoreList)
    {
        if (_scoreLabels.Length != highScoreList.Count)
        {
            Debug.LogError("score label array length does not match high score list count");
            Debug.Log("highscorelist count is " + highScoreList.Count + " scorelabels array length is " + _scoreLabels.Length);
            return;
        }
        for (int i = 0; i < highScoreList.Count; i++)
        {
            Debug.Log("setting text label " + i);
            _scoreLabels[i].nameText.text = highScoreList[i].name;
            _scoreLabels[i].scoreText.text = highScoreList[i].score.ToString();
            _scoreLabels[i].coinsText.text = highScoreList[i].coins.ToString();
        }
    }
}
