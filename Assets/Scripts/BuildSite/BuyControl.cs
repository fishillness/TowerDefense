using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_towerBuyControlPrefab;

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

        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                m_rectTransform.anchoredPosition = position;
                m_activeControls = new List<TowerBuyControl>();
                  
                foreach (var asset in buildSite.BuildableTowers)
                {
                    if (asset.IsAvailable())
                    {
                        var newControl = Instantiate(m_towerBuyControlPrefab, transform);
                        m_activeControls.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }
                if (m_activeControls.Count > 0)
                {
                    gameObject.SetActive(true);
                    var angle = 360 / m_activeControls.Count;
                    for (int i = 0; i < m_activeControls.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.up * 100);
                        m_activeControls[i].transform.position += offset;
                    }

                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }
                }
            }
            else
            {
                if (m_activeControls != null)
                {
                    foreach (var control in m_activeControls)
                    {
                        if (control)
                            Destroy(control.gameObject);
                    }
                    m_activeControls.Clear();
                }                
                gameObject.SetActive(false);
            }
        }
    }
}

