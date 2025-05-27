using Unity.Entities;
using UnityEngine;

public class MoveComponent : ComponentSystem
{
    private EntityQuery _query;
    
    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<DevilMove>());
    }
    
    protected override void OnUpdate()
    {
        
        Entities.With(_query).ForEach((Entity entity, Transform transform, DevilMove devilMove) => 
        {
            var p = transform.position;
            p.y += devilMove.Speed;
            transform.position = p;

        });
    }
    
    
}
