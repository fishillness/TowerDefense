using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_rectTransform;
        private void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }

        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                m_rectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
            }
            else {
                gameObject.SetActive(false);
            }
            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }
    }
}

