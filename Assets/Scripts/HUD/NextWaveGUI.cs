using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{   
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text m_bonusAmount;
        private EnemyWaveManager m_manager;
        private float m_timeToNextWave;

        private void Start()
        {
            m_manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                m_timeToNextWave = time;
            };
        }
        private void Update()
        {
            var bonus = (int)m_timeToNextWave;
            if (bonus < 0)
                bonus = 0;

            m_bonusAmount.text = bonus.ToString();
            m_timeToNextWave -= Time.deltaTime;
        }

        public void CallWave()
        {
            m_manager.ForceNextWave();
        }
    }
}
