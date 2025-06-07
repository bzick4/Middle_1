using System.IO;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public int ShotCount;
    public Settings setting;
    
    
    private string file;

    private void Awake()
    {
        file = Application.dataPath + "/GameData.json";
    }

    public void Save()
    {
        PlayerStatsData save = new PlayerStatsData
        {
            Health = setting.HeroHealth
        };
        string json = JsonUtility.ToJson(save);

        File.WriteAllText(file, json);
        GoogleDriveTool.Upload(json, "1pkQZ0j2kVGNOZU12Hx-uVoD8cMllX2WT", OnDone);
    }
    
    private void OnDone()
    {
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class PlayerStatsData
{
    public int Health;
}
