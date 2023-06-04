using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider m_slider;
        [SerializeField] private Vector3 m_offset;

        private void Start()
        {
            m_slider.gameObject.SetActive(true);
        }

        private void Update()
        {
            m_slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + m_offset);
        }

        public void SetHealthValue(int currentHealth, int maxHealth)
        {
            //m_slider.gameObject.SetActive(currentHealth < maxHealth); //хочу ли я так?
            m_slider.maxValue = maxHealth;
            m_slider.value = currentHealth;
        }
    }
}

