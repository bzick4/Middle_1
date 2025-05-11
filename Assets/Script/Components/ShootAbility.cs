
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject Bullet;
    public float ShootDelay;

    private float _shootTime = float.MinValue;
    
    public void Execute()
    {
        if(Time.time < _shootTime + ShootDelay) return;
        
        _shootTime = Time.time;
        
        if (Bullet != null)
        {
            var t = transform;
            var newBullet = Instantiate(Bullet, t.position, t.rotation);
        }
        else
        {
            Debug.LogError("[SHOOT ABILITY] bullet prefab is not assigned.");
        }
    }
}
