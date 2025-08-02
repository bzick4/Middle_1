using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DummyHealth : IHealthConfig
{
    public float HeroHealth => 300f;
}

public interface IHealthConfig
{
    float HeroHealth { get; }
}

