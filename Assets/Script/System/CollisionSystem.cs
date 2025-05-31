using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery _collisionQuery;

    private Collider[] _result = new Collider[50];
    

    protected override void OnCreate()
    {
        _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(), 
            ComponentType.ReadOnly<Translation>());
    }

    protected override void OnUpdate()
    {
        var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        
        Entities.With(_collisionQuery).ForEach((Entity entity, CollisionAbility ability,
            ref ActorColliderData colliderData) =>
        {
            
            if(ability ==null || ability.gameObject == null)
            { 
                dstManager.DestroyEntity(entity);
                return;
            }
            
            var go = ability.gameObject;
            float3 position = go.transform.position;
            Quaternion rotation = go.transform.rotation;
            
            ability.collisions?.Clear();

            int size = 0;

            switch (colliderData.ColliderType)
            {
                case ColliderType.Shpere :
                    size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position,
                        colliderData.SphereRadius, _result);
                    break;
                
                case ColliderType.Capsule :
                    var center = ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                    var point1 = colliderData.CapsuleStart + position;
                    var point2 = colliderData.CapsuleEnd + position;
                    
                    point1 = (float3) (rotation * (point1 - center)) + center;
                    point2 = (float3) (rotation * (point2 - center)) + center;
                    size = Physics.OverlapCapsuleNonAlloc(point1, point2, colliderData.CapsuleRadius, _result);
                    break;
                
                case ColliderType.Box :
                    size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtents, _result, colliderData.BoxOrientation * rotation);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (size > 0)
            {
                foreach (var result in _result)
                {
                    ability.collisions?.Add(result);
                }
                
                ability.Execute();
            }
        });
        
    }
}
