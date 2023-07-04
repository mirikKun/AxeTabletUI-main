using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct User : IComparer<User>
{
    public User(Sprite newAvatar, string newUserName, int newScore)
    {
        Avatar = newAvatar;
        UserName = newUserName;
        Score = newScore;
    }


    public Sprite Avatar { get; set; }
    public string UserName { get; set; }
    public int Score { get; set; }

    public int Compare(User x, User y)
    {
        if (x.Score > y.Score)
        {
            return 1;
        }

        if (x.Score < y.Score)
        {
            return -1;
        }

        return 1;
    }
}