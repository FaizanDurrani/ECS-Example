using UnityEngine;

namespace ECSExample
{
    public class PrefabManager : MonoBehaviour
    {
        public string UnitPrefabName = "Unit";
        private GameObject _unitPrefab;
        public GameObject UnitPrefab
        {
            get
            {
                if (_unitPrefab != null) return _unitPrefab;
                return _unitPrefab = Resources.Load<GameObject>(UnitPrefabName);
            }
        }
    }
}