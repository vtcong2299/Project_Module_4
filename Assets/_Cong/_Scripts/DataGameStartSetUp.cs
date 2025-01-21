public class DataGameStartSetUp
{
    GameData _gameData;
    public DataGameStartSetUp(IGameData gameData)
    {
        _gameData = gameData.data;
    }

    public void SetUp()
    {
        DataPlayer.Instance.StartPlayerData();
        SetPanelOptions();
    }

    void SetPanelOptions()
    {
        if (_gameData.hasBGM)
        {
            UIManager.Instance.panelOption.OnBGM();
        }
        else
        {
            UIManager.Instance.panelOption.OffBGM();
        }
        if (_gameData.hasSFX)
        {
            UIManager.Instance.panelOption.OnSFX();
        }
        else
        {
            UIManager.Instance.panelOption.OffSFX();
        }

    }

}
