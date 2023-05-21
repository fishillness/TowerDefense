using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{    public class BuyUpgrade : MonoBehaviour
    {
        public static event Action OnBuy;

        [SerializeField] private UpgradeProperties m_asset;

        [SerializeField] private Image m_upgradeIcon;
        [SerializeField] private GameObject m_costUI;
        [SerializeField] private Text m_levelText;
        [SerializeField] private Text m_upgradeText;
        [SerializeField] private Text m_costText;
        [SerializeField] private Button m_buyButton;

        private int m_cost;

        public Button BuyButton => m_buyButton;

        private void Start()
        {
            m_buyButton.onClick.AddListener(Buy);
        }

        private void OnDestroy()
        {
            m_buyButton.onClick.RemoveListener(Buy);
        }

        public void Initialize()
        {
            var savedLevel = Upgrades.GetUpgradeLevel(m_asset);
            m_upgradeIcon.sprite = m_asset.Sprite;

            if (savedLevel < m_asset.UpgradeTextByLevel.Length)
                m_upgradeText.text = m_asset.UpgradeTextByLevel[savedLevel];
            else
                m_upgradeText.text = m_asset.UpgradeTextByLevel[m_asset.UpgradeTextByLevel.Length - 1];

            if (savedLevel >= m_asset.CostByLevel.Length)
            {
                m_levelText.text = $"(MAX) Lvl {savedLevel}";
                m_buyButton.interactable = false;
                m_costUI.gameObject.SetActive(false);
                m_cost = int.MaxValue;
            }
            else
            {
                m_cost = m_asset.CostByLevel[savedLevel];
                m_costText.text = m_cost.ToString();
                m_levelText.text = $"Level {savedLevel + 1}";
            }
        }

        public void CheckCost(int starsAmount)
        {
            m_buyButton.interactable = starsAmount >= m_cost;
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(m_asset);
            Initialize();
            OnBuy?.Invoke();
        }
    }
}
