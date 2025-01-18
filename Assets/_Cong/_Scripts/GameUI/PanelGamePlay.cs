using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGamePlay : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] Text txtLevel;
    [SerializeField] Slider exp;

    private void Awake()
    {
        pauseButton.onClick.AddListener(ClickPauseButton);
    }
    private void Update()
    {
        ChangeLevel();
        ChangeSliderValue();
    }
    void ClickPauseButton()
    {
        AudioManager.Instance.SoundClickButton();
        UIManager.Instance.OnEnablePanelPauseGame();
    }
    public void ChangeLevel()
    {
        txtLevel.text =""+ DataPlayer.Instance.curentLevel;
    }
    public void ChangeSliderValue()
    {
        this.exp.maxValue = DataPlayer.Instance.expMax;
        this.exp.value = DataPlayer.Instance.exp;
    }
}
