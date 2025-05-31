using System.Collections;
using System.Collections.Generic;
using Script.Components.Interface;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Mathematics;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{
    public Collider Collider;
    public List<MonoBehaviour> CollisionAction = new List<MonoBehaviour>();
    public List<IAbilityTarget> AbilityAction = new List<IAbilityTarget>();
    [HideInInspector] public List<Collider> collisions;


    private void Start()
    {
        foreach (var action in CollisionAction)
        {
            if (action is IAbilityTarget ability)
            {
                AbilityAction.Add(ability);
            }
            else
            {
                Debug.LogError("Collision ability doesn't implement IAbility");
            }
        }
    }

    public void Execute()
    {
        foreach (var action in AbilityAction)
        {
            action.Targets = new List<GameObject>();
                collisions.ForEach(c =>
                {
                    if (c != null) action.Targets.Add(c.gameObject);
                });
               action.Execute();
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;

        switch (Collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Shpere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    isInitialTakeOff = true
                });
                break;

            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    isInitialTakeOff = true
                });
                break;

            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    isInitialTakeOff = true
                });
                break;
        }
        
        Collider.enabled = false;

    }

    
}

public struct ActorColliderData : IComponentData
    {
        public ColliderType ColliderType;
        
        public float3 SphereCenter;
        public float SphereRadius;
        public float3 CapsuleStart;
        public float3 CapsuleEnd;
        public float CapsuleRadius;
        public float3 BoxCenter;
        public float3 BoxHalfExtents;
        public quaternion BoxOrientation;
        
        public bool isInitialTakeOff;
    }

    public enum ColliderType
    {
        Shpere = 0,
        Capsule = 1,
        Box = 2
    }

