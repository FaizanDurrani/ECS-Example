using System.ComponentModel.Design;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Networking;

namespace ECSExample
{
    public sealed class GameBoostrap
    {
        public static EntityArchetype UnitArchetype;
        public static MeshInstanceRenderer UnitMesh;
        public static GameSettings Settings;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitBeforeScene()
        {
            var entitityManager = World.Active.GetOrCreateManager<EntityManager>();
            UnitArchetype = entitityManager.CreateArchetype(typeof(Unit), typeof(Position), typeof(TransformMatrix));
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitAfterScene()
        {
            Settings = Object.FindObjectOfType<GameSettings>();
            if (Settings == null)
                return;

            UnitMesh = GetMeshFromPrototype("UnitMeshProto");
            UnitSpawnSystem.Initialize(World.Active.GetOrCreateManager<EntityManager>());
        }

        private static MeshInstanceRenderer GetMeshFromPrototype(string name)
        {
            var proto = GameObject.Find(name);
            var result = proto.GetComponent<MeshInstanceRendererComponent>().Value;
            Object.Destroy(proto);
            return result;
        }
    }
}