
using System;
using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility //IConvertGameObjectToEntity
{
    [SerializeField] private Transform _FirePoint;
    [SerializeField] private float _BulletSpeed = 10;
    [SerializeField] private ParticleSystem _ParticleSystem;
   //public GameObject Bullet;
    private PoolObject _BulletPool => FindObjectOfType<PoolObject>();

    public float ShootDelay;

    private float _shootTime = float.MinValue;

    // public Entity bulletPrefabEntity;
    // public EntityManager entityManager;
    // private PoolBullet _BulletPool => FindObjectOfType<PoolBullet>();
    public PlayerStats Stats;

    private void Awake()
    {
        //entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

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
        // if (Time.time < _shootTime + ShootDelay) return;

        // _shootTime = Time.time;

        // if (Bullet != null)
        // {
        //     var newBullet = Instantiate(Bullet, _FirePoint.position, _FirePoint.rotation);

        //     Rigidbody rb =  newBullet.GetComponent<Rigidbody>();
        //     if (rb != null)
        //     {
        //         rb.velocity = _FirePoint.forward * _BulletSpeed;
        //     }
        //     Stats.ShotCount++;

        // }
        // else
        // {
        //     Debug.LogError("[SHOOT ABILITY] bullet prefab is not assigned.");
        // }

        // if (bulletPrefabEntity != Entity.Null)
        // {
        //     var newBullet = entityManager.Instantiate(bulletPrefabEntity);
        //     entityManager.SetComponentData(newBullet, new Unity.Transforms.Translation { Value = transform.position });

        // }



        if (Time.time < _shootTime + ShootDelay) return;
        _shootTime = Time.time;

        _ParticleSystem.Play();

        GameObject bullet = _BulletPool.GetObject();

        bullet.transform.position = _FirePoint.position;
        bullet.transform.rotation = _FirePoint.rotation;


        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = _FirePoint.up * _BulletSpeed;
        }

        StartCoroutine(ReturnBulletToPool(bullet, 2f));

    }
    
    private IEnumerator ReturnBulletToPool(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(0.9f);
        _BulletPool.ReturnObject(bullet);
    }
}
