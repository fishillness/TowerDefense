using UnityEngine;

namespace TowerDefense
{    
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController animations;

        [Header("Игровые параметры")]
        public float moveSpeed = 1;
        public int hitpoint = 1;
        public int score = 1;
        public float radiusCollider = 0.17f;
    }
}
