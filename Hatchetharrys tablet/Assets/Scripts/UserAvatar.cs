using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserAvatar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image avatar;
    [SerializeField] private Text userName;
    [SerializeField] private CanvasGroup canvasGroup;
    private RectTransform _transform;
    private Transform _lastParent;
    private UsersAvatarController _usersAvatarController;

    private User _curUser;

    private void Start()
    {
        _usersAvatarController = GetComponentInParent<UsersAvatarController>();
        _transform = GetComponent<RectTransform>();
        _lastParent = _transform.parent;
    }


    public void SetUserAvatar(User newUser)
    {
        avatar.sprite = newUser.Avatar;
        userName.text = newUser.UserName;
        _curUser = newUser;
    }

    public User GetUser()
    {
        return _curUser;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastParent = _transform.parent;
        canvasGroup.blocksRaycasts = false;
        _transform.parent = _usersAvatarController.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        canvasGroup.blocksRaycasts = true;
        if (_transform.parent == _usersAvatarController.transform)
        {
            _transform.parent = _lastParent;
        }
    }
}