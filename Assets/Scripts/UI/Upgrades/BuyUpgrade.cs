using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeProperties m_asset;

        [SerializeField] private Image m_upgradeIcon;
        [SerializeField] private Text m_levelText;
        [SerializeField] private Text m_upgradeText;
        [SerializeField] private Text m_costText;
        [SerializeField] private Button m_buyButton;

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
            m_levelText.text = $"Level {savedLevel + 1}";
            m_upgradeText.text = m_asset.UpgradeText;
            m_costText.text = m_asset.CostByLevel[savedLevel].ToString();
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(m_asset);
        }
    }
}
