using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_path;
        private int pathIndex;
        public void SetPath(Path path)
        {
            m_path = path;
            pathIndex = 0;
            SetPatrolBehaviour(path[pathIndex]);
        }
        protected override void GetNewPoint()
        {
            pathIndex++;
            if (m_path.Length > pathIndex)
            {
                SetPatrolBehaviour(m_path[pathIndex]);
            }
            else
            {
                Destroy(gameObject);
                //TO DO урон лагерю
            }
        }
    }
}

