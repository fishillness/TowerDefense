using System;
using UnityEngine;
using SpaceShooter;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    public enum ArmorType
    {
        Base = 0,
        MagicFire = 1
    }

    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        private static Func<int, DamageType, int, int>[] ArmorDamageFunctions =
        {
            //ArmorType.Base
            (int damage, DamageType type, int armor) =>
            {
                switch (type)
                {
                    case DamageType.MagicFire:
                        return damage;
                    default: 
                        return Mathf.Max(damage - armor, 1);
                }
            },
            //ArmorType.MagicFire
            (int damage, DamageType type, int armor) => 
            {
                if (type == DamageType.Base)
                     armor = armor / 2;
                
                return Mathf.Max(damage - armor, 1);
            }
        };

        #region Properties
        [SerializeField] private int m_damage;
        [SerializeField] private int m_gold;
        [SerializeField] private int m_armor;
        [SerializeField] private bool invulnerabilityOfFire;
        public bool IsInvulnerableOfFire => invulnerabilityOfFire;
        [SerializeField] private ArmorType m_armorType;

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
            m_armorType = asset.ArmorType;

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

        public void TakeDamage(int damage, DamageType damageType)
        {
            m_destructible.ApplyDamage(ArmorDamageFunctions[(int) m_armorType](damage, damageType, m_armor));

            //m_destructible.ApplyDamage(Mathf.Max(1, damage - m_armor));
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
