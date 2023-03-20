using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        #region Properties
        [SerializeField] private TowerAsset m_towerAsset;
        [SerializeField] private Text m_text;
        [SerializeField] private Button m_button;
        [SerializeField] private Transform m_buildSite;
        #endregion
        #region Unity Events
        private void Start()
        {
            TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
            m_text.text = m_towerAsset.GoldCost.ToString();
            m_button.GetComponent<Image>().sprite = m_towerAsset.SpriteGUI;
        }
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_towerAsset.GoldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;
                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }
        #endregion
        #region Public API
        public void SetBuildSite(Transform value)
        {
            m_buildSite = value;
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_towerAsset, m_buildSite);
            BuildSite.HideControls();
        }
        #endregion
    }
}
