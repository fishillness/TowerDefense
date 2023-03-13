using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Enemy m_EnemyPrefabs;
        [SerializeField] private Path m_path;
        [SerializeField] private EnemyAsset[] m_EnemySettings;

        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(m_EnemyPrefabs);
            e.Use(m_EnemySettings[Random.Range(0, m_EnemySettings.Length)]);
            e.GetComponent<TDPatrolController>().SetPath(m_path);

            return e.gameObject;            
        }
    }
}

