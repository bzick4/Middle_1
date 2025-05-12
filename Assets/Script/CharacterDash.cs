using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;


public class CharacterDash : ComponentSystem
{
    private EntityQuery _dashQuery;

    protected override void OnCreate()
    {
        _dashQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<DashData>(),
            ComponentType.ReadOnly<UserInputData>(),
            ComponentType.ReadOnly<Translation>());
    }

    protected override void OnUpdate()
    {
                Entities.With(_dashQuery).ForEach((ref UserInputData inputData, ref InputData input, ref Translation translation) =>
                {
                if (input.Dash > 0f && input.Move.x != 0f && input.Move.y != 0f && 
                    inputData.DashAction != null && inputData.DashAction is IDash dash)
                {
                    dash.DashExecute();
                    
                    var pos = translation.Value;
                    pos += new float3(input.Move.x * 2, 0, input.Move.y * 2);
                    translation.Value = pos;
                }
                
                });
            
        }
    
}

// input.Move.x != 0f && input.Move.y != 0f &&