using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGamePlay : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] Text txtLevel;
    [SerializeField] Text txtWave;
    [SerializeField] Slider exp;
    [SerializeField] Image attackCountdown;
    private void Awake()
    {
        pauseButton.onClick.AddListener(ClickPauseButton);
    }
    private void Update()
    {
        ChangeLevel();
        ChangeSliderValue();
        ChangeWave();
        FillImageCountdownAttack();
    }
    void ClickPauseButton()
    {
        AudioManager.Instance.SoundClickButton();
        UIManager.Instance.OnEnablePanelPauseGame();
    }
    void ChangeLevel()
    {
        txtLevel.text =""+ DataPlayer.Instance.curentLevel;
    }
    void ChangeWave()
    {
        txtWave.text ="Wave - "+ EnemyManager.Instance.currentWave;
    }
    void ChangeSliderValue()
    {
        this.exp.maxValue = DataPlayer.Instance.expMax;
        this.exp.value = DataPlayer.Instance.exp;
    }
    void FillImageCountdownAttack()
    {
        if( PlayerCtrl.Instance.playerAttack.canAttack)
        {
            attackCountdown.fillAmount = 1;
            attackCountdown.DOColor(Color.green, 0);
        }
        else
        {
            attackCountdown.fillAmount = 0;
            attackCountdown.DOColor(Color.red, 0);
            attackCountdown.fillAmount = PlayerCtrl.Instance.playerAttack.timeElapsed / DataPlayer.Instance.currentAttackCountdown;
        }
        
    }
}
