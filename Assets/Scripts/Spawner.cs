using UnityEngine;

namespace SpaceShooter
{
    public abstract class Spawner : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Spawner operation mode: at startup or periodically.
        /// ����� ������ ��������: ��� ������ ��� ������������.
        /// </summary>
        public enum SpawnMode
        {
            Start,
            Loop
        }

        protected abstract GameObject GenerateSpawnedEntity();

        /// <summary>
        /// Area to spawn into.
        /// ����, � �������� ����� ��������.
        /// </summary>
        [SerializeField] private CircleArea m_Area;
        /// <summary>
        /// Link to SpawnMode
        /// ������ �� SpawnMode
        /// </summary>
        [SerializeField] private SpawnMode m_SpawnMode;
        /// <summary>
        /// Number of spawns.
        /// ���������� �������.
        /// </summary>
        [SerializeField] private int m_NumSpawns;
        /// <summary>
        /// Timer update rate.
        /// ������� ���������� �������.
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
                var e = GenerateSpawnedEntity();
                e.transform.position = m_Area.GetRandomInsideZone();
            }
        }
        #endregion
    }
}

