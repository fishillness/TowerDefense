using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradesShop : MonoBehaviour
    {        
        [SerializeField] private GameObject m_shopPanel;
        [SerializeField] private Button m_openShopnButton;

        [SerializeField] private Text m_starsAmountText;
        [SerializeField] private int m_starsAmount;

        [SerializeField] private BuyUpgrade[] upgrades;

        private void Start()
        {
            m_shopPanel.SetActive(false);
            m_openShopnButton.onClick.AddListener(OpenShop);

            m_starsAmount = MapCompletion.Instance.GetTotalScore(); ////&?????????????????/
            m_starsAmountText.text = m_starsAmount.ToString();

            foreach (var slot in upgrades)
            {
                slot.Initialize();
            }
        }

        private void OnDestroy()
        {
            m_openShopnButton.onClick.RemoveListener(OpenShop);
        }

        private void OpenShop()
        {
            m_shopPanel.SetActive(!m_shopPanel.activeSelf);
        }
    }
}

