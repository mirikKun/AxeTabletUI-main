using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardLoader : MonoBehaviour
{
    [SerializeField] private Image bestAvatar;
    [SerializeField] private Text bestName;
    [SerializeField] private Text bestScore;


    [SerializeField] private PlayerScore[] playerScores;
    [SerializeField] private WebSocketController webSocketController;
    private UsersController _usersController;
    [SerializeField] private List<User> _sortedUsers = new List<User>();

    private void Awake()
    {
        _usersController = GetComponentInParent<UsersController>();
        Debug.Log(_usersController);
    }

    private void OnEnable()
    {
        _sortedUsers = _usersController.GetSortedUsers();

        if (_sortedUsers.Count > 0)
        {
            bestAvatar.sprite = _sortedUsers[0].Avatar;
            bestName.text = _sortedUsers[0].UserName;

            bestScore.text = _sortedUsers[0].Score.ToString();
            webSocketController.SendBestUser(_sortedUsers[0]);


            for (int i = 0; i < playerScores.Length; i++)
            {
                if (_sortedUsers.Count > i + 1)
                {
                    playerScores[i].SetPlayerScore(_sortedUsers[i + 1].UserName, _sortedUsers[i + 1].Score.ToString());
                }
                else
                {
                    playerScores[i].SetPlayerScore("", "");
                }
            }
        }
    }
}