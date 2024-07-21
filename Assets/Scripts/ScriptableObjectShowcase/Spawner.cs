using UnityEngine;

namespace ScriptableObjectShowcase
{
    public class Spawner : MonoBehaviour
    {
        public GameObject EntityToSpawn;

        public SpawnManagerScriptableObject spawnManagerData;

        private int _instanceNnumber;

        void Start()
        {
            SpawnEntities();
        }

        private void SpawnEntities()
        {
            int currentSpawnPointIndex = 0;

            for (int i = 0; i < spawnManagerData.NumberOfPrefabsToCreate; i++)
            {
                GameObject currentEntity =
                    Instantiate(
                        EntityToSpawn,
                        spawnManagerData.SpawnPoints[currentSpawnPointIndex],
                        Quaternion.identity);

                currentEntity.name = 
                    spawnManagerData.PrefabName + 
                    _instanceNnumber;

                currentSpawnPointIndex = 
                    (currentSpawnPointIndex + 1) %
                    spawnManagerData.SpawnPoints.Length;

                _instanceNnumber++;
            }
        }
    }
}
