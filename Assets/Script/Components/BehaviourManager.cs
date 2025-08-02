using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<MonoBehaviour> Behaviours;
    public IBehaviour activeBehavior;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<AIAgent>(entity);
    }
}

public struct AIAgent: IComponentData
{
}