using UnityEngine;

namespace SpaceShooter
{

    public class Projectile : Entity
    {
        #region Properties
        /// <summary>
        /// Projectile speed.
        /// Скорость снаряда.
        /// </summary>
        [SerializeField] private float m_Velocity;
        /// <summary>
        /// Projectile lifetime.
        /// Время жизни снаряда.
        /// </summary>
        [SerializeField] private float m_Lifetime;
        /// <summary>
        /// Projectile damage.
        /// Урон снаряда.
        /// </summary>
        [SerializeField] private int m_Damage;
        /// <summary>
        /// Link to the explosion prefab.
        /// Ссылка на префаб взрыва.
        /// </summary>
        [SerializeField] private ImpactEffect m_impactEffectPrefab;
        /// <summary>
        /// Lifetime.
        /// Время жизни.
        /// </summary>
        private float m_Timer;
        #endregion

        #region Unity Events
        private void Start()
        {
            Player.Instance.ActiveShip.EventOnDeath.AddListener(OnShipDeath);
        }
        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);
            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();
                if (dest != null && dest != m_Parent)
                {
                    dest.ApplyDamage(m_Damage);

                    if (m_Parent == Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                        if (dest.HitPoints <= 0 && dest.GetComponent<SpaceShip>() != null &&
                            dest.TeamId != Player.Instance.ActiveShip.TeamId) //
                        {
                            Player.Instance.AddKill();
                        }
                    }
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnShipDeath()
        {
            Destroy(gameObject);
        }

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }
        #endregion

        #region Public API
        private Destructible m_Parent;
        public void SetParentShoter(Destructible parent)
        {
            m_Parent = parent;
        }
        #endregion
    }

}