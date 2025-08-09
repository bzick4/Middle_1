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
            ComponentType.ReadOnly<Translation>(),
            ComponentType.ReadOnly<InvisibleData>()
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
        if (input.Invis > 0f && inputData.InvisAction != null && inputData.InvisAction is IAbility invis)
        {
            invis.Execute();
        }
    }); 
    }
    
}
