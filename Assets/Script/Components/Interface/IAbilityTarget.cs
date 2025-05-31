using UnityEngine;
using System.Collections.Generic;

namespace Script.Components.Interface
{
    public interface IAbilityTarget : IAbility
    {
        List<GameObject> Targets { get; set; }
    }
}