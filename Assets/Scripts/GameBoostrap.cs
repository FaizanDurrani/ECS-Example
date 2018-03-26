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
        
        public static GameSettings GameSettings;
        public static UiReferences UiReferences;
        public static PrefabManager PrefabManager;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitAfterScene()
        {
            GameSettings = Object.FindObjectOfType<GameSettings>();
            PrefabManager = Object.FindObjectOfType<PrefabManager>();
            UiReferences = Object.FindObjectOfType<UiReferences>();

            if (!GameSettings || !UiReferences || !PrefabManager)
            {
                Debug.LogError("Couldn't find either GameSettings, UiRefences or PrefabManager in the scene");
                return;
            }

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