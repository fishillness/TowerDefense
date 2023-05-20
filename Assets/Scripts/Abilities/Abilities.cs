using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SpaceShooter;

namespace TowerDefense
{
    public class Abilities : SingletonBase<Abilities>
    {
        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_cost = 5;
            [SerializeField] private int m_damage = 5;
            [SerializeField] private DamageType m_damageType = DamageType.MagicFire;
            [SerializeField] private int m_radius = 5;
            [SerializeField] private Color m_targetingColor;
            public void Use() 
            {
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);

                    var hit = Physics2D.OverlapCircleAll(position, m_radius);
                    foreach (var collider in hit)
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_damage, m_damageType);
                        }
                    }                
                });
                

            }
        }
        [Serializable]
        public class TimeAbility
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
        [SerializeField] private Image m_targetingCircle;
        //[SerializeField] private GameObject m_clickProtection;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();

        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();                
    }
}
