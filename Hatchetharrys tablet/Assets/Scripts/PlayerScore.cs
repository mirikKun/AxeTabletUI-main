using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private Text userName;
    [SerializeField] private Text userScore;

    public void SetPlayerScore(string newName, string newScore)
    {
        userName.text = newName;
        userScore.text = newScore;
    }
}