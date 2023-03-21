using UnityEngine;

namespace TowerDefense
{    
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        [SerializeField] private Color m_color = Color.white;
        public Color Color => m_color;

        [SerializeField] private Vector2 m_spriteScale = new Vector2(3, 3);
        public Vector2 SpriteScale => m_spriteScale;

        [SerializeField] private RuntimeAnimatorController m_animations;
        public RuntimeAnimatorController Animations => m_animations;

        [Header("Игровые параметры")]
        [SerializeField] private float m_moveSpeed = 1;
        public float MoveSpeed => m_moveSpeed;

        [SerializeField] private int m_hitpoint = 1;
        public int HitPoint => m_hitpoint;

        [SerializeField] private int m_score = 1;
        public int Score => m_score;

        [SerializeField] private float m_radiusCollider = 0.17f;
        public float RadiusCollider => m_radiusCollider;

        [SerializeField] private int m_damage = 1;
        public int Damage => m_damage;

        [SerializeField] int m_gold = 1;
        public int Gold => m_gold;

        [SerializeField] private bool m_invulnerabilityOfFire;
        public bool IsInvulnerableOfFire => m_invulnerabilityOfFire;
    }
}
