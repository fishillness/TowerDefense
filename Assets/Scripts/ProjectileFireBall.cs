using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class ProjectileFireBall : Projectile
    {
        protected override void HitObject(Destructible dest)
        {
            var enemy = dest.GetComponent<Enemy>();
            if (enemy != null && !enemy.IsInvulnerableOfFire)
            {
                dest.ApplyDamage(m_Damage);
                
                var fire = dest.GetComponentInChildren<Fire>();
                if (fire)
                {
                    fire.SetFireActive(dest);
                }
                
                if (Player.Instance != null && dest.HitPoints < 0)
                {
                    if (m_Parent == Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                            /*if (dest.HitPoints <= 0 && dest.GetComponent<SpaceShip>() != null &&
                                dest.TeamId != Player.Instance.ActiveShip.TeamId) //
                            {
                                Player.Instance.AddKill();
                            }*/
                    }
                }
            }            
        }
    }
}

