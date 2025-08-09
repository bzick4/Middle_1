
using System;
using Unity.Entities;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility //IConvertGameObjectToEntity
{
    [SerializeField] private Transform _FirePoint;
    [SerializeField] private float _BulletSpeed = 10;
    public GameObject Bullet;
   
    public float ShootDelay;

    private float _shootTime = float.MinValue;

    public Entity bulletPrefabEntity;
    public EntityManager entityManager;

    public PlayerStats Stats;

    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;  
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

        if (bulletPrefabEntity != Entity.Null)
        {
            var newBullet = entityManager.Instantiate(bulletPrefabEntity);
            entityManager.SetComponentData(newBullet, new Unity.Transforms.Translation { Value = transform.position });
            
        }
    
        
        if (Time.time < _shootTime + ShootDelay) return;
        _shootTime = Time.time;

        if (Bullet != null)
        {
            var newBullet = Instantiate(Bullet, _FirePoint.position, _FirePoint.rotation);

            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
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

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        bulletPrefabEntity = conversionSystem.GetPrimaryEntity(Bullet);
        dstManager.AddComponentObject(entity, this);
        Debug.Log("Bullet: " + Bullet);
        Debug.Log("bulletPrefabEntity: " + bulletPrefabEntity);
        
    }
}
