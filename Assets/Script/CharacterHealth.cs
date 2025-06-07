using UnityEngine;


public class CharacterHealth : MonoBehaviour
{
    public Settings Settings;

    private int _health = int.MaxValue;
    //public ShootAbility ShootAbility;
    public PlayerStats HealthStats;

    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, 100);
            WriteStatistics();
            if (_health <= 0) Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        //Debug.Log(Health);
    }

    private void WriteStatistics()
    {
        Settings.HeroHealth = _health;
        HealthStats.Save();
        // var jsonString  = JsonUtility.ToJson(HealthStats);
        // Debug.Log(jsonString);
        // PlayerPrefs.SetString("Stats", jsonString);
        //
        // string folderID = "1pkQZ0j2kVGNOZU12Hx-uVoD8cMllX2WT";
        //
        // GoogleDriveTool.Upload(jsonString, folderID, OnDone);

    }
    
    private void Awake()
    {
        _health = Settings.HeroHealth;
    }

    // private void OnDone()
    // {
    //     throw new System.NotImplementedException();
    // }



}
