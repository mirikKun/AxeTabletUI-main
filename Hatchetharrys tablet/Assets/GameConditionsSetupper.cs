using System;
using FancyScrollView.Example02;
using UnityEngine;
using UnityEngine.UI;

public class GameConditionsSetupper : MonoBehaviour
{
    [SerializeField] private InputField roundsCount;
    [SerializeField] private InputField throwsCount;

    [SerializeField] private InputField shadowAxeRoundsCount;

    private GameController _gameController;
    [SerializeField] private WebSocketController webSocketController;
    private Example02.GameData _curGameData;

    private void Start()
    {
        _gameController = GetComponentInParent<GameController>();
    }

    public void SetGameData(Example02.GameData gameData)
    {
        _curGameData = gameData;
    }

    public void SetupDefaultConditions()
    {
        int newRoundsCount = Int32.Parse(roundsCount.text);
        int newThrowsCount = Int32.Parse(throwsCount.text);
        _gameController.SetGameConditions(newRoundsCount, newThrowsCount);
        webSocketController.SendGameInfo(newThrowsCount, newRoundsCount, _curGameData.gameName);
    }

    public void SetupUnlimitedGameConditions()
    {
        _gameController.SetUnlimitedGameConditions();
        webSocketController.SendGameInfo(0, 3, _curGameData.gameName);
    }

    public void SetupShadowAxeGameConditions()
    {
        int newRoundsCount = Int32.Parse(shadowAxeRoundsCount.text);
        _gameController.SetGameConditions(newRoundsCount, 10);
        webSocketController.SendGameInfo(10, newRoundsCount, _curGameData.gameName);
    }
}