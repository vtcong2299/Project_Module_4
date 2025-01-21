using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public PanelOption panelOption;
    public PanelGamePlay panelGP;
    [SerializeField] GameObject panelGamePlay;
    [SerializeField] GameObject panelPauseGame;
    [SerializeField] GameObject panelQuitGame;
    [SerializeField] GameObject panelPowerUp;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelOptions;
    [SerializeField] CanvasGroup canvasGroupGamePlay;
    [SerializeField] CanvasGroup canvasGroupPauseGame;
    [SerializeField] CanvasGroup canvasGroupQuitGame;
    [SerializeField] CanvasGroup canvasGroupPowerUp;
    [SerializeField] CanvasGroup canvasGroupGameOver;
    [SerializeField] CanvasGroup canvasGroupOptions;
    [SerializeField] UnityEngine.Transform selectedBuff;
    [SerializeField] List<Sprite> buffList = new List<Sprite>();
    [SerializeField] GameObject imageBuff;
    public bool hasPanelBuff;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnEnablePanelGameOver();
        }
    }
    public void OnEnablePanelGamePlay()
    {
        Show(panelGamePlay, canvasGroupGamePlay, false);
    }
    public void OnDisablePanelGamePlay()
    {
        Hide(panelGamePlay, canvasGroupGamePlay, false);
    }
    public void OnEnablePanelPauseGame()
    {
        Show(panelPauseGame, canvasGroupPauseGame, true);
    }
    public void OnDisablePanelPauseGame()
    {
        Hide(panelPauseGame, canvasGroupPauseGame, true);
    }
    public void OnEnablePanelQuitGame()
    {
        Show(panelQuitGame, canvasGroupQuitGame, false);
    }
    public void OnDisablePanelQuitGame()
    {
        Hide(panelQuitGame, canvasGroupQuitGame, false);
    }
    public void OnEnablePanelPowerUp()
    {
        Show(panelPowerUp, canvasGroupPowerUp, true);
        hasPanelBuff =true;        
    }
    public void OnDisablePanelPowerUp()
    {
        Hide(panelPowerUp, canvasGroupPowerUp, true);
        hasPanelBuff = false;        
    }
    public void OnEnablePanelGameOver()
    {
        Show(panelGameOver, canvasGroupGameOver, true);
    }
    public void OnDisablePanelGameOver()
    {
        Hide(panelGameOver, canvasGroupGameOver, true);
    }
    public void OnEnablePanelOptions()
    {
        Show(panelOptions, canvasGroupOptions, true);
    }
    public void OnDisablePanelOptions()
    {
        Hide(panelOptions, canvasGroupOptions, true);
    }
    public void AddToBuffList(ConfigPowerUp buff)
    {      
        buffList.Add(buff.iconBuff);
        AddBuffToPausePanel();       
    }
    public void AddBuffToPausePanel()
    {
        foreach (UnityEngine.Transform buff in selectedBuff)
        {
            Destroy(buff.gameObject);
        }
        foreach (Sprite buff in buffList)
        {
            GameObject obj = Instantiate(imageBuff, selectedBuff);
            var iconBuff = obj.transform.GetChild(0).GetComponent<Image>(); 
            iconBuff.sprite = buff;
        }
    }
    public void ClearBuffList()
    {
        buffList.Clear();
    }
    public void Show(GameObject panel, CanvasGroup canvasGroup, bool pause)
    {
        canvasGroup.alpha= 0;
        panel.SetActive(true);
        canvasGroup.DOFade(1, 0.5f).SetUpdate(UpdateType.Normal, true);
        if (pause)
            {
                GameManager.Instance.SetGameState(GameState.Pause);
            }        
    }
    public void Hide(GameObject panel, CanvasGroup canvasGroup, bool resume)
    {
        canvasGroup.DOFade(0, 0.3f)
            .SetUpdate(UpdateType.Normal, true)
            .OnComplete(() => DisablePanel(panel));
        if (resume)
        {
            GameManager.Instance.SetGameState(GameState.Running);
        }
    }
    void DisablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
