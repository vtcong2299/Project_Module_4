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
        vfxButton.onClick.AddListener(ClickVfxButton);
    }
    void ClickBackButton()
    {
        AudioManager.Instance.SoundClickButton();
        UIManager.Instance.OnEnablePanelPauseGame();
        UIManager.Instance.OnDisablePanelOptions();
    }
    void ClickMusicButton()
    {
        AudioManager.Instance.SoundClickButton();        
        if (black1.activeSelf)
        {
            AudioManager.Instance.SetVolumAudioBGM(true);
            black1.SetActive(false);
        }
        else
        {
            AudioManager.Instance.SetVolumAudioBGM(false);
            black1.SetActive(true);
        }
    }
    void ClickVfxButton()
    {
        AudioManager.Instance.SoundClickButton();
        if (black2.activeSelf)
        {
            AudioManager.Instance.SetVolumAudioSFX(true);
            black2.SetActive(false);
        }
        else
        {
            AudioManager.Instance.SetVolumAudioSFX(false);
            black2.SetActive(true);
        }
    }
}
