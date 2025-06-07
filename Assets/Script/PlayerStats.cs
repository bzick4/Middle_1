using System.IO;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public int ShotCount;
    public Settings Setting;
    
    
    private string file;


private void Update()
    {
        Press();
    }

    private void Awake()
    {
        file = Application.dataPath + "/GameHeath.json";
    }

    public void Save()
    {
        PlayerStatsData save = new PlayerStatsData
        {
            Health = Setting.HeroHealth
        };
        string json = JsonUtility.ToJson(save);

        File.WriteAllText(file, json);
       GoogleDriveTool.Upload(json, "1pkQZ0j2kVGNOZU12Hx-uVoD8cMllX2WT", OnDone);
    }


    public void Load()
    {
        string json = File.ReadAllText(file);

        PlayerStatsData data = JsonUtility.FromJson<PlayerStatsData>(json);
       
        Setting.HeroHealth = data.Health;

        Debug.Log("Load: " + data.Health);
    }



    private void Press()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
            Debug.Log("Load" + Setting.HeroHealth + file);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Save();
            Debug.Log("Save" + Setting.HeroHealth + file);
        }
    }

    private void OnDone()
    {
        //throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class PlayerStatsData
{
    public float Health;
}
