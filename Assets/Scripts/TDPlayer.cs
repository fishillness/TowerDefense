using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        #region Properties
        public static new TDPlayer Instance => Player.Instance as TDPlayer;
        private static event Action<int> OnGoldUpdate;
        public static void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }
        private static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        [SerializeField] private int m_gold = 0;
        [SerializeField] private Tower m_towerPrefab;
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

        //TODO: сделать проверку на кол-во золота
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            //if (m_gold >= m_towerAsset.GoldCost)
            ChangeGold(-towerAsset.GoldCost);
            var tower = Instantiate<Tower>(m_towerPrefab, buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.Sprite;
           
            Destroy(buildSite.gameObject);
        }
        #endregion
    }
}

