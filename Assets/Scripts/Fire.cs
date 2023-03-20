using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{    public class Fire : MonoBehaviour
    {
        [SerializeField] private GameObject m_fire;
        [SerializeField] private int m_damage;
        [SerializeField] private int m_step;
        [SerializeField] private float m_actionTime;
        private float m_burningTimer;
        private float m_stepTimer;
        private bool isActive;
        private Destructible m_destructible;

        private void Update()
        {
            if (isActive == true)
            {
                m_burningTimer += Time.deltaTime;
                m_stepTimer += Time.deltaTime;

                if (m_destructible)
                {
                    if (m_stepTimer > m_step)
                    {
                        m_destructible.ApplyDamage(m_damage);
                        m_stepTimer = 0;
                    }
                }

                m_fire.SetActive(true);
            }
            if (isActive == false)
            {
                m_fire.SetActive(false);
            }

            
            if (m_burningTimer > m_actionTime)
            {
                isActive = false;
                m_burningTimer = 0;
            }
        }

        public void SetFireActive(Destructible destructible)
        {
            isActive = true;
            m_destructible = destructible;
        }
    }
}
