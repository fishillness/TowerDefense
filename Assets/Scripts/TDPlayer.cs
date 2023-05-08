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
        public static void GoldUpdateUnsubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }
        public static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }
        public static void LifeUpdateUnsubscribe(Action<int> act)
        {
            OnLifeUpdate -= act;
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

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.GoldCost);
            var tower = Instantiate<Tower>(m_towerPrefab, buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.Sprite;

            if (towerAsset.TurretProperties)
            {
                tower.GetComponentInChildren<Turret>().SetTurretProperties(towerAsset.TurretProperties);
            }
           
            Destroy(buildSite.gameObject);
        }
        #endregion
    }
}

