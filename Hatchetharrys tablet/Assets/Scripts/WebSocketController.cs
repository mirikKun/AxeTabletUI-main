using System;
using WebSocketSharp;
using UnityEngine;
using UnityEngine.UI;

public class WebSocketController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private InputField ipAddressInputField;
    private WebSocket _webSocket;
    private AxeJsonParser _jsonParser;
    [SerializeField] private GamePanelChanger panelChanger;

    [SerializeField] private Button startButton;
    [SerializeField] private Button playfieldButton;
    
    private FromJsonRaspberryPieWebsocket _raspberryPieWebsocket;
    private bool _axeMessageReceived;
    private bool _onConnected;
    private bool _onClosed;
    private bool _gameStarted;
    private bool _readyToPlay;


    public void SetReadyToPlay(bool readyToPlay)
    {
        _readyToPlay = readyToPlay;
    }

    private void Start()
    {
        gameController.OnNewUser += SendUserInfo;
        _jsonParser = GetComponent<AxeJsonParser>();
        ipAddressInputField.text = GetLocalIPAddress();
    }

    public static string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return "0.0.0.0";
    }

    public void ConnectWebSocket()
    {
        _webSocket = new WebSocket("ws://" + ipAddressInputField.text + ":8000/ws/123");

        _webSocket.Log.Output = (data, s) => { Debug.Log(data.ToString()); };
        _webSocket.OnMessage += (sender, e) =>
        {
            Debug.Log("OnMessage");
            Debug.Log(e.Data);
            var newRaspberryPieWebsocket = _jsonParser.ParseRaspberryPieString(e.Data);
            if (!newRaspberryPieWebsocket.IsDefault())
            {
                Debug.Log("Axe isnt def");

                _raspberryPieWebsocket = newRaspberryPieWebsocket;
                _axeMessageReceived = true;
            }
        };
        _webSocket.OnOpen += (sender, e) =>
        {
            Debug.Log("OnOpen");
            Debug.Log(e.ToString());
            _onConnected = true;
        };
        _webSocket.OnClose += (sender, e) =>
        {
            Debug.Log("OnClose");
            Debug.Log(e.Reason);
            _onClosed = true;
        };
        _webSocket.ConnectAsync();
    }

    private void Update()
    {
        if (_onConnected)
        {
            _onConnected = false;
            startButton.interactable = true;
            playfieldButton.interactable = true;
        }

        if (_onClosed)
        {
            _onClosed = false;
            startButton.interactable = false;
            playfieldButton.interactable = false;

            panelChanger.ChangeToEnterPanel();
        }
    }
    
    private void DetectHit()
    {
        gameController.UseAxe(_raspberryPieWebsocket.markerX < 0 || _raspberryPieWebsocket.markerY < 0);
    }

    public void SendUserInfo(User userInfo)
    {
        byte[] imageData = userInfo.Avatar.texture.EncodeToPNG();
        string spriteInBytes = Convert.ToBase64String(imageData);

        _webSocket.Send("{\"cmdType\": \"newUser\", \"userName\":\"" + userInfo.UserName + "\", \"imageInBytes\": \"" +
                        spriteInBytes + "\", \"score\":" + 0 + ", \"roundNumber\":" +
                        (gameController.GetCurRound() + 1) + "}");
    }

    public void SendGameInfo(int newThrowsCount, int newRoundsCount, string gameName)
    {
        _webSocket.Send("{\"cmdType\": \"throwsCount\", \"throwsCount\":" + newThrowsCount + ", \"roundsCount\":" +
                        newRoundsCount + ",\"gameName\": \"" + gameName + "\"}");
    }

    public void SetScoreFromCoordinates()
    {
        if (gameController.AttemptsIsLeft())
        {
            gameController.SetNewScore(_raspberryPieWebsocket.markerX, _raspberryPieWebsocket.markerY);
            gameController.UseAxe(_raspberryPieWebsocket.markerX < 0 || _raspberryPieWebsocket.markerY < 0);
            SendScoreWebsocket();
        }
    }

    public void ChangeToResults()
    {
        if (!_readyToPlay)
            return;
        _gameStarted = false;
        panelChanger.ChangeToGameResultsPanel();
    }

    public void SendBestUser(User bestUser)
    {
        byte[] imageData = bestUser.Avatar.texture.EncodeToPNG();

        string spriteInBytes = Convert.ToBase64String(imageData);

        _webSocket.Send("{\"cmdType\": \"bestUser\", \"userName\":\"" + bestUser.UserName + "\", \"imageInBytes\": \"" +
                        spriteInBytes + "\", \"score\": " + bestUser.Score + "}");
    }

    public void SendMissWebsocket()
    {
        if (gameController.AttemptsIsLeft())
        {
            gameController.SetNewScore(-1, -1);
            gameController.UseAxe(true);
            _webSocket.Send("{ \"cmdType\": \"state\", \"state\": \"COMPLETE\" , \"markerX\": -1,\"markerY\": -1 }");
            SendScoreWebsocket();
        }
    }

    public void SendStateWebsocket(string state)
    {
        _webSocket.Send("{ \"cmdType\": \"state\", \"state\": \"" + state + "\", \"markerX\": " +
                        _raspberryPieWebsocket.markerX + ",\"markerY\": " + _raspberryPieWebsocket.markerY + " }");
    }

    public void SendScoreWebsocket()
    {
        int allScore = gameController.GetScore();
        int newPoints = gameController.GetNewPoints();
        _webSocket.Send("{\"cmdType\": \"gameScore\", \"gameType\":\"SimpleTarget\", \"allScore\": \"" +
                        allScore + "\", \"newPoints\": \"" + newPoints + "\"}");
    }

    public void SendEmptyPlayFieldCommand()
    {
        _webSocket.Send("{ \"empty\": \"state\", \"state\": \"PLAYFIELD\" , \"markerX\": -1,\"markerY\": -1 }");
    }

    private void OnApplicationQuit()
    {
        _webSocket?.Close();
    }
}