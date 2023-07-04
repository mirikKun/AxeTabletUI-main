using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldContentChecker : MonoBehaviour
{
    private InputField _inputField;

    [SerializeField] private int maxCount;
    [SerializeField] private int defaultCount;

    private void Start()
    {
        _inputField = GetComponent<InputField>();
    }

    public void OnTextChangingComplete()
    {
        string text = _inputField.text;
        if (text == "")
        {
            _inputField.text = defaultCount.ToString();
            return;
        }

        int value = Int32.Parse(text);
        if (value > maxCount)
        {
            _inputField.text = 10.ToString();
        }
        else if (value <= 0)
        {
            _inputField.text = defaultCount.ToString();
        }
    }
}