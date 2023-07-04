using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UsersAvatarHolder : MonoBehaviour, IDropHandler
{
    [SerializeField] private UserAvatar userAvatarPrefab;
    [SerializeField] private Transform usersAvatarParent;
    [SerializeField] private GameObject textOnClearPanel;
    public Action OnUserAvatarDragged;
    public Action OnUserCountChange;

    public List<User> GetUsers()
    {
        List<User> plyingUsers = new List<User>();
        foreach (var avatar in GetComponentsInChildren<UserAvatar>())
        {
            plyingUsers.Add(avatar.GetUser());
        }

        return plyingUsers;
    }

    public void CreateUser(User newUser)
    {
        var newUserAvatar = Instantiate(userAvatarPrefab, usersAvatarParent);
        newUserAvatar.SetUserAvatar(newUser);
        CheckUserCount();
    }

    public void ClearAvatars()
    {
        foreach (Transform userAvatar in usersAvatarParent)
        {
            Destroy(userAvatar.gameObject);
        }
    }

    public void CheckUserCount()
    {
        textOnClearPanel.SetActive(usersAvatarParent.childCount == 0);
        OnUserCountChange?.Invoke();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Dropped!!!!!!!");

            eventData.pointerDrag.transform.parent = usersAvatarParent;
            Debug.Log(OnUserAvatarDragged);
            OnUserAvatarDragged?.Invoke();
        }
    }
}