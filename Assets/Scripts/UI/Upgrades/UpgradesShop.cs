using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradesShop : MonoBehaviour
    {        
        [SerializeField] private GameObject m_shopPanel;
        [SerializeField] private GameObject m_starsPanel;
        [SerializeField] private Button m_openShopnButton;

        [SerializeField] private Text m_starsAmountText;
        [SerializeField] private int m_starsAmount;

        [SerializeField] private BuyUpgrade[] m_upgrades;

        private void Start()
        {
            m_shopPanel.SetActive(false);
            m_starsPanel.SetActive(true);
            m_openShopnButton.onClick.AddListener(OpenShop);

            foreach (var slot in m_upgrades)
            {
                slot.Initialize();
                //slot.BuyButton.onClick.AddListener(UpdateStars);
              
            }

            BuyUpgrade.OnBuy += UpdateStars;

            UpdateStars();
        }

        private void OnDestroy()
        {
            m_openShopnButton.onClick.RemoveListener(OpenShop);
            /*foreach (var slot in m_upgrades)
            {
                slot.BuyButton.onClick.RemoveListener(UpdateStars);
            }*/
            BuyUpgrade.OnBuy -= UpdateStars;
        }

        public void UpdateStars()
        {
            m_starsAmount = MapCompletion.Instance.GetTotalScore();
            m_starsAmount -= Upgrades.GetTotalCost();
            m_starsAmountText.text = m_starsAmount.ToString();

            foreach(var slot in m_upgrades)
            {
                slot.CheckCost(m_starsAmount);
            }
        }

        private void OpenShop()
        {
            m_shopPanel.SetActive(!m_shopPanel.activeSelf);
            m_starsPanel.SetActive(!m_shopPanel.activeSelf);
        }
    }
}

