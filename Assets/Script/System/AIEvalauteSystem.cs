using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class AIEvalauteSystem : ComponentSystem
{
    private EntityQuery _evaluateQuery;

    protected override void OnCreate()
    {
        _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_evaluateQuery).ForEach((Entity entity,BehaviourManager manager) =>
        {
            IBehaviour bestBeahaviour;
            float highScore = float.MinValue;

            var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            manager.activeBehavior = null;

            foreach (var behaviour in manager.Behaviours)
            {
                if (behaviour is IBehaviour ai)
                {
                    var currentScore = ai.Evalaute();

                    if (currentScore > highScore)
                    {
                        highScore = currentScore;
                        manager.activeBehavior = ai;
                    }
                }
            }
            Debug.Log(manager.activeBehavior);
        });
    }
}
