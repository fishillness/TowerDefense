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
            [SerializeField] private FireAbilitiesProperties m_fireProperties;

            private int m_manaCost = 5;
            private int m_damage = 5;
            private DamageType m_damageType = DamageType.MagicFire;
            private int m_radius = 2;

            public FireAbilitiesProperties FireProperties => m_fireProperties;
            public int ManaCost => m_manaCost;

            public void Use()
            {
                TDPlayer.Instance.ChangeMana(-m_manaCost);

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

            public void SetProperties(FireAbilitiesProperties properties)
            {
                m_manaCost = properties.ManaCost;
                m_damage = properties.Damage;
                m_damageType = properties.DamageType;
                m_radius = properties.Radius;
            }
        }
        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private TimeAbilitiesProperties m_timePropteries;

            private int m_manaCost = 10;
            private float m_cooldown = 15f;
            private float m_duration = 5f;
            private Color m_enemyPaintColor;

            public TimeAbilitiesProperties TimeProperties => m_timePropteries;
            public int ManaCost => m_manaCost;

            public void Use()
            {
                TDPlayer.Instance.ChangeMana(-m_manaCost);

                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                    enemy.GetComponentInChildren<SpriteRenderer>().color = m_enemyPaintColor;
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfMaxLinearVelocity();
                    ship.GetComponentInChildren<SpriteRenderer>().color = m_enemyPaintColor;
                }

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_duration);

                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreMaxLinearVelocity();
                        ship.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    }

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
            public void SetProperties(TimeAbilitiesProperties properties)
            {
                m_manaCost = properties.ManaCost;
                m_cooldown = properties.CoolDown;
                m_duration = properties.Duration;
                m_enemyPaintColor = properties.EnemyPaintColor;
            }
        }
        [Header("GameObjects")]
        [SerializeField] private Button m_fireButton;
        [SerializeField] private Button m_timeButton;
        [SerializeField] private Text m_fireAbilityText;
        [SerializeField] private Text m_timeAbilityText;

        [Header("Fire Ability")]
        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();

        [Header("Time Ability")]
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();

        private void Start()
        {
            TDPlayer.ManaUpdateSubscribe(CheckManaCost);

            m_fireAbilityText.text = m_FireAbility.ManaCost.ToString();
            m_timeAbilityText.text = m_TimeAbility.ManaCost.ToString();

            m_FireAbility.SetProperties(m_FireAbility.FireProperties);
            m_TimeAbility.SetProperties(m_TimeAbility.TimeProperties);

            CheckManaCost(0);
        }

        private void OnDestroy()
        {
            TDPlayer.ManaUpdateUnsubscribe(CheckManaCost);
        }

        private void CheckManaCost(int value)
        {
            m_fireButton.interactable = (TDPlayer.Instance.CurrentMana > m_FireAbility.ManaCost);
            m_timeButton.interactable = (TDPlayer.Instance.CurrentMana > m_TimeAbility.ManaCost);
        }
    }
}
