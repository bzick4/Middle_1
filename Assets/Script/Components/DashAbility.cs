
using UnityEngine;

public class DashAbility : MonoBehaviour, IDash
{
    
    public float DashDelay;
    private float _dashTime = float.MinValue;
    
    public void DashExecute()
    {
        if(Time.time < _dashTime + DashDelay) return;
        
        _dashTime = Time.time;
        
        var pos = transform.position;
        pos += new Vector3(pos.x * 2 , 0, pos.y * 2);
        transform.position = pos;
    }
}
