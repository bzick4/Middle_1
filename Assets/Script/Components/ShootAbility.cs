
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] private Transform _FirePoint;
    [SerializeField] private float _BulletSpeed = 10;
    public GameObject Bullet;
    public float ShootDelay;

    private float _shootTime = float.MinValue;
    
    public void Execute()
    {
        if(Time.time < _shootTime + ShootDelay) return;
        
        _shootTime = Time.time;
        
        if (Bullet != null)
        {
            // var t = transform;
            var newBullet = Instantiate(Bullet, _FirePoint.position, _FirePoint.rotation);
            // Bullet.transform.position = _FirePoint.position;
            // Bullet.transform.rotation = _FirePoint.rotation;
            
            Rigidbody rb =  newBullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = _FirePoint.forward * _BulletSpeed;
            }
        }
        else
        {
            Debug.LogError("[SHOOT ABILITY] bullet prefab is not assigned.");
        }
    }
}
