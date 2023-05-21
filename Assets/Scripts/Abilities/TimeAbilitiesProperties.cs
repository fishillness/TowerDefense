using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TimeAbilitiesProperties : ScriptableObject
    {
        [SerializeField] private int m_manaCost = 10;
        [SerializeField] private float m_cooldown = 15f;
        [SerializeField] private float m_duration = 5f;
        [SerializeField] private Color m_enemyPaintColor;

        public int ManaCost => m_manaCost;
        public float CoolDown => m_cooldown;
        public float Duration => m_duration;
        public Color EnemyPaintColor => m_enemyPaintColor;

        [Header("Upgrade")]
        [SerializeField] private UpgradeProperties m_requiredUpgrade;
        [SerializeField] private int m_requiredUpgradeLevel;
        [SerializeField] private TimeAbilitiesProperties[] m_upgradesTo;

        public UpgradeProperties RequiredUpgrade => m_requiredUpgrade;
        public int RequiredUpgradeLevel => m_requiredUpgradeLevel;
        public bool IsAvailable() => !m_requiredUpgrade ||
            m_requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(m_requiredUpgrade);
        public TimeAbilitiesProperties[] UpgradesTo => m_upgradesTo;
    }
}
