using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler
    {
        private Image m_blocker;
        private Action<Vector2> m_OnClickAction;

        private void Start()
        {
            m_blocker = GetComponent<Image>();
            m_blocker.enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_blocker.enabled = false;
            m_OnClickAction(eventData.pressPosition);
            m_OnClickAction = null;
        }

        public void Activate(Action<Vector2> mouseAction)
        {
            m_blocker.enabled = true;
            m_OnClickAction = mouseAction;
        }

        
    }
}

