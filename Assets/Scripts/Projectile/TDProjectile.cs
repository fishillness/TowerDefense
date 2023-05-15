using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public enum DamageType
    {
        Base,
        MagicFire
    }

    public class TDProjectile : Projectile
    {
        [SerializeField] protected DamageType m_DamageType;

        protected override void OnHit(RaycastHit2D hit)
        {
            Enemy enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(m_Damage, m_DamageType);
            }
        }
    }
}

