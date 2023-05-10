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

        [SerializeField] private UpgradeProperties m_healthUpgrade;
        //[SerializeField] private int m_extraHeartsPerUpgrade;

        [SerializeField] private UpgradeProperties m_goldUpgrade;
        //[SerializeField] private int m_extraGoldPerUpgrade;
        #endregion

        public new void Awake()
        {
            base.Awake();
            var level = Upgrades.GetUpgradeLevel(m_healthUpgrade);
            ApplyDamage(-level * (int) m_healthUpgrade.Value);// m_extraHeartsPerUpgrade);

            level = Upgrades.GetUpgradeLevel(m_goldUpgrade);
            if (level >= 1)
                ChangeGold(level * (int) m_goldUpgrade.Value);// m_extraGoldPerUpgrade);
        }

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

