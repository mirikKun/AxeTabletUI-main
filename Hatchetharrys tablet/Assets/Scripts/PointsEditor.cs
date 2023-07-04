using System;
using UnityEngine;
using UnityEngine.UI;

public class PointsEditor : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private GameController gameController;


    public void EditScore()
    {
        gameController.EditLastPoints(Int32.Parse(dropdown.captionText.text));
    }
}