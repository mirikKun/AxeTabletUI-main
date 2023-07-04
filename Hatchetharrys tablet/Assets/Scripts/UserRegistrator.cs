using UnityEngine;
using UnityEngine.UI;

public class UserRegistrator : MonoBehaviour
{
    private int maxSize = -1;

    [SerializeField] private Image avatar;
    [SerializeField] private InputField userName;

    [SerializeField] private GameObject registrationPanel;

    [SerializeField] private UsersAvatarHolder usersAvatarHolder;
    [SerializeField] private Sprite[] defaultSprites;
    private Sprite _newSprite;
    private int _defaultIconIndex;
    private int _defaultSpriteCount;

    private void Awake()
    {
        _defaultSpriteCount = defaultSprites.Length;
    }

    private void OnEnable()
    {
        avatar.sprite = defaultSprites[_defaultIconIndex];
        _newSprite = defaultSprites[_defaultIconIndex];
        userName.text = "";
        _defaultIconIndex++;
        if (_defaultIconIndex >= _defaultSpriteCount)
        {
            _defaultIconIndex = 0;
        }
    }

    public void GetPhotoSprite()
    {
        Texture2D texture = null;
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            if (path != null)
            {
                texture = NativeCamera.LoadImageAtPath(path, maxSize, false);
                if (texture == null)
                {
                    return;
                }
            }

            if (texture != null)
            {
                if (texture.width < texture.height)
                {
                    _newSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.width),
                        new Vector2(0.5f, 0.5f));
                }
                else
                {
                    _newSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.height, texture.height),
                        new Vector2(0.5f, 0.5f));
                }

                avatar.sprite = _newSprite;
            }
        }, maxSize);
    }

    public void SaveUser()
    {
        if (userName.text != "")
        {
            usersAvatarHolder.CreateUser(new User(_newSprite, userName.text, 0));
            gameObject.SetActive(false);
        }
    }
}