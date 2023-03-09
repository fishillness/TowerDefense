using UnityEngine;

namespace SpaceShooter
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        #region Properties
        /// <summary>
        /// Mass for automatic installation at rigidbody.
        /// Масса для автоматической установки у rigidbody.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Pushing force.
        /// Толкающая вперед сила.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Rotating force.
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Maximum line speed.
        /// Максимальная линейная скорость.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// Maximum rotational speed. In degrees/sec
        /// Максимальная вращательная скорость. В градусах/сек
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// Saved reference to rigidbody.
        /// Сохраненная ссылки на rigidbody.
        /// </summary>
        private Rigidbody2D m_Rigid;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;
        #endregion

        #region Public API

        /// <summary>
        /// Linear thrust control. -1.0 to +1.0.
        /// Управление линейной тягой. От -1.0 до +1.0.
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotary thrust control. -1.0 to +1.0.
        /// Управление вращательной тягой. От -1.0 до +1.0.
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
        /// Метод добавления сил кораблю для движения.
        /// </summary>
        private void UpdateRigidbody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        /// <summary>
        /// TODO: заменить временнй метод-заглушку.
        /// Используетс турелми.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawAmmo(int count)
        {
            return true;
        }
        /// <summary>
        /// TODO: заменить временнй метод-заглушку.
        /// Используетс турелми.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawEnergy(int count)
        {
            return true;
        }
        /// <summary>
        /// TODO: заменить временнй метод-заглушку.
        /// Используетс ИИ-контроллером.
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
        /// Максимальный показатель энергии.
        /// </summary>
        [SerializeField] private int m_MaxEnergy;
        /// <summary>
        /// Maximum ammo value.
        /// Максимальный показатель патрон.
        /// </summary>
        [SerializeField] private int m_MaxAmmo;
        /// <summary>
        /// The amount of energy restored per second.
        /// Количество энергии восстанавливаемое в секунду.
        /// </summary>
        [SerializeField] private int m_EnergyRegenPerSecond;

        /// <summary>
        /// Current energy value.
        /// Текущий показатель энергии.
        /// </summary>
        private float m_PrimaryEnergy;
        /// <summary>
        /// Current ammo value.
        /// Текущий показатель патрон.
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