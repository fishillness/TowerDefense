using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static event Action<Enemy> OnEnemySpawn;

        [SerializeField] private Enemy m_EnemyPrefabs;
        [SerializeField] private Path[] m_paths;
        [SerializeField] private EnemyWave m_currentWave;
        private int m_activeEnemyCount = 0; 
        public event Action OnAllWavesDead;
        
        private void Start()
        {
            m_currentWave.Prepare(SpawnEnemies);
        }
        private void RecordEnemyDead()
        {
            if (--m_activeEnemyCount == 0)
            {
                ForceNextWave();
            }
        }
        public void ForceNextWave()
        {
            if (m_currentWave)
            {
                TDPlayer.Instance.ChangeGold((int)m_currentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {
                if (m_activeEnemyCount == 0)
                {
                    Debug.Log("All Waves Dead");
                    OnAllWavesDead?.Invoke();
                }
            } 
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in m_currentWave.EnumerateSquads())
            {
                if (pathIndex < m_paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EnemyPrefabs, 
                            m_paths[pathIndex].StartArea.GetRandomInsideZone(), 
                            Quaternion.identity);
                        e.OnEnd += RecordEnemyDead;
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(m_paths[pathIndex]);
                        m_activeEnemyCount += 1;
                        OnEnemySpawn?.Invoke(e);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                }
            }

            m_currentWave = m_currentWave.PrepareNext(SpawnEnemies);
        }
    }
}

