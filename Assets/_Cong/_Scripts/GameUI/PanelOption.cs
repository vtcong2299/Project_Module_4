using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : MonoBehaviour, IOnGameStart<IGameData>, IOnGameStart<IDataManipulator>
{
    
    [SerializeField] Button backButton;
    [SerializeField] Button musicButton;
    [SerializeField] Button vfxButton;
    [SerializeField] GameObject black1;
    [SerializeField] GameObject black2;

    IGameData gameData;
    IDataManipulator dataManipulator;

    Action<IGameData> IOnGameStart<IGameData>.onGameStartAction => data => gameData = data;

    Action<IDataManipulator> IOnGameStart<IDataManipulator>.onGameStartAction => manipulator => dataManipulator = manipulator;

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
        dataManipulator.SaveDataGame();
    }
    void ClickMusicButton()
    {
        AudioManager.Instance.SoundClickButton();
        if (!gameData.data.hasBGM)
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
        if (!gameData.data.hasSFX)
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
        gameData.data.hasBGM = true;
        AudioManager.Instance.SetVolumAudioBGM(true);
        black1.SetActive(false);
    }
    public void OffBGM()
    {
        gameData.data.hasBGM = false;
        AudioManager.Instance.SetVolumAudioBGM(false);
        black1.SetActive(true);
    }
    public void OnSFX()
    {
        gameData.data.hasSFX = true;
        AudioManager.Instance.SetVolumAudioSFX(true);
        black2.SetActive(false);
    }
    public void OffSFX()
    {
        gameData.data.hasSFX = false;
        AudioManager.Instance.SetVolumAudioSFX(false);
        black2.SetActive(true);
    }
}
