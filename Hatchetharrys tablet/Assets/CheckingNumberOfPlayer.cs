using UnityEngine;
using UnityEngine.UI;

public class CheckingNumberOfPlayer : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    private UsersAvatarHolder _usersAvatarHolder;

    private void Start()
    {
        _usersAvatarHolder = GetComponent<UsersAvatarHolder>();
        _usersAvatarHolder.OnUserCountChange += EnableButton;
    }

    private void EnableButton()
    {
        if (transform.childCount > 0)
        {
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
        }
    }
}