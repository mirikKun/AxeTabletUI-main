using UnityEngine;

public static class ScoreCounter
{
    private static int _openCVTargetXSize = 300;
    private static int _openCVTargetYSize = 300;
    private static float firstСircleFactor = 0.18f;
    private static float secondСircleFactor = 0.40f;
    private static float thirdСircleFactor = 0.68f;

    private static float _unityTargetXSize = 600;
    private static float _unityTargetYSize = 600;

    public static int CalculateScore(int xPos, int yPos)
    {
        int curPoints;
        var newPos = new Vector2(
            (xPos - _openCVTargetXSize / 2) * _unityTargetXSize / _openCVTargetXSize,
            -(yPos - _openCVTargetYSize / 2) * _unityTargetYSize / _openCVTargetYSize);
        var distance = Vector2.Distance(newPos, Vector2.zero);
        if (distance / (_unityTargetXSize / 2) < firstСircleFactor)
        {
            curPoints = 5;
        }
        else if (distance / (_unityTargetXSize / 2) < secondСircleFactor)
        {
            curPoints = 3;
        }
        else if (distance / (_unityTargetXSize / 2) < thirdСircleFactor)
        {
            curPoints = 1;
        }
        else
        {
            curPoints = 0;
        }

        return curPoints;
    }


    private static float startShadowAxeCircleFactor = 0.8f;

    public static int CalculateShadowAxeScore(int xPos, int yPos, int numberOfHits)
    {
        int curPoints;
        var newPos = new Vector2(
            (xPos - _openCVTargetXSize / 2) * _unityTargetXSize / _openCVTargetXSize,
            -(yPos - _openCVTargetYSize / 2) * _unityTargetYSize / _openCVTargetYSize);
        var distance = Vector2.Distance(newPos, Vector2.zero);
        if (distance / (_unityTargetXSize / 2) < startShadowAxeCircleFactor - 0.07 * numberOfHits)
        {
            curPoints = numberOfHits + 1;
        }
        else
        {
            curPoints = 0;
        }


        return curPoints;
    }
}