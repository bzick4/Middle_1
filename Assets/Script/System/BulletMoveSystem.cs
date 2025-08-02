using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class BulletMoveSystem : ComponentSystem
{
    private EntityQuery _shootQuery;

    protected override void OnCreate()
    {
        _shootQuery = GetEntityQuery(typeof(BulletMoveData), typeof(Translation));
    }

    protected override void OnUpdate()
    {
        var entities = _shootQuery.ToEntityArray(Unity.Collections.Allocator.TempJob);
        var translations = _shootQuery.ToComponentDataArray<Translation>(Unity.Collections.Allocator.TempJob);
        var moves = _shootQuery.ToComponentDataArray<BulletMoveData>(Unity.Collections.Allocator.TempJob);

        for (int i = 0; i < entities.Length; i++)
        {
            var translation = translations[i];
            var moveData = moves[i];

            translation.Value += moveData.Direction * moveData.Speed * Time.DeltaTime;
            EntityManager.SetComponentData(entities[i], translation);
        }

        entities.Dispose();
        translations.Dispose();
        moves.Dispose();
    }
}