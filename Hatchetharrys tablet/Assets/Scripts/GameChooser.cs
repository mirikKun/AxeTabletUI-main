using FancyScrollView.Example02;
using UnityEngine;

public class GameChooser : MonoBehaviour
{
    [SerializeField] private GameObject infinateAxeRules;
    [SerializeField] private GameObject kickAxeRules;
    [SerializeField] private GameObject shadowAxeRules;
    [SerializeField] private GameObject[] objectsToHideInUlimited;

    public enum GameTypes
    {
        InfinateAxe,
        KickAxe,
        ShadowAxe
    }

    public void ChooseGameType(Example02.GameData gameData)
    {
        if (gameData.gameName == GameTypes.InfinateAxe.ToString())
        {
            infinateAxeRules.SetActive(true);
            kickAxeRules.SetActive(false);
            shadowAxeRules.SetActive(false);
            foreach (var objectsToHide in objectsToHideInUlimited)
            {
                objectsToHide.SetActive(false);
            }
        }
        else if (gameData.gameName == GameTypes.KickAxe.ToString())
        {
            infinateAxeRules.SetActive(false);
            kickAxeRules.SetActive(true);
            shadowAxeRules.SetActive(false);
            foreach (var objectsToShow in objectsToHideInUlimited)
            {
                objectsToShow.SetActive(true);
            }
        }
        else if (gameData.gameName == GameTypes.ShadowAxe.ToString())
        {
            infinateAxeRules.SetActive(false);
            kickAxeRules.SetActive(false);
            shadowAxeRules.SetActive(true);
            foreach (var objectsToShow in objectsToHideInUlimited)
            {
                objectsToShow.SetActive(true);
            }
        }
    }
}