using UnityEngine;
using UnityEngine.UI;

public class AxeCharge : MonoBehaviour
{
    [SerializeField] private Image axeImage;
    [SerializeField] private Sprite grayAxe;
    [SerializeField] private Sprite redAxe;
    [SerializeField] private Image miss;

    public void ResetAxe()
    {
        axeImage.sprite = grayAxe;
        miss.enabled = false;
    }

    public void UseAxe()
    {
        axeImage.sprite = redAxe;
    }

    public void ShowMiss()
    {
        miss.enabled = true;
    }
}