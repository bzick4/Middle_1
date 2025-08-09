using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisAbility: MonoBehaviour, IAbility
{
    public float Delay;

    private float _time = float.MinValue;

    public void Execute()
    {
        if (Time.time < _time + Delay) return;
        _time = Time.time;
        
        Debug.Log("WORK");

    }

}
