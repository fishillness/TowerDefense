using System;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        #region Properties
        /// <summary>
        /// Number of lives.
        /// Количество жизней.
        /// </summary>
        [SerializeField] private int m_NumLives;
        public int NumLives => m_NumLives;
        /// <summary>
        /// Link to the ship.
        /// Ссылка на корабль.
        /// </summary>
        [SerializeField] private SpaceShip m_Ship;
        public SpaceShip ActiveShip => m_Ship;
        /// <summary>
        /// Link to ship prefab.
        /// Ссылка на префаб корабля.
        /// </summary>
        [SerializeField] private GameObject m_PlayerShipPrefab;
        
        /// <summary>
        /// Link to the camera controller.
        /// Ссылка на контроллер камеры.
        /// </summary>
        //[SerializeField] private CameraController m_CameraController;
        /// <summary>
        /// Link to the movement controller.
        /// Ссылка на контроллер движения.
        /// </summary>
        //[SerializeField] private MovementController m_MovementController;
        #endregion

        #region Unity Events
        protected override void Awake()
        {
            base.Awake();
            if (m_Ship != null)
                Destroy(m_Ship.gameObject);
        }

        private void Start()
        {
            if (m_Ship)
                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            Respawn();
        }

        private void OnShipDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);
                m_Ship = newPlayerShip.GetComponent<SpaceShip>();
                m_Ship.EventOnDeath.AddListener(OnShipDeath);                    ////

                //m_CameraController.SetTarget(m_Ship.transform);
                //m_MovementController.SetTargetShip(m_Ship);
            }
        }
        protected void ApplyDamage(int m_damage)
        {
            m_NumLives -= m_damage;
            if (m_NumLives <= 0)
            {
                Debug.Log("End level");
                // LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }
        #endregion

        #region Score
        public int Score { get; private set; }
        public int NumKills { get; private set; }
        public void AddKill()
        {
            NumKills++;
        }
        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion

        #region public API
        public Vector3 GetActiveShipPosition()
        {
            return m_Ship.transform.position;
        }
        public void AddNumLives(int num)
        {
            m_NumLives += num;
        }
        #endregion
    }
}

