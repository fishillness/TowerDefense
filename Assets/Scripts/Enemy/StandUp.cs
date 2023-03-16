using UnityEngine;

namespace TowerDefense
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D m_rigitbody;
        private SpriteRenderer m_spriteRenderer;

        private void Start()
        {
            m_rigitbody = transform.root.GetComponent<Rigidbody2D>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;
            var xMotion = m_rigitbody.velocity.x;
            if (xMotion > 0.01f)
                m_spriteRenderer.flipX = false;
            else
                if (xMotion < 0.01f)
                m_spriteRenderer.flipX = true;
        }
    }
}

