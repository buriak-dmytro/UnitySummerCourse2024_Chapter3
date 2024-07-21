using UnityEngine;

namespace ScriptableObjectShowcase
{
    [CreateAssetMenu(
        fileName = "Data",
        menuName = "ScriptableObjects/SpawnManagerScriptableObject", 
        order = 1)]
    public class SpawnManagerScriptableObject : ScriptableObject
    {
        public string PrefabName;

        public int NumberOfPrefabsToCreate;
        public Vector3[] SpawnPoints;
    }
}
