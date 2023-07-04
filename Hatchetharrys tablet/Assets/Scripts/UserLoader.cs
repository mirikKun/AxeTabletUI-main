using UnityEngine;
using UnityEngine.UI;

public class UserLoader : MonoBehaviour
{
    [SerializeField] private Image userPhoto;
    [SerializeField] private Text userNameImage;
    private UsersController _usersController;
    private User _curUser;

    private void Awake()
    {
        _usersController = FindObjectOfType<UsersController>();
    }

    private void OnEnable()
    {
        _curUser = _usersController.GetCurUser();
        userPhoto.sprite = _curUser.Avatar;
        userNameImage.text = _curUser.UserName;
    }
}