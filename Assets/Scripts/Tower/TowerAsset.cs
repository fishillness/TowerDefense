using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {        
        [SerializeField] private int m_goldCost = 15;
        public int GoldCost => m_goldCost;

        [SerializeField] private Sprite m_sprite;
        public Sprite Sprite => m_sprite;

        [SerializeField] private Sprite m_spriteGUI;
        public Sprite SpriteGUI => m_spriteGUI;

        [SerializeField] private TurretProperties m_turretProperties;
        public TurretProperties TurretProperties => m_turretProperties;

        [Header("Upgrade")]
        [SerializeField] private UpgradeProperties m_requiredUpgrade;
        public UpgradeProperties RequiredUpgrade => m_requiredUpgrade;

        [SerializeField] private int m_requiredUpgradeLevel;
        public int RequiredUpgradeLevel => m_requiredUpgradeLevel;
        public bool IsAvailable() => !m_requiredUpgrade || 
            m_requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(m_requiredUpgrade);

        [SerializeField] private TowerAsset[] m_upgradesTo;
        public TowerAsset[] UpgradesTo => m_upgradesTo;    
    }
}
