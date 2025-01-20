using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.ComponentModel;

[Serializable]
public class GaneData
{
    public double highestWave;
    public bool hasBGM;
    public bool hasSFX;
    public float damageDefault = 100;
    public float hpDefault = 1000;
    public float attackSpeedDefault = 5;
    public float moveSpeedDefault = 10;
    public float armorDefault = 20;
    public float lifeStealPercentDefault = 0;

}

public class DataGamePlay : Singleton<DataGamePlay>, IGameData, IDataManipulator
{
    private string dataPath;

    GaneData gameData;

    public GaneData data => gameData;

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

    GaneData LoadData()
    {
        if (File.Exists(dataPath))
        {
                string json = File.ReadAllText(dataPath);
                GaneData data = JsonUtility.FromJson<GaneData>(json);
                return data;
        }
        return new GaneData(); // Trả về dữ liệu mới nếu không tìm thấy file
    }    
}
