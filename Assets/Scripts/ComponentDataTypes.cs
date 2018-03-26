using System;
using System.ComponentModel;
using Unity.Entities;
using Unity.Mathematics;

namespace ECSExample
{
    public struct Unit : IComponentData
    {
        public int UnitId;
    }
    
    public struct Selectable : IComponentData
    {
        public bool1 Selected;
    }

    public struct UnitSpawnData : IComponentData
    {
        public int SpawnCount;
    }

    public struct Controllable : IComponentData
    {
        public float Speed;
        public float3 NextPosition;
    }
}