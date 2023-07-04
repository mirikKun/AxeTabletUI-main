using System;
using FancyScrollView.Example02;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text[] scoreTexts;
    [SerializeField] private Text curPoints;


    [SerializeField] private Image gameTextImage;
    [SerializeField] private Image gameIcon;

    [SerializeField] private GameObject nextThrowButton;
    [SerializeField] private GameObject nextPlayerButton;
    [SerializeField] private GameObject showResultsButton;

    [SerializeField] private GameObject detectPanel;
    [SerializeField] private GameObject curThrowPanel;

    private int _maxRoundsCount = 3;
    private int _maxAttemptsCount = 5;
    public Action<User> OnNewUser;
    private int _curAttemptsCount;
    private int _curScore;
    private int _curNewPoints;
    private int _curRound;
    private int _numberOfHits;
    private int _userCount;
    private int _curUserNumber = 0;
    private UsersController _usersController;
    private GamePanelChanger _gamePanelChanger;
    private AxeChargesController _axeChargesController;
    private RoundChargesController _roundChargesController;
    private Example02.GameData _curGameData;

    private void Start()
    {
        _usersController = GetComponent<UsersController>();
        _gamePanelChanger = GetComponent<GamePanelChanger>();
        _axeChargesController = GetComponent<AxeChargesController>();
        _roundChargesController = GetComponent<RoundChargesController>();
        _usersController.OnNewUsers += SetUserCount;
        ResetGame();
    }

    public void SetUserCount()
    {
        _userCount = _usersController.GetUserCount();
    }

    public void SetUnlimitedGameConditions()
    {
        _axeChargesController.ResetAxeCharges();
        _roundChargesController.ResetRoundCharges();
        _maxRoundsCount = 1;
        _maxAttemptsCount = 999999;
        _curAttemptsCount = _maxAttemptsCount;
        _curRound = 0;


        detectPanel.SetActive(true);
        nextThrowButton.SetActive(true);
        nextPlayerButton.SetActive(false);
        showResultsButton.SetActive(false);
        curThrowPanel.SetActive(false);
    }

    public void SetGameConditions(int roundsCount, int throwsCount)
    {
        _axeChargesController.ResetAxeCharges();
        _roundChargesController.ResetRoundCharges();
        _maxRoundsCount = roundsCount;
        _maxAttemptsCount = throwsCount;
        _curAttemptsCount = _maxAttemptsCount;

        _axeChargesController.CreateAxeCharges(throwsCount);
        _roundChargesController.CreateRoundCharges(roundsCount);
        _curRound = 0;


        detectPanel.SetActive(true);
        nextThrowButton.SetActive(true);
        nextPlayerButton.SetActive(false);
        showResultsButton.SetActive(false);
        curThrowPanel.SetActive(false);
    }

    public void SetGameData(Example02.GameData gameData)
    {
        _curGameData = gameData;
        gameTextImage.sprite = gameData.gameNameImage;
        gameIcon.sprite = gameData.gameIcon;
    }

    public void SetFirstUser()
    {
        _curUserNumber = 0;
        _curAttemptsCount = _maxAttemptsCount;
        OnNewUser?.Invoke(_usersController.GetCurUser());
    }

    public void SetNewUser()
    {
        _curUserNumber++;
        _usersController.NextUser();
        ResetScore(false);
        _curAttemptsCount = _maxAttemptsCount;
        nextPlayerButton.SetActive(false);
        nextThrowButton.SetActive(true);
        OnNewUser?.Invoke(_usersController.GetCurUser());
    }

    public void UseAxe(bool miss)
    {
        if (_curAttemptsCount > 0)
        {
            _axeChargesController.UseAxe(_curAttemptsCount - 1, miss);

            _curAttemptsCount--;
            if (_curAttemptsCount < 1)
            {
                if (!_usersController.IsItLastUser())
                {
                    nextPlayerButton.SetActive(true);
                    nextThrowButton.SetActive(false);
                }
                else if (_curRound + 1 < _maxRoundsCount)
                {
                    _curUserNumber = -1;
                    _usersController.ResetUserNumber();
                    nextPlayerButton.SetActive(true);
                    nextThrowButton.SetActive(false);
                    _roundChargesController.RoundPass(_curRound);
                    _curRound++;
                }
                else
                {
                    _roundChargesController.RoundPass(_curRound);

                    showResultsButton.SetActive(true);
                    nextThrowButton.SetActive(false);
                }
            }
        }
    }

    public int GetScore()
    {
        return _curScore;
    }

    public int GetNewPoints()
    {
        return _curNewPoints;
    }

    public int GetCurRound()
    {
        return _curRound;
    }

    public void ResetGame()
    {
        _curUserNumber = 0;
        ResetScore(true);
    }

    public void ResetScore(bool resetScore)
    {
        _numberOfHits = 0;
        nextPlayerButton.SetActive(false);
        _axeChargesController.ResetAxeCharges();
        _curAttemptsCount = _maxAttemptsCount;
        if (resetScore)
        {
            _usersController.ResetUsersScore();
        }

        _curScore = _usersController.GetCurUser().Score;
        UpdateScoreText();
    }

    public void SetNewScore(int x, int y)
    {
        int newScore = 0;
        if (x > 0 && y > 0)
        {
            if (_curGameData.gameName != GameChooser.GameTypes.ShadowAxe.ToString())
            {
                newScore = ScoreCounter.CalculateScore(x, y);
            }
            else
            {
                Debug.Log(_maxAttemptsCount - _curAttemptsCount);
                newScore = ScoreCounter.CalculateShadowAxeScore(x, y, _numberOfHits);
            }

            _numberOfHits++;
        }


        _curScore += newScore;
        _curNewPoints = newScore;
        UpdateNewPointsText(newScore);
        _usersController.SetUserScore(_curUserNumber, _curScore);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        foreach (var scoreText in scoreTexts)
        {
            scoreText.text = _curScore.ToString();
        }

        UpdateRoundText();
    }

    private void UpdateRoundText()
    {
        //roundNumber.text = (_curRound + 1).ToString();
    }

    private void UpdateNewPointsText(int newPoints)
    {
        curPoints.text = newPoints.ToString();
    }

    public bool AttemptsIsLeft()
    {
        return _curAttemptsCount > 0;
    }

    public void EditLastPoints(int correctPoints)
    {
        _curScore = _curScore - _curNewPoints + correctPoints;
        _curNewPoints = correctPoints;
        UpdateNewPointsText(_curScore);
        _usersController.SetUserScore(_curUserNumber, _curScore);
        UpdateScoreText();
    }
}