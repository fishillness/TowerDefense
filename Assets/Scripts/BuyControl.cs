using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_rectTransform;
        private void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToTransform;
            gameObject.SetActive(false);
        }

        private void MoveToTransform(Transform target)
        {
            if (target)
            {
                var position = Camera.main.WorldToScreenPoint(target.position);
                m_rectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
            }
            else {
                gameObject.SetActive(false);
            }
        }
    }
}

