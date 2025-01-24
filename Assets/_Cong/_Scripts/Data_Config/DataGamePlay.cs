using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.ComponentModel;

[Serializable]
public class GameData
{
    public double highestWave;
    public bool hasBGM;
    public bool hasSFX;
    public float damageDefault = 50;
    public float hpDefault = 500;
    public float attackCountdownDefault = 4;
    public float moveSpeedDefault = 3.5f;
    //public float armorDefault = 20;
    public float lifeStealPercentDefault = 0;

}

public class DataGamePlay : Singleton<DataGamePlay>, IGameData, IDataManipulator
{
    private string dataPath;

    GameData gameData;

    public GameData data => gameData;

    public void StartDataGamePlay()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
        gameData = LoadData(); //load khi khởi động
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(gameData); //chuyển data thành chuỗi json
            File.WriteAllText(dataPath, json); //tạo file nếu chưa tồn tại
    }

    GameData LoadData()
    {
        if (File.Exists(dataPath))
        {
                string json = File.ReadAllText(dataPath);
                GameData data = JsonUtility.FromJson<GameData>(json);
                return data;
        }
        return new GameData(); // Trả về dữ liệu mới nếu không tìm thấy file
    }    
}
