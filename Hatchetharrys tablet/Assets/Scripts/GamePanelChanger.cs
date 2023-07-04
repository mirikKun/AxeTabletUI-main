using UnityEngine;

public class GamePanelChanger : MonoBehaviour
{
    [SerializeField] private GameObject gameEnterPanel;

    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject gameChoosePanel;
    [SerializeField] private GameObject gameRulesPanel;
    [SerializeField] private GameObject gamePreparingPanel;
    [SerializeField] private GameObject gameThrowingPanel;
    [SerializeField] private GameObject gameResultsPanel;

    [SerializeField] private GameObject timeLeftPopup;
    [SerializeField] private GameObject registrationPopup;


    private GameObject _lastPanel;

    private void Start()
    {
        _lastPanel = gameEnterPanel;
    }

    public void TurnOffLastPanel(GameObject newLastPanel)
    {
        if (newLastPanel == _lastPanel)
            return;
        _lastPanel.SetActive(false);
        _lastPanel = newLastPanel;
    }

    public void ChangeToEnterPanel()
    {
        TurnOffLastPanel(gameEnterPanel);

        timeLeftPopup.SetActive(false);

        gameEnterPanel.SetActive(true);
    }

    public void ChangeToLoginPanel()
    {
        TurnOffLastPanel(loginPanel);
        loginPanel.SetActive(true);
    }

    public void ChangeToGameRulesPanel()
    {
        TurnOffLastPanel(gameRulesPanel);

        gameRulesPanel.SetActive(true);
    }

    public void ChangeToGameChoosePanel()
    {
        TurnOffLastPanel(gameChoosePanel);

        gameChoosePanel.SetActive(true);
    }

    public void ChangeToGamePreparingPanel()
    {
        TurnOffLastPanel(gamePreparingPanel);

        //timeLeftPopup.SetActive(true);
        gamePreparingPanel.SetActive(true);
    }

    public void ChangeToGameThrowingPanel()
    {
        TurnOffLastPanel(gameThrowingPanel);

        gameThrowingPanel.SetActive(true);
    }

    public void ChangeToGameResultsPanel()
    {
        TurnOffLastPanel(gameResultsPanel);

        gameResultsPanel.SetActive(true);
    }
}