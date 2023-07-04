using UnityEngine;
using UnityEngine.UI;

public class RoundCharge : MonoBehaviour
{
    [SerializeField] private Image roundImage;
    [SerializeField] private Sprite grayRound;
    [SerializeField] private Sprite blackRound;

    public void ResetRound()
    {
        roundImage.sprite = grayRound;
    }

    public void UseAxe()
    {
        roundImage.sprite = blackRound;
    }
}