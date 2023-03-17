using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [System.Serializable] public class TowerAsset
        {
            [SerializeField] private int m_goldCost = 15;
            public int GoldCost => m_goldCost;

            [SerializeField] private Sprite m_towerGUI;
            public Sprite TowerGUI => m_towerGUI;
        }
        [SerializeField] private TowerAsset m_towerAsset;
        [SerializeField] private Text m_text;
        [SerializeField] private Button m_button;

        private void Awake()
        {
            TDPlayer.OnGoldUpdate += GoldStatusCheck;
        }
        private void Start()
        {
            m_text.text = m_towerAsset.GoldCost.ToString();
            m_button.GetComponent<Image>().sprite = m_towerAsset.TowerGUI;
        }
        private void GoldStatusCheck(int gold)
        {
            if (gold > m_towerAsset.GoldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;
                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }
    }
}
