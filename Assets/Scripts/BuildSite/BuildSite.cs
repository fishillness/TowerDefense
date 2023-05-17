using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<BuildSite> OnClickEvent;

        [SerializeField] private TowerAsset[] m_buildableTowers;
        public TowerAsset[] BuildableTowers => m_buildableTowers;

        public static void HideControls()
        {
            OnClickEvent(null);
        }
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }
        public void SetBuildableTowers(TowerAsset[] towers)
        {
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                m_buildableTowers = towers;
            }
        }
    }
}

