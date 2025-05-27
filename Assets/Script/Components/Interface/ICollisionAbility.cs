using UnityEngine;
using System.Collections.Generic;

    public interface ICollisionAbility : IAbility
    {
    List<Collider> Collisions { get; set; }
    }
