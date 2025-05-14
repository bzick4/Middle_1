using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CharacterMove : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<MoveData>(),
            ComponentType.ReadOnly<Translation>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach((Transform transform, ref InputData inputData, ref MoveData move) =>
        {
            var direction = new Vector3(inputData.Move.x, 0, inputData.Move.y);

            if (direction != Vector3.zero)
            {
                // Обновляем позицию
                var pos = transform.position;
                pos += direction * move.Speed;
                transform.position = pos;

                // Обновляем поворот
                transform.rotation = Quaternion.LookRotation(direction);
            }     
        });
    }
}
