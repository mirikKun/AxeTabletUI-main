using System.Collections.Generic;
using UnityEngine;

public class UsersAvatarController : MonoBehaviour
{
    [SerializeField] private UsersAvatarHolder avatarHolderRegistered;
    [SerializeField] private UsersAvatarHolder avatarHolderReadyToPlay;
    [SerializeField] private UsersController usersController;
    [SerializeField] private WebSocketController webSocketController;
    private Transform _currentDraggingUser;

    public void TryToStartGame()
    {
        if (avatarHolderReadyToPlay.transform.childCount > 0)
        {
            usersController.AddUsers(avatarHolderReadyToPlay.GetUsers());
        }
    }

    private void OnEnable()
    {
        avatarHolderRegistered.OnUserAvatarDragged += CheckHolders;
        avatarHolderReadyToPlay.OnUserAvatarDragged += CheckHolders;
    }

    private void OnDisable()
    {
        avatarHolderRegistered.OnUserAvatarDragged -= CheckHolders;
        avatarHolderReadyToPlay.OnUserAvatarDragged -= CheckHolders;
    }

    private void CheckHolders()
    {
        avatarHolderRegistered.CheckUserCount();
        avatarHolderReadyToPlay.CheckUserCount();
    }

    public void CreateUsersAvatarPool(List<User> users)
    {
        avatarHolderRegistered.ClearAvatars();
        avatarHolderReadyToPlay.ClearAvatars();
        foreach (var user in users)
        {
            avatarHolderRegistered.CreateUser(user);
        }

        avatarHolderRegistered.CheckUserCount();
        avatarHolderReadyToPlay.CheckUserCount();
    }
}