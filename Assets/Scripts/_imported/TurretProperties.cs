using UnityEngine;


namespace SpaceShooter
{
    /// <summary>
    /// Turret types.
    /// Виды турели.
    /// </summary>
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        /// <summary>
        /// Turret type.
        /// Вид турели.
        /// </summary>
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;
        /// <summary>
        /// Projectile prefab.
        /// Префаб снаряда.
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;
        /// <summary>
        /// Turret rate of fire. Delay per second.
        /// Скорострельность турели. Задержка в секунду.
        /// </summary>
        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;
        /// <summary>
        /// How much power the turret needs.
        /// Сколько турели необходимо энергии.
        /// </summary>
        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;
        /// <summary>
        /// How many turrets need ammo.
        /// Сколько турели необходимо патрон.
        /// </summary>
        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;
        /// <summary>
        /// Link to the sound that will be played when the projectile is fired.
        /// Ссылка на звук, который будет воспроизводиться при запуске снаряда.
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}
