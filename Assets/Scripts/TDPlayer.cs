using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private int m_gold = 0;
        public void ChangeGold(int change)
        {
            m_gold += change;
        }
    }
}

