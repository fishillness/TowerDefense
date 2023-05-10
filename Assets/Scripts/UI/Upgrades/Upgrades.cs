using System;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string m_filename = "upgrades.dat";

        [Serializable]
        private class UpgradeSave
        {
            public UpgradeProperties m_asset;
            public int m_level = 0;
        }
        [SerializeField] private UpgradeSave[] m_save;

        public static void BuyUpgrade(UpgradeProperties asset)
        {
            foreach (var upgrade in Instance.m_save)
            {
                if (upgrade.m_asset == asset)
                {
                    upgrade.m_level += 1;
                    Saver<UpgradeSave[]>.Save(m_filename, Instance.m_save);
                }
            }
        }

        public static int GetUpgradeLevel(UpgradeProperties asset)
        {
            foreach (var upgrade in Instance.m_save)
            {
                if (upgrade.m_asset == asset)
                {
                    return upgrade.m_level;
                }
            }
            return 0;
        }
        public static int GetTotalCost()
        {
            int result = 0;
            foreach (var upgrade in Instance.m_save)
            {
                for (int i = 0; i < upgrade.m_level; i++)
                {
                    result += upgrade.m_asset.CostByLevel[i];
                }
            }
            return result;
        }

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(m_filename, ref m_save);
        }
    }
}

