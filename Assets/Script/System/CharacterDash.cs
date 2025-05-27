using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;


public class CharacterDash : ComponentSystem
{
    private EntityQuery _dashQuery;

    protected override void OnCreate()
    {
        _dashQuery = GetEntityQuery(
            ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<DashData>(),
            ComponentType.ReadOnly<UserInputData>(),
            ComponentType.ReadOnly<Translation>()
        );
}

    protected override void OnUpdate()
    {
       Entities.With(_dashQuery).ForEach((Entity entity, ref InputData input, UserInputData inputData) =>
    {
        if (input.Dash > 0f && inputData.DashAction != null && inputData.DashAction is IDash dash) 
        {
            dash.DashExecute(); 
        } 
    }); 
    }
    
}
