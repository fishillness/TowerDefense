using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class UpgradeProperties : ScriptableObject
    {
        [SerializeField] private Sprite m_sprite;
        [SerializeField] private int[] m_costByLevel;
        [SerializeField] private string m_upgradeText;
        [SerializeField] private float m_value;

        public Sprite Sprite => m_sprite;
        public int[] CostByLevel => m_costByLevel;
        public string UpgradeText => m_upgradeText;
        public float Value => m_value;
    }
}


