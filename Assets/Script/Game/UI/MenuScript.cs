using DG.Tweening;
using UnityEngine;

public enum MenuType { MainMenu, GameModeMenu, SettingsMenu, ResultMenu}

public class MenuScript : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] private GameObject background;

    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject gamemodePanel;
    [SerializeField] private GameObject optionsPanel;

    private CanvasGroup groupBackground { get { return background.GetComponent<CanvasGroup>(); } }

    public void OpenMainPanel()
    {
        bool isActive = mainPanel.activeInHierarchy;

        gamemodePanel.SetActive(isActive);
        optionsPanel.SetActive(isActive);

        if (groupBackground.alpha == 0)
        {
            groupBackground.blocksRaycasts = true;
            FadePanel(groupBackground, 0, 1, .5f);
        }

        mainPanel.SetActive(true);
    }

    public void OpenGameModePanel()
    {
        bool isActive = gamemodePanel.activeInHierarchy;

        mainPanel.SetActive(isActive);
        optionsPanel.SetActive(isActive);

        if (groupBackground.alpha == 0)
        {
            groupBackground.blocksRaycasts = true;
            FadePanel(groupBackground, 0, 1, .5f);
        }

        gamemodePanel.SetActive(true);
    }

    public void OpenOptionsPanel()
    {
        bool isActive = optionsPanel.activeInHierarchy;

        mainPanel.SetActive(isActive);
        gamemodePanel.SetActive(isActive);

        groupBackground.blocksRaycasts = true;
        FadePanel(groupBackground, 0, 1, .5f);

        optionsPanel.SetActive(true);
    }

    private void FadePanel(CanvasGroup canvas, float alphaStart, float alphaEnd, float time)
    {
        DOVirtual.Float(alphaStart, alphaEnd, time, x => canvas.alpha = x);
    }

    public void StartGame()
    {
        mainPanel.SetActive(false);
        gamemodePanel.SetActive(false);
        optionsPanel.SetActive(true);

        groupBackground.blocksRaycasts = false;
        FadePanel(groupBackground, 1, 0, .5f);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
