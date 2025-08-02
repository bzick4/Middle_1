
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class EcsAnimation: MonoBehaviour
{
    public Entity entity;
    public EntityManager entityManager;
    private Animator animator => GetComponent<Animator>();
    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        if (entity == Entity.Null)
        {
            entity = entityManager.CreateEntity();
        }
        
        if (!entityManager.HasComponent<InputData>(entity))
        {
            entityManager.AddComponentData(entity, new InputData());
        }
    }

    private void Update()
    {
        if (entityManager.HasComponent<InputData>(entity))
        {
            var input = entityManager.GetComponentData<InputData>(entity);
            float speed = math.length(input.Move);
            animator.SetFloat("Blend", speed);
        }
    }
}
