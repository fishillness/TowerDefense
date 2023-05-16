using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_towerBuyControlPrefab;
        [SerializeField] private TowerAsset[] m_towerAssets;
        [SerializeField] private UpgradeProperties m_mageTowerUpgrade;

        private RectTransform m_rectTransform;
        private List<TowerBuyControl> m_activeControls;

        private void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }

        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                m_rectTransform.anchoredPosition = position;
                gameObject.SetActive(true);

                m_activeControls = new List<TowerBuyControl>();

                //////////////////////////!!!!!
                int k = 0;
                for (int i = 0; i < m_towerAssets.Length; i++)
                {
                    if (i != 1 || Upgrades.GetUpgradeLevel(m_mageTowerUpgrade) > 0)
                    {
                        var newControl = Instantiate(m_towerBuyControlPrefab, transform);
                        m_activeControls.Add(newControl);
                        ////////////////////////////////!!!!
                        newControl.transform.position += Vector3.left * 100 * k; // i;
                        newControl.SetTowerAsset(m_towerAssets[i]);

                        k++;
                    }
                }                
            }
            else
            {
                foreach (var control in m_activeControls)
                {
                    if (control)
                        Destroy(control.gameObject);
                }
                gameObject.SetActive(false);
            }
            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }
    }
}

