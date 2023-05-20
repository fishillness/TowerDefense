using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class Abilities : SingletonBase<Abilities>
    {
        public interface Usable{ void Use(); }

        [Serializable]
        public class FireAbility : Usable
        {
            [SerializeField] private int m_cost = 5;
            [SerializeField] private int m_damage = 2;
            public void Use() { }
        }
        [Serializable]
        public class TimeAbility : Usable 
        {
            [SerializeField] private int m_cost = 10;
            [SerializeField] private float m_cooldown = 15f;
            [SerializeField] private float m_duration = 5f;
            public void Use()
            {
                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_duration);

                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();

                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                Instance.StartCoroutine(Restore()); //Abilities.Instance
                
                IEnumerator TimeAbilityButton()
                {
                    Instance.m_timeButton.interactable = false;
                    yield return new WaitForSeconds(m_cooldown);
                    Instance.m_timeButton.interactable = true;
                }

                Instance.StartCoroutine(TimeAbilityButton());
            }
        }
        [SerializeField] private Button m_timeButton;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();

        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();
    }
}
