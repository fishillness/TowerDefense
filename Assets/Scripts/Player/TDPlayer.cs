using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        #region Properties
        public static new TDPlayer Instance => Player.Instance as TDPlayer;

        #region Actions
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
        public static event Action<int> OnManaUpdate;
        public static void ManaUpdateSubscribe(Action<int> act)
        {
            OnManaUpdate += act;
            act(Instance.m_currentMana);
        }
        public static void ManaUpdateUnsubscribe(Action<int> act)
        {
            OnManaUpdate -= act;
        }
        #endregion

        [SerializeField] private int m_gold;
        [SerializeField] private Tower m_towerPrefab;

        [Header("Mana")]
        [SerializeField] private int m_currentMana;
        [SerializeField] private int m_maxMana;
        [SerializeField] private float m_recoveryManaTime;
        [SerializeField] private int m_stepRecoveryMana;

        [Header("Upgrades")]
        [SerializeField] private UpgradeProperties m_healthUpgrade;
        //[SerializeField] private int m_extraHeartsPerUpgrade;

        [SerializeField] private UpgradeProperties m_goldUpgrade;
        //[SerializeField] private int m_extraGoldPerUpgrade;

        private Timer m_recoveryManaTimer;

        public int CurrentMana => m_currentMana;
        #endregion

        public void Start()
        {
            var level = Upgrades.GetUpgradeLevel(m_healthUpgrade);
            ApplyDamage(-level * (int) m_healthUpgrade.Value);// m_extraHeartsPerUpgrade);

            level = Upgrades.GetUpgradeLevel(m_goldUpgrade);
            if (level >= 1)
                ChangeGold(level * (int) m_goldUpgrade.Value);// m_extraGoldPerUpgrade);

            InitTimers();
        }
        /*
        public new void Awake()
        {
            base.Awake();
            var level = Upgrades.GetUpgradeLevel(m_healthUpgrade);
            ApplyDamage(-level * (int) m_healthUpgrade.Value);// m_extraHeartsPerUpgrade);

            level = Upgrades.GetUpgradeLevel(m_goldUpgrade);
            if (level >= 1)
                ChangeGold(level * (int) m_goldUpgrade.Value);// m_extraGoldPerUpgrade);
        }
        */
        private void Update()
        {
            UpdateTimers();

            if (m_recoveryManaTimer.IsFinished)
            {
                ChangeMana(m_stepRecoveryMana);
                m_recoveryManaTimer.Start(m_recoveryManaTime);
            }
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

        public void ChangeMana(int change)
        {
            m_currentMana += change;

            if (m_currentMana >= m_maxMana)
                m_currentMana = m_maxMana;

            OnManaUpdate(m_currentMana);
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
            tower.GetComponentInChildren<BuildSite>().SetBuildableTowers(towerAsset.UpgradesTo);

            Destroy(buildSite.gameObject);
        }
        #endregion

        #region Timers
        private void InitTimers()
        {
            m_recoveryManaTimer = new Timer(m_recoveryManaTime);
        }

        private void UpdateTimers()
        {
            m_recoveryManaTimer.RemoveTime(Time.deltaTime);
        }
        #endregion
    }
}

