public class DataManipulator : IDataManipulator, IGameData
{
    GaneData gameData;
    public DataManipulator()
    {
        DataGamePlay.Instance.StartDataGamePlay();
        LoadDataGame();
    }

    public void AfterInject()
    {
        DataPlayer.Instance.StartPlayerData();
        SetPanelOptions();

    }

    public GaneData data => gameData;

    void LoadDataGame()
    {
        gameData = DataGamePlay.Instance.LoadData();
    }
    public void SaveDataGame()
    {
        DataGamePlay.Instance.SaveData(gameData);
    }
    void SetPanelOptions()
    {
        if (gameData.hasBGM)
        {
            UIManager.Instance.panelOption.OnBGM();
        }
        else
        {
            UIManager.Instance.panelOption.OffBGM();
        }
        if (gameData.hasSFX)
        {
            UIManager.Instance.panelOption.OnSFX();
        }
        else
        {
            UIManager.Instance.panelOption.OffSFX();
        }

    }

}
