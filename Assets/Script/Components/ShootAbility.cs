
using System;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] private Transform _FirePoint;
    [SerializeField] private float _BulletSpeed = 10;
    public GameObject Bullet;
    public float ShootDelay;

    private float _shootTime = float.MinValue;

    public PlayerStats Stats;
    
    private void Start()
    {
        Stats = new PlayerStats();
        var jsonString = PlayerPrefs.GetString("Stats");
        if (jsonString.Equals(String.Empty, StringComparison.Ordinal))
        {
            Stats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }
        else
        {
            Stats = new PlayerStats();
        }
    }
    
    
    public void Execute()
    {
        if(Time.time < _shootTime + ShootDelay) return;
        
        _shootTime = Time.time;
        
        if (Bullet != null)
        {
            var newBullet = Instantiate(Bullet, _FirePoint.position, _FirePoint.rotation);
            
            Rigidbody rb =  newBullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = _FirePoint.forward * _BulletSpeed;
            }
            Stats.ShotCount++;
            
        }
        else
        {
            Debug.LogError("[SHOOT ABILITY] bullet prefab is not assigned.");
        }
    }
}
