using UnityEngine;
using Zenject;

[CreateAssetMenu]
public class Settings : ScriptableObject, IHealthConfig
{
    public float _HeroHealth=100;

    public float HeroHealth => _HeroHealth;
    
}
