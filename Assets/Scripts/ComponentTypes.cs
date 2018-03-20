using System;
using System.ComponentModel;
using Unity.Entities;
using Unity.Mathematics;

namespace ECSExample
{
    [Serializable]
    public struct Unit : IComponentData
    {
        public float Speed;
        public float3 NextPosition;
    }

    [Serializable]
    public struct UnitSpawnData : IComponentData
    {
        public int SpawnCount;
    }

}