using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class ProjectileFireBall : Projectile
    {
        protected override void HitObject(Enemy enemy)
        {
            var dest = enemy.transform.root.GetComponent<Destructible>();
            //var enemy = dest.GetComponent<Enemy>();
            if (enemy != null && !enemy.IsInvulnerableOfFire)
            {
                dest.ApplyDamage(m_Damage);
                
                var fire = dest.GetComponentInChildren<Fire>();
                if (fire)
                {
                    fire.SetFireActive(dest);
                }
            }            
        }
    }
}

