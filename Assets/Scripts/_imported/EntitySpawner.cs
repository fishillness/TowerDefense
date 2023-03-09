using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Spawner operation mode: at startup or periodically.
        /// Режим работы спавнера: при старте или переодически.
        /// </summary>
        public enum SpawnMode
        {
            Start,
            Loop
        }

        /// <summary>
        /// An array of prefabs that the spawner can spawn.
        /// Mассив префабов, которые может заспавнить спаунер.
        /// </summary>
        [SerializeField] private Entity[] m_EntityPrefabs;
        /// <summary>
        /// Area to spawn into.
        /// Зона, в котороый можно спавнить.
        /// </summary>
        [SerializeField] private CircleArea m_Area;
        /// <summary>
        /// Link to SpawnMode
        /// Ссылка на SpawnMode
        /// </summary>
        [SerializeField] private SpawnMode m_SpawnMode;
        /// <summary>
        /// Number of spawns.
        /// Количество спавнов.
        /// </summary>
        [SerializeField] private int m_NumSpawns;
        /// <summary>
        /// Timer update rate.
        /// Частота обновления таймера.
        /// </summary>
        [SerializeField] private float m_RespawnTime;
        private float m_Timer;
        #endregion

        #region Unity Events
        private void Start()
        {
            SpawnEntities(); 
            m_Timer = m_RespawnTime;
        }

        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                int index = Random.Range(0, m_EntityPrefabs.Length);

                GameObject e = Instantiate(m_EntityPrefabs[index].gameObject);
                e.transform.position = m_Area.GetRandomInsideZone();
            }
        }

        #endregion

    }
}

