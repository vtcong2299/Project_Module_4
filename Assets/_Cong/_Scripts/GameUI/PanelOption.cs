using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : MonoBehaviour
{
    
    [SerializeField] Button backButton;
    [SerializeField] Button musicButton;
    [SerializeField] Button vfxButton;
    [SerializeField] GameObject black1;
    [SerializeField] GameObject black2;

    private void Awake()
    {
        backButton.onClick.AddListener(ClickBackButton);
        musicButton.onClick.AddListener(ClickMusicButton);
        vfxButton.onClick.AddListener(ClickSFXButton);
    }
    void ClickBackButton()
    {
        AudioManager.Instance.SoundClickButton();
        UIManager.Instance.OnEnablePanelPauseGame();
        UIManager.Instance.OnDisablePanelOptions();
        GameManager.Instance.SaveDataGame();
    }
    void ClickMusicButton()
    {
        AudioManager.Instance.SoundClickButton();
        if (!GameManager.Instance.gameData.hasBGM)
        {
            OnBGM();
        }
        else
        {
            OffBGM();
        }
    }
    void ClickSFXButton()
    {
        AudioManager.Instance.SoundClickButton();
        if (!GameManager.Instance.gameData.hasSFX)
        {
            OnSFX();
        }
        else
        {
            OffSFX();
        }
    }
    public void OnBGM()
    {
        GameManager.Instance.gameData.hasBGM = true;
        AudioManager.Instance.SetVolumAudioBGM(true);
        black1.SetActive(false);
    }
    public void OffBGM()
    {
        GameManager.Instance.gameData.hasBGM = false;
        AudioManager.Instance.SetVolumAudioBGM(false);
        black1.SetActive(true);
    }
    public void OnSFX()
    {
        GameManager.Instance.gameData.hasSFX = true;
        AudioManager.Instance.SetVolumAudioSFX(true);
        black2.SetActive(false);
    }
    public void OffSFX()
    {
        GameManager.Instance.gameData.hasSFX = false;
        AudioManager.Instance.SetVolumAudioSFX(false);
        black2.SetActive(true);
    }
}
