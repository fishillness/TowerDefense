using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class ProjectileFireBall : TDProjectile
    {
        protected override void OnHit(RaycastHit2D hit)
        {
            Enemy enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)// && !enemy.IsInvulnerableOfFire)
            {
                enemy.TakeDamage(m_Damage, m_DamageType);

                if (!enemy.IsInvulnerableOfFire)
                {
                    var dest = enemy.transform.root.GetComponent<Destructible>();
                    var fire = dest.GetComponentInChildren<Fire>();
                    if (fire)
                    {
                        fire.SetFireActive(dest);
                    }
                }                
            }
        }

        /*protected override void HitObject(Destructible dest)//(Enemy enemy)
        {
            //var dest = enemy.transform.root.GetComponent<Destructible>();
            var enemy = dest.GetComponent<Enemy>();
            if (enemy != null && !enemy.IsInvulnerableOfFire)
            {
                dest.ApplyDamage(m_Damage);
                
                var fire = dest.GetComponentInChildren<Fire>();
                if (fire)
                {
                    fire.SetFireActive(dest);
                }
            }            
        }*/
    }
}

