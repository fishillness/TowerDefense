using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        #region Properties
        public static new TDPlayer Instance => Player.Instance as TDPlayer;

        [SerializeField] private int m_gold = 0;

        public static event Action<int> OnGoldUpdate;
        public static event Action<int> OnLifeUpdate;
        #endregion

        #region Unity Events
        private void Start()
        {
            OnGoldUpdate(m_gold);
            OnLifeUpdate(NumLives);
        }
        #endregion
        #region Public API
        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }
        public void ReduceLife(int change)
        {
            ApplyDamage(change);
            OnLifeUpdate(NumLives);
        }
        #endregion
    }
}

