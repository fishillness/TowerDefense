using UnityEngine;
using SpaceShooter;
using UnityEditor;
using UnityEngine.UIElements;

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_damage;

        public void Use(EnemyAsset asset)
        {
            var spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            spriteRenderer.color = asset.Color;
            spriteRenderer.transform.localScale = new Vector3(asset.SpriteScale.x, asset.SpriteScale.y, 1);
            spriteRenderer.GetComponent<Animator>().runtimeAnimatorController = asset.Animations;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.RadiusCollider;

            m_damage = asset.Damage;
        }

        public void DamagePlayer()
        {
            Player.Instance.ApplyDamage(m_damage);
        }
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
