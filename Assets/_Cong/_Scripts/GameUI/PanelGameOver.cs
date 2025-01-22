using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameOver : MonoBehaviour
{
    [SerializeField] Button replayButton;
    [SerializeField] Button homeButton;

    private void Awake()
    {
        replayButton.onClick.AddListener(ClickReplayButton);
        homeButton.onClick.AddListener(ClickHomeButton);
    }
    void ClickReplayButton()
    {
        AudioManager.Instance.SoundClickButton();
        UIManager.Instance.LoadScene("MainGame");
    }
    void ClickHomeButton()
    {
        AudioManager.Instance.SoundClickButton();
        UIManager.Instance.LoadScene("MenuScene");
    }
}
