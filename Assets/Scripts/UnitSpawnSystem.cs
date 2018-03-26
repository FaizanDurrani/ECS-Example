using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;

namespace ECSExample
{
    [AlwaysUpdateSystem]
    public class UnitSpawnSystem : ComponentSystem
    {
        public static void Initialize(EntityManager entityManager)
        {
            var spawnDataArchetype = entityManager.CreateArchetype(typeof(UnitSpawnData));
            var spawnData = entityManager.CreateEntity(spawnDataArchetype);
            entityManager.SetComponentData(spawnData, new UnitSpawnData {SpawnCount = GameBoostrap.GameSettings.SpawnCount});
        }

        protected override void OnUpdate()
        {
        }
    }
}