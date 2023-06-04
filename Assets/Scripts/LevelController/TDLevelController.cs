using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{    
    public class TDLevelController : LevelController
    {
        public static event Action<int> OnLevelCompleted;

        private int m_levelScore = 3;
        
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(false);
            };
            m_ReferenceTime += Time.time;
            m_EventLevelCompleted.AddListener( () =>
            {
                StopLevelActivity();
                if (m_ReferenceTime <= Time.time)
                {
                    m_levelScore -= 1;
                }
                MapCompletion.SaveEpisodeResult(m_levelScore);
                OnLevelCompleted?.Invoke(m_levelScore);
            });

            void LifeScoreChange(int _)
            {
                m_levelScore -= 1;
                TDPlayer.OnLifeUpdate -= LifeScoreChange;
            } 
            TDPlayer.OnLifeUpdate += LifeScoreChange;
        }     
        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<EnemyWave>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
            DisableAll<TDPlayer>();
        }
    }
}
