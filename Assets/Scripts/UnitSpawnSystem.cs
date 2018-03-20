using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;

namespace ECSExample
{
    public class UnitSpawnSystem : ComponentSystem
    {
        struct UnitSpawnDataGroup
        {
            public int Length;
            public ComponentDataArray<UnitSpawnData> UnitySpawnData;
        }

        [Inject] private UnitSpawnDataGroup _unitSpawnDataGroup;

        public static void Initialize(EntityManager entityManager)
        {
            var spawnDataArchetype = entityManager.CreateArchetype(typeof(UnitSpawnData));
            var spawnData = entityManager.CreateEntity(spawnDataArchetype);
            entityManager.SetComponentData(spawnData, new UnitSpawnData {SpawnCount = GameBoostrap.Settings.SpawnCount});
        }

        protected override void OnUpdate()
        {
            new SpawnUnitJob
            {
                PostUpdateCommands = PostUpdateCommands,
                SpawnCount = _unitSpawnDataGroup.UnitySpawnData[0].SpawnCount
            }.Schedule().Complete();

            base.Enabled = false;
        }
    }

    public struct SpawnUnitJob : IJob
    {
        public EntityCommandBuffer PostUpdateCommands;
        [ReadOnly] public int SpawnCount;

        public void Execute()
        {
            for (int index = 0; index < SpawnCount; index++)
            {
                PostUpdateCommands.CreateEntity(GameBoostrap.UnitArchetype);
                PostUpdateCommands.SetComponent(new Unit {NextPosition = new float3(0, 1, 0), Speed = 2});
                PostUpdateCommands.SetComponent(new Position {Value = new float3(index + 2, 1, 0)});
                PostUpdateCommands.AddSharedComponent(GameBoostrap.UnitMesh);
            }
        }
    }
}