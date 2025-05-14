
using UnityEngine;

public class DashAbility : MonoBehaviour, IDash
{
    
    public float DashDelay;
    private float _dashTime = float.MinValue;
    
    public void DashExecute()
    {
        if (Time.time < _dashTime + DashDelay) return;
        _dashTime = Time.time;
        var pos = transform.position;
        pos += transform.forward * 5f; 
        transform.position = pos;
    }
}
