using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Weapon type.
        /// ��� ������.
        /// </summary>
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;
        /// <summary>
        /// Link to TurretProperties.
        /// ������ �� TurretProperties.
        /// </summary>
        [SerializeField] private TurretProperties m_turretProperties;
        /// <summary>
        /// Re-shot timer.
        /// ������ ���������� ��������.
        /// </summary>
        private float m_RefireTimer;
        /// <summary>
        /// Possibility to shoot.
        /// ����������� ����������.
        /// </summary>
        public bool CanFire => m_RefireTimer <= 0;
        /// <summary>
        /// Cached link to the parent ship.
        /// ������������ ������ �� ������������ �������.
        /// </summary>
        private SpaceShip m_Ship;
        #endregion

        #region Unity Events
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
                m_RefireTimer -= Time.deltaTime;
        }
        #endregion

        #region Public API
        public void Fire()
        {
            if (m_turretProperties == null) return;

            if (m_RefireTimer > 0) return;

            if (m_Ship.DrawEnergy(m_turretProperties.EnergyUsage) == false)
                return; ///

            if (m_Ship.DrawAmmo(m_turretProperties.AmmoUsage) == false)
                return;

            Projectile projectile = Instantiate(m_turretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            projectile.SetParentShoter(m_Ship);

            m_RefireTimer = m_turretProperties.RateOfFire;
            
            {
                // SFX ������ ������ - ����������
            }
        }

        public void AssignLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            m_turretProperties = props;
        }
        #endregion
    }
}
