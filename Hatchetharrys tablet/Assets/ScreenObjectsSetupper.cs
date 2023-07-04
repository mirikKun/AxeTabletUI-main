using UnityEngine;

public class ScreenObjectsSetupper : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject[] objectsToShow;

    private void OnEnable()
    {
        foreach (var objectToHide in objectsToHide)
        {
            objectToHide.SetActive(false);
        }

        foreach (var objectToShow in objectsToShow)
        {
            objectToShow.SetActive(true);
        }
    }
}