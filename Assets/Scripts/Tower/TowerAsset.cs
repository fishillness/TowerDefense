using UnityEngine;

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
    }
}
