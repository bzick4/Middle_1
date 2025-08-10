using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float speed;
    
    public MonoBehaviour ShootAction;
    public MonoBehaviour DashAction;
    public MonoBehaviour InvisAction;

    public float DashDistance = 5f;
    
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
        
        if (InvisAction != null && InvisAction is IAbility)
        {
            dstManager.AddComponentData(entity, new InvisibleData());

        }
        
        
        dstManager.AddComponentObject(entity, this);
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public float Shoot;
    public float Dash;
    public float Invis;

    
}
public struct DashData : IComponentData
{
}

public struct MoveData : IComponentData
{
    public float Speed;
}

public struct ShootData : IComponentData
{

}

public struct BulletMoveData : IComponentData
{
    public float3 Direction;
    public float Speed;
    public bool Active;
}

public struct InvisibleData : IComponentData
{

}
