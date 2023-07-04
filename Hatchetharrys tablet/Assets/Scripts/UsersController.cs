using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UsersController : MonoBehaviour
{
    [SerializeField] private List<User> playingUsers = new List<User>();
    public Action OnNewUsers;
    private User _curUser;
    private int _userCount;
    private int _curUserNumber;

    public int GetUserCount()
    {
        return playingUsers.Count;
    }

    public void AddUsers(List<User> newUser)
    {
        _curUserNumber = 0;
        playingUsers = newUser;
        _userCount = playingUsers.Count;
        OnNewUsers?.Invoke();
    }

    public void ResetUsersScore()
    {
        for (int i = 0; i < _userCount; i++)
        {
            SetUserScore(i, 0);
        }
    }

    public void SetUserScore(int userNumber, int newScore)
    {
        playingUsers[userNumber] =
            new User(playingUsers[userNumber].Avatar, playingUsers[userNumber].UserName, newScore);
    }

    public List<User> GetSortedUsers()
    {
        List<User> sortedUsers = playingUsers.OrderBy(p => p.Score).ToList();
        sortedUsers.Reverse();
        return sortedUsers;
    }

    public User GetCurUser()
    {
        if (playingUsers.Count > 0)
        {
            return playingUsers[_curUserNumber];
        }

        return new User(null, "None", 0);
    }

    public void NextUser()
    {
        _curUserNumber++;
    }

    public void ResetUserNumber()
    {
        _curUserNumber = -1;
    }

    public bool IsItLastUser()
    {
        return _curUserNumber + 1 >= _userCount;
    }

    public void ClearUsers()
    {
        playingUsers.Clear();
    }
}