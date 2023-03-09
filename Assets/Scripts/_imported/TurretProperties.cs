using UnityEngine;


namespace SpaceShooter
{
    /// <summary>
    /// Turret types.
    /// ���� ������.
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
        /// ��� ������.
        /// </summary>
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;
        /// <summary>
        /// Projectile prefab.
        /// ������ �������.
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;
        /// <summary>
        /// Turret rate of fire. Delay per second.
        /// ���������������� ������. �������� � �������.
        /// </summary>
        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;
        /// <summary>
        /// How much power the turret needs.
        /// ������� ������ ���������� �������.
        /// </summary>
        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;
        /// <summary>
        /// How many turrets need ammo.
        /// ������� ������ ���������� ������.
        /// </summary>
        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;
        /// <summary>
        /// Link to the sound that will be played when the projectile is fired.
        /// ������ �� ����, ������� ����� ���������������� ��� ������� �������.
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}
