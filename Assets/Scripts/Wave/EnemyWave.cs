using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave : MonoBehaviour
    {
        [Serializable] private class Squad
        {
            public EnemyAsset m_asset;
            public int m_count;
        }
        [Serializable] private class PathGroup
        {
            public Squad[] m_squads;

        }
        [SerializeField] private PathGroup[] m_groups;
        [SerializeField] private float m_prepareTime = 10f;
        [SerializeField] private EnemyWave m_nextWave;
        private event Action OnWaveReady;

        #region Unity Events
        private void Awake()
        {
            enabled = false;
        }
        private void Update()
        {
            if (Time.time >= m_prepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }    
        }
        #endregion

        #region Public API
        public void Prepare(Action spawnEnemies)
        {
            m_prepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < m_groups.Length; i++)
            {
                foreach (var squad in m_groups[i].m_squads)
                {
                    yield return (squad.m_asset, squad.m_count, i);
                }                
            }
        }
        
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if (m_nextWave)
                m_nextWave.Prepare(spawnEnemies);
            return m_nextWave;
        }
        #endregion
    }
}