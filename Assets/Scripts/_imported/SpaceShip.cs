using UnityEngine;

namespace SpaceShooter
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        #region Properties
        /// <summary>
        /// Mass for automatic installation at rigidbody.
        /// ����� ��� �������������� ��������� � rigidbody.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Pushing force.
        /// ��������� ������ ����.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Rotating force.
        /// ��������� ����.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Maximum line speed.
        /// ������������ �������� ��������.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// Maximum rotational speed. In degrees/sec
        /// ������������ ������������ ��������. � ��������/���
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// Saved reference to rigidbody.
        /// ����������� ������ �� rigidbody.
        /// </summary>
        private Rigidbody2D m_Rigid;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;
        #endregion

        #region Public API

        /// <summary>
        /// Linear thrust control. -1.0 to +1.0.
        /// ���������� �������� �����. �� -1.0 �� +1.0.
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotary thrust control. -1.0 to +1.0.
        /// ���������� ������������ �����. �� -1.0 �� +1.0.
        /// </summary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity Event

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            //InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidbody();
            //UpdateEnergyRegen();
        }
        #endregion

        /// <summary>
        /// Method for adding forces to the ship for movement.
        /// ����� ���������� ��� ������� ��� ��������.
        /// </summary>
        private void UpdateRigidbody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        /// <summary>
        /// TODO: �������� �������� �����-��������.
        /// ����������� �������.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawAmmo(int count)
        {
            return true;
        }
        /// <summary>
        /// TODO: �������� �������� �����-��������.
        /// ����������� �������.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawEnergy(int count)
        {
            return true;
        }
        /// <summary>
        /// TODO: �������� �������� �����-��������.
        /// ����������� ��-������������.
        /// </summary>
        public void Fire(TurretMode mode)
        {
            return;
        }

        /*
        [SerializeField] private Turret[] m_Turrets;
        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        #region Energy and Ammo
        /// <summary>
        /// Maximum energy value.
        /// ������������ ���������� �������.
        /// </summary>
        [SerializeField] private int m_MaxEnergy;
        /// <summary>
        /// Maximum ammo value.
        /// ������������ ���������� ������.
        /// </summary>
        [SerializeField] private int m_MaxAmmo;
        /// <summary>
        /// The amount of energy restored per second.
        /// ���������� ������� ����������������� � �������.
        /// </summary>
        [SerializeField] private int m_EnergyRegenPerSecond;

        /// <summary>
        /// Current energy value.
        /// ������� ���������� �������.
        /// </summary>
        private float m_PrimaryEnergy;
        /// <summary>
        /// Current ammo value.
        /// ������� ���������� ������.
        /// </summary>
        private int m_SecondaryAmmo;

        public void AddEnergy(int e)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
        }
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);

        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }

            return false;
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }

        #endregion
        */
    }

}