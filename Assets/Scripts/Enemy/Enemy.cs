using System;
using UnityEngine;
using SpaceShooter;
using UnityEditor;
using UnityEngine.UIElements;

namespace TowerDefense
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        #region Properties
        [SerializeField] private int m_damage;
        [SerializeField] private int m_gold;
        [SerializeField] private int m_armor;
        [SerializeField] private bool invulnerabilityOfFire;
        public bool IsInvulnerableOfFire => invulnerabilityOfFire;

        public event Action OnEnd;

        private Destructible m_destructible;

        #endregion
        private void Awake()
        {
            m_destructible = GetComponent<Destructible>();
        }
        private void OnDestroy() { OnEnd?.Invoke(); }
        #region Public API
        public void Use(EnemyAsset asset)
        {
            var spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            spriteRenderer.color = asset.Color;
            spriteRenderer.transform.localScale = new Vector3(asset.SpriteScale.x, asset.SpriteScale.y, 1);
            spriteRenderer.GetComponent<Animator>().runtimeAnimatorController = asset.Animations;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.RadiusCollider;

            m_damage = asset.Damage;
            m_gold = asset.Gold;
            m_armor = asset.Armor;

            invulnerabilityOfFire = asset.IsInvulnerableOfFire;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_gold);
        }

        public void TakeDamage(int damage)
        {
            m_destructible.ApplyDamage(Mathf.Max(1, damage - m_armor));
        }
        #endregion
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, 
                typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
    }
#endif
}
