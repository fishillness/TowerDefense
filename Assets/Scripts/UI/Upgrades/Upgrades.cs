using System;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string filename = "upgrades.dat";

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
                    Saver<UpgradeSave[]>.Save(filename, Instance.m_save);
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

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(filename, ref m_save);
        }

    }
}

