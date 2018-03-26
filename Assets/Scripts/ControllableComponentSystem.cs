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
    public class ControllableComponentSystem : JobComponentSystem
    {
        struct MoveToPosition : IJobProcessComponentData<Position, Controllable, Unit>
        {
            public float DeltaTime;

            public void Execute(ref Position UnitPosition, [ReadOnly] ref Controllable controllable, [ReadOnly] ref Unit UnitData)
            {
                UnitPosition.Value = Vector3.MoveTowards(UnitPosition.Value, controllable.NextPosition, controllable.Speed * DeltaTime);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var commands = new NativeArray<RaycastCommand>(1, Allocator.Temp);
            var results = new NativeArray<RaycastHit>(1, Allocator.Temp);

            var mainCamera = Camera.main;
            var mouseWorldPosition = mainCamera.ScreenPointToRay(Input.mousePosition);
            var cameraDirection = mainCamera.transform.forward;
            
            Debug.Log(mouseWorldPosition.origin);
            commands[0] = new RaycastCommand(mouseWorldPosition.origin, mouseWorldPosition.direction);
            var raycastJobHandle = RaycastCommand.ScheduleBatch(commands, results, 1);
            
            
            raycastJobHandle.Complete();
            if (results[0].collider != null)
                Debug.Log(results[0].transform.name);
            
            commands.Dispose();
            results.Dispose();
            
            var moveJob = new MoveToPosition()
            {
                DeltaTime = Time.deltaTime
            };
            return moveJob.Schedule(this, 100, inputDeps);
        }
    }
}