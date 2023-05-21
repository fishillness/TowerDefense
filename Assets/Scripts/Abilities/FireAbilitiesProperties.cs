using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class FireAbilitiesProperties : ScriptableObject
    {
        [SerializeField] private int m_manaCost = 5;
        [SerializeField] private int m_damage = 5;
        [SerializeField] private DamageType m_damageType = DamageType.MagicFire;
        [SerializeField] private int m_radius = 2;

        public int ManaCost => m_manaCost;
        public int Damage => m_damage;
        public DamageType DamageType => m_damageType;
        public int Radius => m_radius;

        [Header("Upgrade")]
        [SerializeField] private UpgradeProperties m_requiredUpgrade;
        [SerializeField] private int m_requiredUpgradeLevel;
        [SerializeField] private FireAbilitiesProperties[] m_upgradesTo;

        public UpgradeProperties RequiredUpgrade => m_requiredUpgrade;
        public int RequiredUpgradeLevel => m_requiredUpgradeLevel;
        public bool IsAvailable() => !m_requiredUpgrade ||
            m_requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(m_requiredUpgrade);
        public FireAbilitiesProperties[] UpgradesTo => m_upgradesTo;
    }
}
