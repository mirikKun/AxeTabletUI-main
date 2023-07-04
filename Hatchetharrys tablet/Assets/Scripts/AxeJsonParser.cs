using System;
using UnityEngine;

public class AxeJsonParser : MonoBehaviour
{
    [SerializeField] private FromJsonRaspberryPieWebsocket raspberryPieWebsocket;

    public FromJsonRaspberryPieWebsocket ParseRaspberryPieString(string incomingString)
    {
        raspberryPieWebsocket = new FromJsonRaspberryPieWebsocket();
        try
        {
            raspberryPieWebsocket = JsonUtility.FromJson<FromJsonRaspberryPieWebsocket>(incomingString);
        }
        catch (Exception e)
        {
            Debug.Log("wrong axe");
        }

        return raspberryPieWebsocket;
    }
}

[Serializable]
public struct FromJsonRaspberryPieWebsocket
{
    public string cmdType;
    public string state;
    public string deviceId;
    public string deviceName;
    public string ipAddress;
    public string lastReset;
    public bool showMarker;
    public int markerX;
    public int markerY;

    public bool IsDefault()
    {
        return cmdType != null;
    }
}