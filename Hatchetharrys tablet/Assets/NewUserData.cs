using UnityEngine;

public class NewUserData : MonoBehaviour
{
    [SerializeField] private WebSocketController webSocketController;
    [SerializeField] private UsersController usersController;

    private void OnEnable()
    {
        webSocketController.SendUserInfo(usersController.GetCurUser());
    }
}