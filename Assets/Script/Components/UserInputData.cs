using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float speed;
    
    public MonoBehaviour ShootAction;
    public MonoBehaviour DashAction;
    public float DashDistance = 5f;
    public float LastDashTime = float.MinValue;
    public float DashCooldown = 1f;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData
                {
                    Speed = speed/100
                }
            );
        if (ShootAction != null && ShootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }

        if (DashAction != null && DashAction is IDash)
        {
            dstManager.AddComponentData(entity, new DashData());
        }
        
        dstManager.AddComponentObject(entity, this);
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public float Shoot;
    public float Dash;
    
}
public struct DashData : IComponentData
{
    public float DashDistance;
}

public struct MoveData : IComponentData
{
    public float Speed;
}

public struct ShootData : IComponentData
{
    
}

