using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;
using UnityScript.Steps;

namespace ECSExample
{
    // Thanks to trTribe#5732 (from Infallible Code Discord server)
    // for figuring out a way to optimize this #_#
    public class UnitComponentSystem : JobComponentSystem
    {
        struct MoveToPosition : IJobProcessComponentData<Position, Unit>
        {
            public float DeltaTime;

            public void Execute(ref Position UnitPosition, [ReadOnly] ref Unit UnitData)
            {
                UnitPosition.Value = Vector3.MoveTowards(UnitPosition.Value, UnitData.NextPosition, UnitData.Speed * DeltaTime);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new MoveToPosition() {DeltaTime = Time.deltaTime};
            return job.Schedule(this, 100, inputDeps);
        }
    }
}